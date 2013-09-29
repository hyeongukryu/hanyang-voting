using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Models
{
    public class Event
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public bool Activated { get; set; }

        public DateTime? OpeningPlanned { get; set; }
        public DateTime? ClosingPlanned { get; set; }

        public DateTime? OpeningActual { get; set; }
        public DateTime? ClosingActual { get; set; }

        public virtual ICollection<Choice> Choices { get; set; }
        public virtual ICollection<Station> Stations { get; set; }
        public virtual ICollection<Voter> Voters { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
