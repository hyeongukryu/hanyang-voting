using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Models
{
    public class Option
    {
        public long Id { get; set; }

        public virtual Choice Choice { get; set; }
        public string Name { get; set; }

        public int Order { get; set; }
    }
}
