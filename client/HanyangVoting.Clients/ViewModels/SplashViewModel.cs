using HanyangVoting.Clients.Models;
using HanyangVoting.Clients.ServiceInterfaces;
using HanyangVoting.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HanyangVoting.Clients.ViewModels
{
    class SplashViewModel : ViewModel
    {
        public string Hash { get; set; }
        public string ClientType { get; set; }

        private readonly ClientTypes _clientType;
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public DelegateCommand Launch { get; private set; }

        public SplashViewModel(
            IBinaryHashComputer binaryHashComputer,
            ClientTypes clientType,
            IUnityContainer container,
            IRegionManager regionManager
            )
        {
            _clientType = clientType;
            _container = container;
            _regionManager = regionManager;

            Hash = binaryHashComputer.ComputeHash();

            ClientType = clientType.ToString();
            if (ClientType == "Booth")
            {
                ClientType = "기표소";
            }
            else if (ClientType == "Station")
            {
                ClientType = "투표소";
            }
            else
            {
                throw new ArgumentException();
            }

            RaisePropertyChanged(() => Hash);
            RaisePropertyChanged(() => ClientType);

            Launch = new DelegateCommand(DoLaunch);
        }

        void DoLaunch()
        {
            if (_clientType == ClientTypes.Booth)
            {
                LaunchBooth();
            }
            else if (_clientType == ClientTypes.Station)
            {
                LaunchStation();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        void LaunchBooth()
        {
            _container.RegisterInstance<BoothContext>(new BoothContext());
            _regionManager.RequestNavigate(RegionNames.MainRegion, "CodeReaderView");
        }

        void LaunchStation()
        {
            var stationContext = new StationContext();
            var stationService = _container.Resolve<IStationService>();
            _container.RegisterInstance<StationContext>(stationContext);
            stationContext.Event = stationService.GetCurrentEvent();
            _regionManager.RequestNavigate(RegionNames.MainRegion, "CodeReaderView");
        }

        void LaunchGenerator()
        {
            using (var context = new HanyangVotingContext())
            {
                new HanyangVoting.CodeReader.Generator().PrintTickets(context.Tickets);
            }
        }

        protected override void Selected()
        {
        }

        protected override void Unselected()
        {
        }
    }
}
