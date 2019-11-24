using DevOpsManager.MobileApp.Helpers;
using DevOpsManager.MobileApp.Services;
using DevOpsManager.MobileApp.ViewModels;
using FreshMvvm;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.FluentInjector;
using DevOpsManager.MobileApp.Pages;

namespace DevOpsManager.MobileApp
{
    public partial class App : Application
    {
        public App()
        {

            string done = this.StartInjecting()
                                .SetViewModelAssembly(typeof(MainViewModel).Assembly)
                                .AddSingleton<HttpClient>()
                                .AddSingleton<DevOpsService>()
                                .Build();   
            InitializeComponent();
            var page = new MainPage();
            page.Text = done;
            MainPage = page;
        }

        protected override void OnStart()
        {
            
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
