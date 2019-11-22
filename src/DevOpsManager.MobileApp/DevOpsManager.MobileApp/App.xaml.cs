using DevOpsManager.MobileApp.Services;
using DevOpsManager.MobileApp.ViewModels;
using FreshMvvm;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevOpsManager.MobileApp
{
    public partial class App : Application
    {
        public App()
        {

            FreshIOC.Container.Register(new HttpClient { 
                BaseAddress = new Uri("https://dev.azure.com")
            }).AsSingleton();

            FreshIOC.Container.Register<DevOpsService, DevOpsService>().AsSingleton();

            InitializeComponent();

            var basicPage = FreshPageModelResolver.ResolvePageModel<MonViewModel>();
            var basicContainer = new FreshNavigationContainer(basicPage);

          //  var myMaster = new FreshMvvm.FreshMasterDetailNavigationContainer();
            //myMaster.Init("Menu dude");
            //myMaster.AddPage<MainViewModel>("Oh no batman!");
            MainPage = basicContainer;
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
