using HanyangVoting.Clients.ServiceInterfaces;
using HanyangVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.ServiceImplementations
{
    class DatabaseBoothService : IBoothService
    {
        public IEnumerable<Right> GetRights(Ticket ticket)
        {
            using (var context = new HanyangVotingContext())
            {
                return from right in context.Rights
                       where right.Ticket == ticket
                       orderby right.Choice.Priority ascending
                       select right;
            }
        }

        public IEnumerable<Option> GetOptions(Choice choice)
        {
            using (var context = new HanyangVotingContext())
            {
                return from option in context.Options
                       where option.Choice == choice
                       orderby option.Order
                       select option;
            }
        }

        public Ballot Vote(Booth booth, Right right, Option answer)
        {
            using (var context = new HanyangVotingContext())
            {
                var ballot = new Ballot
                {
                    Booth = booth,
                    Question = right.Choice,
                    Answer = answer
                };

                ballot = context.Ballots.Add(ballot);
                
                right.Expired = true;

                context.SaveChanges();

                return ballot;
            }
        }
    }
}
