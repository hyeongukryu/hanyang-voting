using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.UnityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HanyangVoting.Clients
{
    public class HanyangVotingClientApplication : Application
    {
        public HanyangVotingClientApplication(Bootstrapper bootstrapper)
        {
            _bootstrapper = bootstrapper;
        }

        private readonly Bootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if (DEBUG)
            RunInDebugMode(_bootstrapper);
#else
            RunInReleaseMode(_bootstrapper);
#endif
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private static void RunInDebugMode(Bootstrapper bootstrapper)
        {
            bootstrapper.Run();
        }

        private static void RunInReleaseMode(Bootstrapper bootstrapper)
        {
            bootstrapper.Run();
        }
    }
}
