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
    class VoterSearchViewModel : ViewModel
    {
        private readonly IStationService _stationService;
        private readonly IRegionManager _regionManager;
        private readonly StationContext _stationContext;

        public string Title { get; set; }

        public string NumberInput { get; set; }

        public string Number { get; set; }
        public string Name { get; set; }
        public string GroupList { get; set; }
        public string Status { get; set; }

        public DelegateCommand SelectCommand { get; set; }
        public DelegateCommand<string> SearchCommand { get; set; }


        public VoterSearchViewModel(IStationService stationService, IRegionManager regionManager, StationContext stationContext)
        {
            _stationService = stationService;
            _regionManager = regionManager;
            _stationContext = stationContext;

            Title = _stationContext.Station.Name + " 선거인 검색";

            SelectCommand = new DelegateCommand(OnSelect);
            SearchCommand = new DelegateCommand<string>(OnSearch);
        }

        private void OnSelect()
        {
            _regionManager.RequestNavigate(RegionNames.MainRegion, "SignatureView");
        }

        private void OnSearch(string numberInput)
        {
            Number = "";
            Name = "";
            GroupList = "";
            Status = "";
            _stationContext.Voter = null;

            try
            {
                var voter = _stationService.GetVoterByNumber(_stationContext.Event, numberInput);
                _stationContext.Voter = voter;
                
                Number = voter.Number;
                Name = voter.Name;
                Status = voter.Registerd ? "등록을 마쳤습니다." : "등록하지 않았습니다.";

                var groups = from g in _stationService.GetVoterGroups(voter)
                             orderby g.Priority ascending
                             select g.Name;

                GroupList = "";
                foreach (var g in groups)
                {
                    GroupList += " " + g;
                }
                GroupList = GroupList.Substring(1);
            }
            catch
            {
            }

            RaisePropertyChanged(() => Number);
            RaisePropertyChanged(() => Name);
            RaisePropertyChanged(() => Status);
            RaisePropertyChanged(() => GroupList);
        }

        protected override void Selected()
        {
        }

        protected override void Unselected()
        {
        }
    }
}
