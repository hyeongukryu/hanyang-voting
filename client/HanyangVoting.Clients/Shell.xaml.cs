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

namespace HanyangVoting.Clients
{
    /// <summary>
    /// Shell.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Shell : Window
    {
        public Shell(ClientTypes clientType)
        {
            InitializeComponent();

            var type = clientType.ToString();
            if (type == "Booth")
            {
                this.Title += " 기표소용 클라이언트";
            }
            else if (type == "Station")
            {
                this.Title += " 투표소용 클라이언트";
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
