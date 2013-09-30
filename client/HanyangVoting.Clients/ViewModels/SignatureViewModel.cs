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
    class SignatureViewModel : ViewModel
    {
        private readonly IStationService _stationService;
        private readonly IRegionManager _regionManager;
        private readonly StationContext _stationContext;

        public DelegateCommand OkCommand { get; set; }

        public SignatureViewModel(IStationService stationService, IRegionManager regionManager, StationContext stationContext)
        {
            _stationService = stationService;
            _regionManager = regionManager;
            _stationContext = stationContext;

            OkCommand = new DelegateCommand(Ok);
        }

        private void Ok()
        {
            var choices = _stationService.GetChoices(_stationContext.Voter).ToArray();
            _stationService.Sign(_stationContext.Voter, choices[0], new byte[] { 42 });
            _stationService.Sign(_stationContext.Voter, choices[2], new byte[] { 42 });

            _regionManager.RequestNavigate(RegionNames.MainRegion, "CodeReaderView");
        }

        protected override void Selected()
        {
        }

        protected override void Unselected()
        {
        }
    }
}
