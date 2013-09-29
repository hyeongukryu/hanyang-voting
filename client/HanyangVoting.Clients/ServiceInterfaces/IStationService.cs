using HanyangVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.ServiceInterfaces
{
    interface IStationService
    {
        Event GetCurrentEvent();
        Voter GetVoterByNumber(Event evnt, string number);
        IEnumerable<Choice> GetChoices(Voter voter);
        Signature Sign(Voter voter, Choice choice, byte[] strokes);
        IEnumerable<Signature> GetSignatures(Voter voter);
        int Register(Voter voter, Ticket ticket);
    }
}
