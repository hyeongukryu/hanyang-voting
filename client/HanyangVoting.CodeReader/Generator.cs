using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.CodeReader
{
    public class Generator
    {
        public void PrintTickets(IEnumerable<HanyangVoting.Models.Ticket> tickets)
        {
            XpsPrinter printer = new XpsPrinter();

            foreach (var ticket in tickets)
            {
                printer.NewTicket(ticket);
            }

            printer.NewPage();

            printer.Print();
        }
    }
}
