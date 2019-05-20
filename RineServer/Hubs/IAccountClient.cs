using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RineSignalRContracts;

namespace RineServer.Hubs
{
    public interface IAccountClient
    {
        Task OnSignin(UserInfo user);
        Task OnSignup(UserInfo user);
        Task OnSignout(UserInfo user);
    }
}
