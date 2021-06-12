using Caliburn.Micro;
using DKRDesktopUI.EventModels;
using DKRDesktopUI.Library.Api;
using DKRDesktopUI.Library.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DKRDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private readonly IAPIHelper _apiHelper;
        private readonly IEventAggregator _events;
        private readonly SalesViewModel _salesViewModel;
        private readonly ILoggedInUserModel _user;

        public ShellViewModel(IEventAggregator events, SalesViewModel salesViewModel, ILoggedInUserModel user, IAPIHelper apiHelper)
        {
            _events = events;
            _salesViewModel = salesViewModel;
            _user = user;
            _apiHelper = apiHelper;
            _events.SubscribeOnPublishedThread(this);

            ActivateItemAsync(IoC.Get<LoginViewModel>());
        }

        public bool IsLoggedIn => !string.IsNullOrWhiteSpace(_user.Token);

        public void ExitApplication() => TryCloseAsync();

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_salesViewModel, cancellationToken);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public async Task LogOutAsync()
        {
            _user.ResetUserModel();
            _apiHelper.LogOffUser();
            await ActivateItemAsync(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public async Task UserManagementAsync() => await ActivateItemAsync(IoC.Get<UserDisplayViewModel>());
    }
}