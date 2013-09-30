using HanyangVoting.Clients.Models;
using HanyangVoting.Clients.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HanyangVoting.Clients.Views
{
    /// <summary>
    /// RegisterCompleteView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RegisterCompleteView : UserControl
    {
        public RegisterCompleteView()
        {
            InitializeComponent();

            ServiceLocator.Current.GetInstance<RegisterCompleteViewModel>();

            new Action(() =>
                {
                    Thread.Sleep(2000);

                    Dispatcher.Invoke(() =>
                        {
                            var context = ServiceLocator.Current.GetInstance<StationContext>();
                            context.Voter = null;

                            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                            regionManager.RequestNavigate(RegionNames.MainRegion, "VoterSearchView");
                        });

                }).BeginInvoke(null, null);
        }
    }
}
