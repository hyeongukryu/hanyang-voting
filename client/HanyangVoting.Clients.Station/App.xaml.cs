using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HanyangVoting.Clients.Station
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : HanyangVotingClientApplication
    {
        public App()
            : base(new HanyangVotingClientBootstrapper(ClientTypes.Station))
        {
        }
    }
}
