using HanyangVoting.Clients.ServiceInterfaces;
using HanyangVoting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.ServiceImplementations
{
    class PrototypeDatabaseTester : IDatabaseTester
    {
        public void Clear()
        {
            using (var context = new HanyangVotingContext())
            {
                context.Database.Delete();
                context.Database.CreateIfNotExists();



                var e = new Event
                {
                    Name = "전체 학생 대표자 회의 시연 투표"
                };
                context.Events.Add(e);



                var station1 = new Station
                {
                    Name = "제1투표소",
                    Event = e
                };

                var station2 = new Station
                {
                    Name = "제2투표소",
                    Event = e
                };

                context.Stations.Add(station1);
                context.Stations.Add(station2);






                var ticket1 = new Ticket
                {
                    Key = "CommissionTicketOne",
                    Name = "제1투표소 선거관리위원회",
                    Station = station1,
                    Commission = true
                };
                context.Tickets.Add(ticket1);
                var ticket2 = new Ticket
                {
                    Key = "CommissionTicketTwo",
                    Name = "제2투표소 선거관리위원회",
                    Station = station2,
                    Commission = true
                };
                context.Tickets.Add(ticket2);




                var ticket101 = new Ticket
                {
                    Key = "VotingTicket101",
                    Name = "제1투표소 제1투표권",
                    Station = station1,
                    Commission = false
                };
                var ticket102 = new Ticket
                {
                    Key = "VotingTicket102",
                    Name = "제1투표소 제2투표권",
                    Station = station1,
                    Commission = false
                };
                var ticket103 = new Ticket
                {
                    Key = "VotingTicket103",
                    Name = "제1투표소 제3투표권",
                    Station = station1,
                    Commission = false
                };
                context.Tickets.Add(ticket101);
                context.Tickets.Add(ticket102);
                context.Tickets.Add(ticket103);
                var ticket201 = new Ticket
                {
                    Key = "VotingTicket201",
                    Name = "제2투표소 제1투표권",
                    Station = station2,
                    Commission = false
                };
                var ticket202 = new Ticket
                {
                    Key = "VotingTicket202",
                    Name = "제2투표소 제2투표권",
                    Station = station2,
                    Commission = false
                };
                var ticket203 = new Ticket
                {
                    Key = "VotingTicket203",
                    Name = "제2투표소 제3투표권",
                    Station = station2,
                    Commission = false
                };
                context.Tickets.Add(ticket201);
                context.Tickets.Add(ticket202);
                context.Tickets.Add(ticket203);






                context.Booths.Add(new Booth
                {
                    Name = "제1투표소제1기표소",
                    Station = station1
                });

                context.Booths.Add(new Booth
                {
                    Name = "제1투표소제2기표소",
                    Station = station1
                });

                context.Booths.Add(new Booth
                {
                    Name = "제2투표소제1기표소",
                    Station = station2
                });

                var choice100 = new Choice
                {
                    Name = "총학생회 선거 투표",
                    Event = e,
                    Priority = 100
                };

                context.Options.Add(new Option
                {
                    Choice = choice100,
                    Name = "총학생회 후보 하나",
                    Order = 100
                });
                context.Options.Add(new Option
                {
                    Choice = choice100,
                    Name = "총학생회 후보 둘",
                    Order = 200
                });
                context.Options.Add(new Option
                {
                    Choice = choice100,
                    Name = "총학생회 후보 셋",
                    Order = 300
                });

                var choice200 = new Choice
                {
                    Name = "제1대학 학생회 선거 투표",
                    Event = e,
                    Priority = 200
                };
                context.Options.Add(new Option
                {
                    Choice = choice200,
                    Name = "제1대학 학생회 후보 하나",
                    Order = 100
                });
                context.Options.Add(new Option
                {
                    Choice = choice200,
                    Name = "제1대학 학생회 후보 둘",
                    Order = 200
                });

                var choice3001 = new Choice
                {
                    Name = "제1대학 제1전공 대표 선거 투표",
                    Event = e,
                    Priority = 300
                };
                context.Options.Add(new Option
                {
                    Choice = choice3001,
                    Name = "제1대학 제1전공 대표 후보 하나",
                    Order = 100
                });
                context.Options.Add(new Option
                {
                    Choice = choice3001,
                    Name = "제1대학 제1전공 대표 후보 둘",
                    Order = 200
                });

                var choice3002 = new Choice
                {
                    Name = "제2대학 제1전공 대표 선거 투표",
                    Event = e,
                    Priority = 300
                };
                context.Options.Add(new Option
                {
                    Choice = choice3002,
                    Name = "제2대학 제1전공 대표 후보 하나",
                    Order = 100
                });
                context.Options.Add(new Option
                {
                    Choice = choice3002,
                    Name = "제2대학 제1전공 대표 후보 둘",
                    Order = 200
                });




                var group100 = new Group
                {
                    Name = "학부",
                    Choices = new[] { choice100 },
                    Priority = 100
                };

                var group200 = new Group
                {
                    Name = "제1대학",
                    Choices = new[] { choice200 },
                    Priority = 200
                };

                var group201 = new Group
                {
                    Name = "제2대학",
                    Priority = 200
                };

                var group301 = new Group
                {
                    Name = "제1대학 제1전공",
                    Priority = 300,
                    Choices = new[] { choice3001 }
                };

                var group302 = new Group
                {
                    Name = "제1대학 제2전공",
                    Priority = 300,
                };

                var group401 = new Group
                {
                    Name = "제2대학 제1전공",
                    Priority = 300,
                    Choices = new[] { choice3002 }
                };
                
                context.Voters.Add(new Voter
                {
                    Name = "한양인1",
                    Number = "1939000001",
                    Event = e,
                    Groups = new [] { group100, group200, group301 }
                });
                context.Voters.Add(new Voter
                {
                    Name = "한양인2",
                    Number = "1939000002",
                    Event = e,
                    Groups = new[] { group100, group200, group302 }
                });
                context.Voters.Add(new Voter
                {
                    Name = "한양인3",
                    Number = "1939000003",
                    Event = e,
                    Groups = new[] { group100, group201, group401 }
                });


                context.SaveChanges();
            }
        }
    }
}
