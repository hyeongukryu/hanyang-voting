using HanyangVoting.Clients.ServiceImplementations;
using HanyangVoting.Clients.ServiceInterfaces;
using HanyangVoting.Clients.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HanyangVoting.Clients
{
    public class ModuleInit : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public ModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            InitializeContainer();
            InitializePersistence();
        }

        private void InitializeContainer()
        {
            _container.RegisterType<IContext, WpfContext>();
            _container.RegisterType<IDatabaseTester, PrototypeDatabaseTester>();
            _container.RegisterType<ICodeReader, CameraCodeReader>();
            _container.RegisterType<IBinaryHashComputer, DefaultBinaryHashComputer>();

            _container.RegisterType<BackgroudControl>();
            _regionManager.RegisterViewWithRegion(RegionNames.BackgroundRegion,
                () => _container.Resolve<BackgroudControl>());

            _container.RegisterType<SplashView>();
            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion,
                () => _container.Resolve<SplashView>());
        }

        private void InitializePersistence()
        {
            SetConnectionString();
            _container.Resolve<IDatabaseTester>().Clear();
        }

        private void SetConnectionString()
        {
            var settings = ConfigurationManager.ConnectionStrings[0];
            var fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            fi.SetValue(settings, false);
            settings.ProviderName = "System.Data.SqlServerCe.4.0";
            settings.Name = "HanyangVotingCompactDatabase";
            settings.ConnectionString = "Data Source=C:/HanyangVoting/HanyangVotingPrototype.sdf";
        }
    }
}
