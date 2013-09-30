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
    /// BallotView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BallotView : UserControl
    {
        public BallotView()
        {
            InitializeComponent();

            this.DataContext = ServiceLocator.Current.GetInstance<BallotViewModel>();
            this.Loaded += BallotView_Loaded;
        }

        private void Add(string option)
        {
            var dockPanel = new DockPanel();
            var button = new Button
            {
                Margin = new Thickness(20),
                Padding = new Thickness(10),
                FontSize = 14,
                FontFamily = new FontFamily("서울남산체 EB"),
                Content = "유효표 선택"
            };

            button.Click += button_Click;
            DockPanel.SetDock(button, Dock.Right);

            var textBlock = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                FontSize = 27,
                Text = option
            };

            dockPanel.Children.Add(button);
            dockPanel.Children.Add(textBlock);

            stackPanelOptions.Children.Add(dockPanel);
        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as BallotViewModel;
            viewModel.VoteValid.Execute(0);
        }

        void BallotView_Loaded(object sender, RoutedEventArgs e)
        {
            stackPanelOptions.Children.Clear();

            var viewModel = this.DataContext as BallotViewModel;

            if (viewModel.End)
            {
                ServiceLocator.Current.GetInstance<IRegionManager>().RequestNavigate(RegionNames.MainRegion, "EndView");
            }

            if (viewModel.Options != null)
            {
                foreach (var option in viewModel.Options)
                {
                    Add(option);
                }
            }
        }
    }
}
