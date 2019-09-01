using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RineServer.Areas.Identity.ControllerModels
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
