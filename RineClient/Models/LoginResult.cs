using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RineClient.Models
{
    public class LoginResult
    {
        public enum LoginResultType
        {
            Success,
            Failed
        }

        public LoginResultType ResultType { get; set; }

        public List<string> Errors { get; set; }
    }
}
