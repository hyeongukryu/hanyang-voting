using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanyangVoting.Clients
{
    abstract class ViewModel : NotificationObject, INavigationAware, IRegionMemberLifetime
    {
        private readonly SubscriptionToken _subscriptionToken;
        private bool _selected = false;

        protected abstract void Selected();
        protected abstract void Unselected();

        public ViewModel()
        {
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            var nagivationCompletedEvent = eventAggregator.GetEvent<NavigationCompletedEvent>();

            _subscriptionToken = nagivationCompletedEvent.Subscribe(OnNavigationCompleted, ThreadOption.PublisherThread);
        }

        private void OnNavigationCompleted(ViewModel publisher)
        {
            if (publisher != this)
            {
                var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
                var navigationCompletedEvent = eventAggregator.GetEvent<NavigationCompletedEvent>();
                navigationCompletedEvent.Unsubscribe(_subscriptionToken);
                Unselected();
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            var navigationCompletedEvent = eventAggregator.GetEvent<NavigationCompletedEvent>();
            navigationCompletedEvent.Publish(this);

            if (_selected == false)
            {
                _selected = true;
                Selected();
            }
        }

        public bool KeepAlive
        {
            get { return false; }
        }
    }
}
