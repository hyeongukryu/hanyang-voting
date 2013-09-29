using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Models
{
    public class Station
    {
        public long Id { get; set; }

        public virtual Event Event { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Booth> Booths { get; set; }
    }
}
