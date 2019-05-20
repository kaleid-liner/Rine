using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RineSignalRContracts;

namespace RineServer.Hubs
{
    public interface IAccountClient
    {
        Task Signin(UserInfo user);
        Task Signup(UserInfo user);
        Task Signout(UserInfo user);
    }
}
