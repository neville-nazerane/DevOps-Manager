using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DevOpsManager.MobileApp.ViewModels
{
    public class MainViewModel : BindableObject
    {
        private string _title = "Hello world";

        public string Title
        {
            get => _title; 
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }
}
