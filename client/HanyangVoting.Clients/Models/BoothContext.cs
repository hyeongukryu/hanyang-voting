using HanyangVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.Models
{
    class BoothContext
    {
        public Booth Booth { get; set; }
        public Ticket Ticket { get; set; }
    }
}
