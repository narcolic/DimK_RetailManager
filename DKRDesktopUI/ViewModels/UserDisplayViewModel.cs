using AutoMapper;
using Caliburn.Micro;
using DKRDesktopUI.Library.Api;
using DKRDesktopUI.Library.Models;
using System;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
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
        private BindingList<string> _availableRoles = new BindingList<string>();
        private string _selectedAvailableRole;
        private UserModel _selectedUser;
        private string _selectedUserName;
        private string _selectedUserRole;
        private BindingList<string> _userRoles = new BindingList<string>();
        private BindingList<UserModel> _users;

        public UserDisplayViewModel(StatusInfoViewModel status, IWindowManager window, IUserEndpoint userEndpoint, IMapper mapper)
        {
            _status = status;
            _window = window;
            _userEndpoint = userEndpoint;
            _mapper = mapper;
        }

        public BindingList<string> AvailableRoles
        {
            get { return _availableRoles; }
            set
            {
                _availableRoles = value;
                NotifyOfPropertyChange(() => AvailableRoles);
            }
        }

        public string SelectedAvailableRole
        {
            get { return _selectedAvailableRole; }
            set
            {
                _selectedAvailableRole = value;
                NotifyOfPropertyChange(() => SelectedAvailableRole);
            }
        }

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                SelectedUserName = value.Email;
                UserRoles = new BindingList<string>(value.Roles.Select(r => r.Value).ToList());
                LoadRolesAsync();
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        public string SelectedUserName
        {
            get { return _selectedUserName; }
            set
            {
                _selectedUserName = value;
                NotifyOfPropertyChange(() => SelectedUserName);
            }
        }

        public string SelectedUserRole
        {
            get { return _selectedUserRole; }
            set
            {
                _selectedUserRole = value;
                NotifyOfPropertyChange(() => SelectedUserRole);
            }
        }

        public BindingList<string> UserRoles
        {
            get { return _userRoles; }
            set
            {
                _userRoles = value;
                NotifyOfPropertyChange(() => UserRoles);
            }
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

        public async Task AddSelectedRoleAsync()
        {
            await _userEndpoint.AddUserToRoleAsync(SelectedUser.Id, SelectedAvailableRole);

            UserRoles.Add(SelectedAvailableRole);
            AvailableRoles.Remove(SelectedAvailableRole);
        }

        public async Task RemoveSelectedRoleAsync()
        {
            await _userEndpoint.RemoveUserFromRoleAsync(SelectedUser.Id, SelectedUserRole);

            AvailableRoles.Add(SelectedUserRole);
            UserRoles.Remove(SelectedUserRole);
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
                await _window.ShowDialogAsync(_status, null, settings);

                await TryCloseAsync();
            }
        }

        private async Task LoadRolesAsync()
        {
            var roles = await _userEndpoint.GetAllRolesAsync();

            foreach (var role in roles)
            {
                if (UserRoles.IndexOf(role.Value) < 0)
                {
                    AvailableRoles.Add(role.Value);
                }
            }
        }

        private async Task LoadUsersAsync() => Users = new BindingList<UserModel>(await _userEndpoint.GetAllAsync());
    }
}