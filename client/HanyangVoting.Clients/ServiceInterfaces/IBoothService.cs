using HanyangVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.ServiceInterfaces
{
    interface IBoothService
    {
        Booth GetBooth(string code);
        Ticket GetTicket(string code);
        string GetChoiceTitle(Choice choice);

        IEnumerable<Right> GetRights(Ticket ticket);
        IEnumerable<Choice> GetChoices(Ticket ticket);
        IEnumerable<Option> GetOptions(Choice choice);
        Ballot Vote(Booth booth, long rightId, Option answer);
    }
}
