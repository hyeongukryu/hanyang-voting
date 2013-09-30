using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Models
{
    public class Right
    {
        public long Id { get; set; }

        public bool Expired { get; set; }
        public virtual Station Station { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual Choice Choice { get; set; }
    }
}
