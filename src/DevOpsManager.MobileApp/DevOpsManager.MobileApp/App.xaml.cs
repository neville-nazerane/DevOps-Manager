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
                    .AddSingleton<DevOpsService>()
                    .Build();

            InjectionControl.Navigate<AccountsViewModel>();
        }

        private void DbInit()
        {
            var mapper = BsonMapper.Global;

            mapper.Entity<Account>().Id(a => a.Name);
        }

        private async Task OnNavigateAsync(Page page)
        {
            if (page.BindingContext is ViewModelBase vm)
                await vm.InitAsync();
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
