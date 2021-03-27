﻿using Caliburn.Micro;
using DKRDesktopUI.Helpers;
using System;
using System.Threading.Tasks;

namespace DKRDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IAPIHelper _apiHelper;
        private string _errorMessage;
        private string _password;
        private string _userName;

        public LoginViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public bool CanLogin => !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        public bool IsErrorVisible => !string.IsNullOrWhiteSpace(ErrorMessage);

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public async Task LoginAsync()
        {
            try
            {
                ErrorMessage = string.Empty;
                var result = await _apiHelper.AuthenticateAsync(UserName, Password);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}