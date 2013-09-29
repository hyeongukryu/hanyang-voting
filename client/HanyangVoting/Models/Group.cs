using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Models
{
    public class Group
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public int Priority { get; set; }

        public virtual ICollection<Voter> Voters { get; set; }
        public virtual ICollection<Choice> Choices { get; set; }
    }
}
