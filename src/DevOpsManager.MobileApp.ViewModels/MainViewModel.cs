using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DevOpsManager.MobileApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string Title { get; set; } = "Hello world";

        public Command Move => new Command(() => Navigate<AccountsViewModel>());

    }
}
