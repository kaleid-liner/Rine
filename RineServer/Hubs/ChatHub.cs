using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using RineSignalRContracts;
using System.Collections.Concurrent;
using RineServer.Areas.Identity.Models;
using RineServer.Models;

namespace RineServer.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        private readonly RineServerContext _context;

        public ChatHub(RineServerContext context)
        {
            _context = context;
        }

        public async Task SendMessage(MessageSent mesg)
        {
            // don't use `mesg.Sender` to prevent forged messages
            RineUser sender = _context.Users.First(u => u.UserName == Context.User.Identity.Name);
            RineUser receiver = _context.Users.First(u => u.UserName == mesg.Receiver);

            bool received = false;
            DateTime now = DateTime.Now;

            if (receiver.Status == UserStatus.Online || receiver.Status == UserStatus.Invisible)
            {
                await Clients.User(receiver.UserName).ReceiveMessage(new MessageRecv
                {
                    Content = mesg.Content,
                    Sender = sender.UserName,
                    Created = now,
                });
                received = true;
            }

            if (sender != null && receiver != null)
            {
                _context.Add(new RineMessage
                {
                    Content = mesg.Content,
                    Sender = sender,
                    Receiver = receiver,
                    Received = received
                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task AddFriend(FriendRequestSent friend)
        {
            RineUser receiver = _context.Users.First(u => u.UserName == friend.Receiver);
            RineUser sender = _context.Users.First(u => u.UserName == Context.User.Identity.Name);

            DateTime now = DateTime.Now;

            if (sender != null && receiver != null)
            {
                var friendship = new Friendship
                {
                    UserRecvId = receiver.Id,
                    UserRequestId = sender.Id,
                    Description = friend.Description,
                    Status = FriendshipStatus.Pending,
                };
                _context.Add(friendship);
                await _context.SaveChangesAsync();

                await Clients.User(receiver.UserName).NotifyFriendRequests(new FriendRequestRecv
                {
                    Id = friendship.Id,
                    Sender = sender.UserName,
                    Created = now,
                    Description = friend.Description,
                });
            }
        }

        public async Task ActionFriendRequest(FriendRequestAction action)
        {
            RineUser sender = _context.Users.First(u => u.UserName == action.Sender);
            RineUser receiver = _context.Users.First(u => u.UserName == Context.User.Identity.Name);
            Friendship friendship = _context.Friendship.Find(action.Id);

            if (receiver != null 
                && sender != null 
                && friendship != null
                && friendship.UserRecvId == receiver.Id
                && friendship.UserRequestId == sender.Id
                && friendship.Status == FriendshipStatus.Pending)
            {
                if (action.Accept)
                {
                    await Clients.User(sender.UserName).NotifyNewFriend(UserToFriendInfo(receiver));
                    await Clients.User(receiver.UserName).NotifyNewFriend(UserToFriendInfo(sender));
                }

                friendship.Actioned = DateTime.Now;
                friendship.Status = action.Accept
                    ? FriendshipStatus.Accepted
                    : FriendshipStatus.Denied;
                await _context.SaveChangesAsync();
            }
        }

        public async override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;

            RineUser user = _context.Users.First(u => u.UserName == name);

            user.Status = UserStatus.Online;
            await _context.SaveChangesAsync();

            var friendsOnline = (from f in _context.GetAllFriends(user)
                                 select f.UserName).ToList();
            await Clients.Users(friendsOnline).NotifyFriendStatus(UserToFriendInfo(user));

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;

            RineUser user = _context.Users.First(u => u.UserName == name);

            user.Status = UserStatus.Offline;
            user.LastOnline = DateTime.Now;
            await _context.SaveChangesAsync();

            var friendsOnline = (from f in _context.GetAllFriendsOnline(user)
                                 select f.UserName).ToList();
            await Clients.Users(friendsOnline).NotifyFriendStatus(UserToFriendInfo(user));

            await base.OnDisconnectedAsync(exception);
        }

        public static FriendInfo UserToFriendInfo(RineUser user)
        {
            return new FriendInfo
            {
                UserName = user.UserName,
                Created = user.Created,
                LastOnline = user.LastOnline,
                Status = user.Status == UserStatus.Online
                    ? FriendStatus.Online
                    : FriendStatus.Offline,
            };
        }

    }
}
