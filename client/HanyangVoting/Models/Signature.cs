using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Models
{
    public class Signature
    {
        public long Id { get; set; }

        public virtual Voter Voter { get; set; }
        public virtual Choice Choice { get; set; }
        public byte[] Strokes { get; set; }
    }
}
