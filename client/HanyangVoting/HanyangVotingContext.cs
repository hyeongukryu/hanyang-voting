using HanyangVoting.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting
{
    public class HanyangVotingContext : DbContext
    {
        public HanyangVotingContext()
            : base("HanyangVotingCompactDatabase")
        {
        }

        public DbSet<Ballot> Ballots { get; set; }
        public DbSet<Booth> Booths { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Signature> Signatures { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Right> Rights { get; set; }
    }
}
