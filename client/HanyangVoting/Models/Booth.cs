using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Models
{
    public class Booth
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public virtual Station Station { get; set; }
    }
}
