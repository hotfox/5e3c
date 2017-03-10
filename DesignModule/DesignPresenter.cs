using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Activities;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using Prism.Mvvm;
using Prism.Events;
using Teflon.SDK.Events;
using Teflon.SDK.Models;
using System.Diagnostics;
using Prism.Interactivity.InteractionRequest;
using System.Windows.Input;
using Prism.Commands;

namespace Teflon.Modules
{
    [Export(typeof(DesignPresenter))]
    public class DesignPresenter:BindableBase
    {

        public string ProductName { get; private set; }
        public string StationName { get; private set; }
        public InteractionRequest<INotification>
            NewTestItemRequest
        { get; private set; } = new InteractionRequest<INotification>();
        [Import(typeof(IDesigntime))]
        public IDesigntime Designtime { get; private set; }

        public ICommand SaveCommand { get; private set; }

        [ImportingConstructor]
        public DesignPresenter(IEventAggregator eventAggregator):base()
        {
            this.eventAggregator = eventAggregator;
            NewTestItemEvent newTestItemEvent = eventAggregator.GetEvent<NewTestItemEvent>();
            newTestItemToken = newTestItemEvent.Subscribe(NewTestItemEventHandler, ThreadOption.UIThread, false);

            SaveCommand = new DelegateCommand(OnSaveCommand);

        }

        public void NewTestItemEventHandler(TestItem newTestItem)
        {
            NewTestItemRequest.Raise(new Notification { Content = StationName,Title= ProductName });
        }

        public void RunTestItemEventHandler(TestItem newTestItem)
        {
            Debug.WriteLine("in");
        }

        private void OnSaveCommand()
        {
            
        }

        private IEventAggregator eventAggregator;
        private SubscriptionToken newTestItemToken;
    }
}
