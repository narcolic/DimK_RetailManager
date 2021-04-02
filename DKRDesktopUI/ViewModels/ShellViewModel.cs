using Caliburn.Micro;
using DKRDesktopUI.EventModels;

namespace DKRDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private readonly IEventAggregator _events;
        private SalesViewModel _salesViewModel;
        private readonly SimpleContainer _container;

        public ShellViewModel(IEventAggregator events, SalesViewModel salesViewModel,
            SimpleContainer container)
        {
            _events = events;
            _salesViewModel = salesViewModel;
            _container = container;

            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesViewModel);
        }
    }
}