using DevOpsManager.MobileApp.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.FluentInjector;
using Xamarin.Forms;
using static DevOpsManager.MobileApp.Services.Helpers.Constants;

namespace DevOpsManager.MobileApp.ViewModels
{
    public class AccountsViewModel : ViewModelBase
    {
        private ObservableCollection<Account> _accounts;

        public ObservableCollection<Account> Accounts
        {
            get => _accounts; 
            set
            {
                _accounts = value;
                OnPropertyChanged();
            }
        }

        public Command AddCommand => BuildCommand(AddAsync);

        public Command<string> AzureCommand => BuildCommand<string>(GoToAzureAsync);

        public Command<Account> SetKeyCommand => BuildCommand<Account>(SetKeyAsync);

        public Command<string> DeleteCommand => new Command<string>(Delete);

        public Command<Account> GoCommand => BuildCommand<Account>(GoToAccountAsync);

        public AccountsViewModel()
        {
            using var db = new LiteDatabase(DatabaseLocation);
            var collection = db.GetCollection<Account>();
            Accounts = new ObservableCollection<Account>(collection.FindAll());
        }

        public async Task AddAsync()
        {
            string name = await DisplayPromptAsync("Account name", "Provide the name of your account");
            if (name == null) return;
            var account = new Account
            {
                Name = name.Trim(),
            };
            using var db = new LiteDatabase(DatabaseLocation);
            var collection = db.GetCollection<Account>();
            collection.Insert(account);
            Accounts.Add(account);
        }

        public async Task GoToAzureAsync(string name)
        {
            await Launcher.OpenAsync($"https://dev.azure.com/{name}/_usersSettings/tokens");
        }

        public async Task GoToAccountAsync(Account account)
        {
            if (account?.Key == null) return;
            await DisplayAlert("See!", await SecureStorage.GetAsync(account.Key), "OH SHIT!!");
        }

        public async Task SetKeyAsync(Account account)
        {
            string keyToStore = await DisplayPromptAsync("Enter Key", "Ender your key here");
            if (keyToStore == null) return;
            if (account.Key != null)
                SecureStorage.Remove(account.Key);
            account.Key = Guid.NewGuid().ToString("N");
            await SecureStorage.SetAsync(account.Key, keyToStore);
            using var db = new LiteDatabase(DatabaseLocation);
            var collection = db.GetCollection<Account>();
            collection.Update(account);
        }

        public void Delete(string name)
        {
            using var db = new LiteDatabase(DatabaseLocation);
            var collection = db.GetCollection<Account>();
            collection.Delete(name);
            Accounts.Remove(Accounts.Single(a => a.Name == name));
        }

    }
}
