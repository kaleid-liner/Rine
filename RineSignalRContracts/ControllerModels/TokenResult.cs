using System;
using System.Collections.Generic;
using System.Text;

namespace RineSignalRContracts.ControllerModels
{
    public class TokenResult
    {
        public int Code { get; set; }
        public List<string> Messages { get; set; }
        public string Token { get; set; }
    }
}
