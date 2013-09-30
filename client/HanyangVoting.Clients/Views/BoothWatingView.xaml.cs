using HanyangVoting.Clients.ViewModels;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// BoothWatingView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BoothWatingView : UserControl
    {
        public BoothWatingView()
        {
            InitializeComponent();

            this.DataContext = ServiceLocator.Current.GetInstance<BoothWatingViewModel>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServiceLocator.Current.GetInstance<IRegionManager>().RequestNavigate(RegionNames.MainRegion, "CodeReaderView");
        }
    }
}
