using HanyangVoting.Clients.ServiceInterfaces;
using HanyangVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.ServiceImplementations
{
    class DatabaseStationService : IStationService
    {
        public Event GetCurrentEvent()
        {
            using (var context = new HanyangVotingContext())
            {
                return context.Events.Single();
            }
        }

        public Voter GetVoterByNumber(Event evnt, string number)
        {
            using (var context = new HanyangVotingContext())
            {
                return (from voter in context.Voters
                        where voter.Number == number
                        where voter.Event == evnt
                        select voter).Single();
            }
        }

        public IEnumerable<Choice> GetChoices(Voter voter)
        {
            using (var context = new HanyangVotingContext())
            {
                return (from choice in context.Choices
                        from g in voter.Groups
                        where choice.Group == g
                        select choice);
            }
        }

        public Signature Sign(Voter voter, Choice choice, byte[] strokes)
        {
            using (var context = new HanyangVotingContext())
            {
                var signature = new Signature
                {
                    Choice = choice,
                    Voter = voter,
                    Strokes = strokes
                };

                return context.Signatures.Add(signature);
            }
        }

        public IEnumerable<Signature> GetSignatures(Voter voter)
        {
            using (var context = new HanyangVotingContext())
            {
                return from s in context.Signatures
                       where s.Voter == voter
                       orderby s.Choice.Priority
                       select s;
            }
        }

        public int Register(Voter voter, Ticket ticket)
        {
            using (var context = new HanyangVotingContext())
            {
                if (voter.Registerd)
                {
                    throw new ArgumentException();
                }

                var rights = from s in GetSignatures(voter)
                             let choice = s.Choice
                             select new Right
                             {
                                  Choice = s.Choice,
                                  Expired = false,
                                  Ticket = ticket
                             };

                foreach (var right in rights)
                {
                    context.Rights.Add(right);
                }

                voter.Registerd = true;
                context.SaveChanges();

                return rights.Count();
            }
        }
    }
}
