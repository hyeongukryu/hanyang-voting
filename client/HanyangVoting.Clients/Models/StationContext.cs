using HanyangVoting.Models;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.Models
{
    class StationContext
    {
        public Event Event { get; set; }
        public Station Station { get; set; }
        public Voter Voter { get; set; }
        public int Rights { get; set; }
    }
}
