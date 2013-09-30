using HanyangVoting.Clients.ServiceInterfaces;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using HanyangVoting.CodeReader;
using Microsoft.Practices.Unity;
using HanyangVoting.Clients.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;

namespace HanyangVoting.Clients.ViewModels
{
    class CodeReaderViewModel : ViewModel
    {
        private readonly ICodeReader _codeReader;
        private readonly IContext _context;
        private readonly ClientTypes _clientType;
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public string Code { get; set; }
        public ImageSource Image { get; set; }
        public string Title { get; set; }
        public DelegateCommand OkCommand { get; set; }

        public CodeReaderViewModel(
            ICodeReader codeReader, IContext context, ClientTypes clientType, IUnityContainer container, IRegionManager regionManager)
        {
            _codeReader = codeReader;
            _context = context;
            _clientType = clientType;
            _container = container;
            _regionManager = regionManager;

            _codeReader.NewFrame += _codeReader_NewFrame;
            _codeReader.NewCode += _codeReader_NewCode;

            if (clientType == ClientTypes.Station)
            {
                if (container.Resolve<StationContext>().Station == null)
                {
                    Title = "투표소 선거관리위원회 티켓 입력";
                }
                else
                {
                    Title = "새로운 투표 티켓 입력";
                }
            }
            else if (clientType == ClientTypes.Booth)
            {
                if (container.Resolve<BoothContext>().Booth == null)
                {
                    Title = "기표소 선거관리위원회 티켓 입력";
                }
                else
                {
                    Title = "투표 티켓 입력";
                }
            }

            OkCommand = new DelegateCommand(Ok);
        }

        void _codeReader_NewCode(string obj)
        {
            Code = obj;
            RaisePropertyChanged(() => Code);
        }

        void _codeReader_NewFrame(System.Drawing.Bitmap obj)
        {
            _context.Invoke(() =>
                {
                    Image = obj.ToBitmapImage();
                });
            RaisePropertyChanged(() => Image);
        }

        protected override void Selected()
        {
            _codeReader.Start();
        }

        protected override void Unselected()
        {
            _codeReader.Stop();
        }

        private void Ok()
        {
            if (_clientType == ClientTypes.Station)
            {
                var stationContext = _container.Resolve<StationContext>();
                var stationService = _container.Resolve<IStationService>();
                if (stationContext.Station == null)
                {
                    stationContext.Station = stationService.GetStation(Code);
                    _regionManager.RequestNavigate(RegionNames.MainRegion, "VoterSearchView");
                }
                else
                {
                    int rights = stationService.Register(stationContext.Voter, stationService.GetTicket(Code));
                    stationContext.Rights = rights;
                    _regionManager.RequestNavigate(RegionNames.MainRegion, "RegisterCompleteView");
                }
            }
            else if (_clientType == ClientTypes.Booth)
            {
                var boothContext = _container.Resolve<BoothContext>();
                var boothService = _container.Resolve<IBoothService>();

                if (boothContext.Booth == null)
                {
                    boothContext.Booth = boothService.GetBooth(Code);
                    _regionManager.RequestNavigate(RegionNames.MainRegion, "BoothWatingView");
                }
                else
                {
                    boothContext.Ticket = boothService.GetTicket(Code);
                    _regionManager.RequestNavigate(RegionNames.MainRegion, "BallotView");
                }
            }
        }
    }
}
