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
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;

namespace DevOpsManager.MobileApp
{
    public partial class App : Application
    {
        public App()
        {

            CheckAndSetupAppCenter();

            InitializeComponent();

            DbInit();

            this.StartInjecting()
                    .SetViewModelAssembly(typeof(MainViewModel).Assembly)
                    .OverrideAsyncNavigate(OnNavigateAsync)
                    .AddSingleton(new HttpClient {
                        BaseAddress = new Uri("https://dev.azure.com")
                    })
                    .AddSingleton<PersistantState>()
                    .AddSingleton<DevOpsService>()
                    .AddSingleton<FavoriteService>()
                    .Build();

        }

        private void DbInit()
        {
            var mapper = BsonMapper.Global;

            mapper.Entity<Account>().Id(a => a.Name);
            mapper.Entity<Favorite>().Id(a => a.Id);
        }

        private async Task OnNavigateAsync(Page page)
        {
            MainPage = new NavigationPage(page);
            if (page.BindingContext is ViewModelBase vm)
            {
                vm.IsLoading = true;
                await vm.InitAsync();
                vm.IsLoading = false;
            }
        }

        private void CheckAndSetupAppCenter()
        {
            if (!string.IsNullOrEmpty(Config.AppCenterConfig))
            {
                AppCenter.Start(Config.AppCenterConfig,
                   typeof(Analytics), typeof(Crashes));
            }
        }

        protected async override void OnStart()
        {
            await InjectionControl.NavigateAsync<AccountsViewModel>();

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
