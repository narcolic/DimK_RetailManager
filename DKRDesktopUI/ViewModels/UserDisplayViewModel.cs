using AutoMapper;
using Caliburn.Micro;
using DKRDesktopUI.Library.Api;
using DKRDesktopUI.Library.Models;
using System;
using System.ComponentModel;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows;

namespace DKRDesktopUI.ViewModels
{
    public class UserDisplayViewModel : Screen
    {
        private readonly IMapper _mapper;
        private readonly StatusInfoViewModel _status;
        private readonly IUserEndpoint _userEndpoint;
        private readonly IWindowManager _window;
        private BindingList<UserModel> _users;

        public UserDisplayViewModel(StatusInfoViewModel status, IWindowManager window, IUserEndpoint userEndpoint, IMapper mapper)
        {
            _status = status;
            _window = window;
            _userEndpoint = userEndpoint;
            _mapper = mapper;
        }

        public BindingList<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadUsersAsync();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    _status.UpdateMessage("Unauthorized Access", "You dont have permission to interact with sales form");
                }
                else
                {
                    _status.UpdateMessage("Fatal Excepetion", ex.Message);
                }
                _window.ShowDialog(_status, null, settings);

                TryClose();
            }
        }

        private async Task LoadUsersAsync() => Users = new BindingList<UserModel>(await _userEndpoint.GetAllAsync());
    }
}