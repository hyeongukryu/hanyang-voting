using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Models
{
    public class Choice
    {
        public long Id { get; set; }

        public virtual Event Event { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<Option> Options { get; set; }
    }
}
