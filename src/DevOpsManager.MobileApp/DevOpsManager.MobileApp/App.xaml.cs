using DevOpsManager.MobileApp.Services;
using DevOpsManager.MobileApp.ViewModels;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.FluentInjector;
using DevOpsManager.MobileApp.Pages;
using LiteDB;
using DevOpsManager.MobileApp.Models;

namespace DevOpsManager.MobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DbInit();

            this.StartInjecting()
                    .SetViewModelAssembly(typeof(MainViewModel).Assembly)
                    .AddSingleton<AccountService>()
                    .AddSingleton<HttpClient>()
                    .AddSingleton<DevOpsService>()
                    .Build();

            InjectionControl.Navigate<AccountsViewModel>();
        }

        private void DbInit()
        {
            var mapper = BsonMapper.Global;

            mapper.Entity<Account>().Id(a => a.Name);
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
