using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients.ViewModels
{
    class ToBallotViewModel : ViewModel
    {
        protected override void Selected()
        {
            ServiceLocator.Current.GetInstance<IRegionManager>().RequestNavigate(RegionNames.MainRegion, "BallotView");
        }

        protected override void Unselected()
        {
        }
    }
}
