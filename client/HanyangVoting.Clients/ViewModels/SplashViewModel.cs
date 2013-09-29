using HanyangVoting.Clients.ServiceInterfaces;
using HanyangVoting.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HanyangVoting.Clients.ViewModels
{
    class SplashViewModel : NotificationObject
    {
        public string Hash { get; set; }
        public string ClientType { get; set; }

        private readonly ClientTypes _clientType;
        private readonly IUnityContainer _container;

        public DelegateCommand Launch { get; private set; }

        public SplashViewModel(
            IBinaryHashComputer binaryHashComputer,
            ClientTypes clientType,
            IUnityContainer container
            )
        {
            _clientType = clientType;
            _container = container;

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

        }

        void LaunchStation()
        {

        }
    }
}
