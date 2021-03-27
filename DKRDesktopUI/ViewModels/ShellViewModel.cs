using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace DKRDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        LoginViewModel _loginViewModel;

        public ShellViewModel(LoginViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel;
            ActivateItem(_loginViewModel);
        }
    }
}
