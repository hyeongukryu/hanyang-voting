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
                return (from right in context.Rights
                        where right.Ticket.Id == ticket.Id
                        orderby right.Choice.Priority ascending
                        select right).ToArray();
            }
        }

        public IEnumerable<Option> GetOptions(Choice choice)
        {
            using (var context = new HanyangVotingContext())
            {
                return (from option in context.Options
                        where option.Choice.Id == choice.Id
                        orderby option.Order
                        select option).ToArray();
            }
        }

        public Ballot Vote(Booth booth, long rightId, Option answer)
        {
            using (var context = new HanyangVotingContext())
            {
                var right = (from r in context.Rights
                             where r.Id == rightId
                             select r).Single();

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

        public Booth GetBooth(string code)
        {
            using (var context = new HanyangVotingContext())
            {
                var q = from t in context.Tickets
                        where t.Commission
                        where t.Key == code
                        select t.Station;

                var station = q.Single();

                return (from b in context.Booths
                        where b.Station.Id == station.Id
                        select b).First();
            }
        }

        public Ticket GetTicket(string code)
        {
            using (var context = new HanyangVotingContext())
            {
                var q = from t in context.Tickets
                        where t.Key == code
                        select t;

                return q.Single();
            }
        }


        public string GetChoiceTitle(Choice choice)
        {
            using (var context = new HanyangVotingContext())
            {
                return (from c in context.Choices
                        where c.Id == choice.Id
                        select c.Name).Single();
            }
        }


        public IEnumerable<Choice> GetChoices(Ticket ticket)
        {
            using (var context = new HanyangVotingContext())
            {
                var rights = from right in context.Rights
                             where right.Ticket.Id == ticket.Id
                             orderby right.Choice.Priority ascending
                             select right;

                return (from r in rights
                        where r.Expired == false
                        select r.Choice).ToArray();
            }
        }
    }
}
