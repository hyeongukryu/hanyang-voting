using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HanyangVoting.Clients
{
    public class HanyangVotingClientBootstrapper : UnityBootstrapper
    {
        public HanyangVotingClientBootstrapper(ClientTypes clientType)
        {
            _clientType = clientType;
        }

        private readonly ClientTypes _clientType;

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterInstance(typeof(ClientTypes), null, _clientType,
                new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(Shell));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
                new Uri("ModuleCatalog.xaml", UriKind.Relative));
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mappings = base.ConfigureRegionAdapterMappings();
            if (mappings == null) return null;

            return mappings;
        }

        protected override DependencyObject CreateShell()
        {
            var view = Container.TryResolve<Shell>();
            view.Show();
            return view;
        }
    }
}
