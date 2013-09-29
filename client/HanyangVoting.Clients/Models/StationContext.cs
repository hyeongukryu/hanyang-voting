using HanyangVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.Models
{
    class StationContext
    {
        public Event Event { get; set; }
        public Station Station { get; set; }
        public Voter Voter { get; set; }
        public Ticket Ticket { get; set; }
    }
}
