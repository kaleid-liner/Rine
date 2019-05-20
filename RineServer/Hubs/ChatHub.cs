using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RineServer.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {

    }
}
