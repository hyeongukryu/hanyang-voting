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
        Station GetStation(string code);

        Voter GetVoterByNumber(Event evnt, string number);
        IEnumerable<Group> GetVoterGroups(Voter voter);
        IEnumerable<Choice> GetChoices(Voter voter);
        Signature Sign(Voter voter, Choice choice, byte[] strokes);
        IEnumerable<Signature> GetSignatures(Voter voter);
        Ticket GetTicket(string code);
        int Register(Voter voter, Ticket ticket);
    }
}
