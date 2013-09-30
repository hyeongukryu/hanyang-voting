using HanyangVoting.Clients.Models;
using HanyangVoting.Clients.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.ViewModels
{
    class BallotViewModel : ViewModel
    {
        public DelegateCommand VoteInvalid { get; set; }
        public DelegateCommand<int?> VoteValid { get; set; }

        private readonly BoothContext _boothContext;
        private readonly IBoothService _boothService;
        private readonly IRegionManager _regionManager;

        public string Title { get; set; }
        public List<string> Options { get; set; }
        public long RightId { get; set; }
        public long Rights { get; set; }
        public bool End { get; set; }

        public BallotViewModel(BoothContext boothContext, IBoothService boothService, IRegionManager regionManager)
        {
            _boothContext = boothContext;
            _boothService = boothService;
            _regionManager = regionManager;

            VoteInvalid = new DelegateCommand(OnVoteInvalid);
            VoteValid = new DelegateCommand<int?>(OnVoteValid);

            Rights = _boothService.GetRights(_boothContext.Ticket).Count();
            int choices = _boothService.GetChoices(_boothContext.Ticket).Count();
            if (choices == 0)
            {
                End = true;
                return;
            }
            var choice = _boothService.GetChoices(_boothContext.Ticket).First();

            RightId = _boothService.GetRights(_boothContext.Ticket).ToArray()[Rights - choices].Id;

            Options = (from o in _boothService.GetOptions(choice)
                           select o.Name).ToList();
            Title = _boothService.GetChoiceTitle(choice);
        }

        void Vote()
        {
            _boothService.Vote(_boothContext.Booth, RightId, null);
            _regionManager.RequestNavigate(RegionNames.MainRegion, "ToBallotView");
        }

        private void OnVoteInvalid()
        {
            Vote();
        }

        private void OnVoteValid(int? value)
        {
            Vote();
        }

        protected override void Selected()
        {
        }

        protected override void Unselected()
        {
        }
    }
}
