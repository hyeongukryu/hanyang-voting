using HanyangVoting.Clients.ServiceInterfaces;
using HanyangVoting.Clients.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
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
    /// SplashView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SplashView : UserControl
    {
        public SplashView()
        {
            InitializeComponent();

            this.DataContext = ServiceLocator.Current.GetInstance<SplashViewModel>();
            this.MouseDoubleClick += SplashView_MouseDoubleClick;
        }

        void SplashView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (this.DataContext as SplashViewModel).Launch.Execute();
        }
    }
}
