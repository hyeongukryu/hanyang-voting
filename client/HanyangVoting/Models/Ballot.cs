using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Models
{
    public class Ballot
    {
        public long Id { get; set; }
        
        public virtual Booth Booth { get; set; }
        public virtual Choice Question { get; set; }
        public virtual Option Answer { get; set; }
    }
}
