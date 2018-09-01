using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rine.DataLibrary
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }

        public int DstUid { get; set; }

        public int SrcUid { get; set; }

        public string Content { get; set; }

        public DateTime Time { get; set; }

        public override bool Equals(object obj)
        {
            return Code == (obj as Message).Code;
        }

        public override int GetHashCode()
        {
            return Code;
        }
    }
}
