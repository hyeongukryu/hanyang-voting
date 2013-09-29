using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Models
{
    public class Voter
    {
        public long Id { get; set; }

        public virtual Event Event { get; set; }

        public string Number { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public bool Registerd { get; set; }
    }
}
