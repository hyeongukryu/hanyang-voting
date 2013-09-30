using HanyangVoting.Clients.ViewModels;
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
    /// VoterSearchView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class VoterSearchView : UserControl
    {
        public VoterSearchView()
        {
            InitializeComponent();

            this.DataContext = ServiceLocator.Current.GetInstance<VoterSearchViewModel>();

            textBoxNumberInput.TextChanged += textBoxNumberInput_TextChanged;
        }

        void textBoxNumberInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            var viewModel = this.DataContext as VoterSearchViewModel;
            viewModel.SearchCommand.Execute(textBoxNumberInput.Text);
        }
    }
}
