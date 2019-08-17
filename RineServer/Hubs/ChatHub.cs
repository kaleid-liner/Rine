using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using RineSignalRContracts;
using System.Collections.Concurrent;
using RineServer.Areas.Identity.Models;
using RineServer.Models;

namespace RineServer.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        private readonly static ConcurrentDictionary<string, HashSet<string>> _connections
            = new ConcurrentDictionary<string, HashSet<string>>();

        private readonly RineServerContext _context;

        public ChatHub(RineServerContext context)
        {
            _context = context;
        }

        public async Task SendMessage(MessageInfo mesg)
        {
            // don't use `mesg.Sender` to prevent forged messages
            RineUser sender = _context.Users.First(u => u.Username == Context.User.Identity.Name);
            RineUser receiver = _context.Users.First(u => u.Username == mesg.Receiver);

            if (sender != null && receiver != null)
            {
                mesg.Sent = DateTime.Now;
                bool received = false;

                if (_connections.TryGetValue(mesg.Receiver, out var connections))
                {
                    var tasks = new List<Task>();
                    foreach (var connId in connections)
                    {
                        tasks.Add(Clients.Client(connId).ReceiveMessage(mesg));
                        received = true;
                    }
                    await Task.WhenAll(tasks);
                }

                _context.Add(new RineMessage
                {
                    Content = mesg.Content,
                    Sent = mesg.Sent,
                    Sender = sender,
                    Receiver = receiver,
                    Received = received
                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task AddFriend(UserInfo friend)
        {
            RineUser newFriend = _context.RineUser.First(u => u.Username == friend.Username);
            RineUser user = _context.RineUser.First(u => u.Username == Context.User.Identity.Name);

            if (user != null && newFriend != null)
            {
                var friendship = new Friendship
                {
                    UserRecvId = newFriend.Id,
                    UserRequestId = user.Id,
                    Accepted = false,
                    Created = DateTime.Now
                };
                _context.Add(friendship);
                user.FriendRequest.Append(friendship);
                newFriend.FriendRecv.Append(friendship);
                await _context.SaveChangesAsync();

                friend.Username = Context.User.Identity.Name;
                if (_connections.TryGetValue(friend.Username, out var connections))
                {
                    var tasks = new List<Task>();
                    foreach (var connId in connections)
                    {
                        tasks.Add(Clients.Client(connId).NotifyFriendRequests(friend));
                    }
                    await Task.WhenAll(tasks);
                }
            }
        }

        public async Task AcceptFriend(UserInfo friend)
        {
            RineUser newFriend = _context.RineUser.First(u => u.Username == friend.Username);
            RineUser user = _context.RineUser.First(u => u.Username == Context.User.Identity.Name);
            Friendship friendship = user.FriendRecv.First(u => u.UserRecvId == user.Id);

            if (user != null && newFriend != null && friendship != null)
            {
                friendship.Actioned = DateTime.Now;
                friendship.Accepted = true;
            }

            friend.Username = newFriend.Username;
            if (_connections.TryGetValue(friend.Username, out var connections))
            {
                var tasks = new List<Task>();
                foreach (var connId in connections)
                {
                    tasks.Add(Clients.Client(connId).NotifyFriendAccepted(friend));
                }
                await Task.WhenAll(tasks);
            }
        }

        //
        public override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;

            if (!_connections.TryGetValue(name, out var connections))
            {
                connections = new HashSet<string>();
                _connections[name] = connections;
            }

            lock (connections)
            {
                connections.Add(Context.ConnectionId);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;

            _connections.TryGetValue(name, out var connections);

            lock (connections)
            {
                connections.Remove(Context.ConnectionId);
            }

            return base.OnDisconnectedAsync(exception);
        }

    }
}
