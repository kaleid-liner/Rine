using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rine.ServiceContracts
{
    class UidNotValidException : Exception
    {
        private int uid;
        public UidNotValidException(int uid) : 
            base("Uid must be larger than zero")
        {
            this.uid = uid;
        }

        public override string ToString()
        {
            return Message + '[' + uid + ']';
        }
    }
}
