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
        IEnumerable<Right> GetRights(Ticket ticket);
        IEnumerable<Option> GetOptions(Choice choice);
        Ballot Vote(Booth booth, Right right, Option answer);
    }
}
