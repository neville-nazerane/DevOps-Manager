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
using Syncfusion.Licensing;
using System.Threading.Tasks;

namespace DevOpsManager.MobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SyncfusionLicenseProvider.RegisterLicense(Config.Syncfusion);
            DbInit();


            this.StartInjecting()
                    .SetViewModelAssembly(typeof(MainViewModel).Assembly)
                    .OverrideAsyncNavigate(OnNavigateAsync)
                    .AddSingleton(new HttpClient {
                        BaseAddress = new Uri("https://dev.azure.com")
                    })
                    .AddSingleton<PersistantState>()
                    .AddSingleton<DevOpsService>()
                    .Build();

        }

        private void DbInit()
        {
            var mapper = BsonMapper.Global;

            mapper.Entity<Account>().Id(a => a.Name);
        }

        private async Task OnNavigateAsync(Page page)
        {
            MainPage = new NavigationPage(page);
            if (page.BindingContext is ViewModelBase vm)
                await vm.InitAsync();
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
