﻿using DevOpsManager.MobileApp.Models;
using DevOpsManager.MobileApp.Services;
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
        private readonly DevOpsService _devOpsService;
        private readonly PersistantState _persistantState;
        private readonly ILiteDatabase _db;

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

        public AccountsViewModel(DevOpsService devOpsService, PersistantState persistantState, ILiteDatabase db)
        {
            _db = db;
            var collection = _db.GetCollection<Account>();
            Accounts = new ObservableCollection<Account>(collection.FindAll());
            _devOpsService = devOpsService;
            _persistantState = persistantState;
        }

        public override Task InitAsync()
        {
            if (!string.IsNullOrEmpty(_persistantState.Organization))
            {
                var foundAccount = Accounts.SingleOrDefault(a => a.Name == _persistantState.Organization);
                return GoToAccountAsync(foundAccount);
            }
            return base.InitAsync();
        }

        public async Task AddAsync()
        {
            string name = await DisplayPromptAsync("Account name", "Provide the name of your account");
            if (name == null) return;
            var account = new Account
            {
                Name = name.Trim(),
            };
            var collection = _db.GetCollection<Account>();
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
            _devOpsService.Authroize(await SecureStorage.GetAsync(account.Key));
            _persistantState.Organization = account.Name;
            await NavigateAsync<ProjectsViewModel>();
        }

        public async Task SetKeyAsync(Account account)
        {
            string keyToStore = await DisplayPromptAsync("Enter Key", "Enter your key here");
            if (keyToStore == null) return;
            if (account.Key != null)
                SecureStorage.Remove(account.Key);
            account.Key = Guid.NewGuid().ToString("N");
            await SecureStorage.SetAsync(account.Key, keyToStore);
            var collection = _db.GetCollection<Account>();
            collection.Update(account);
        }

        public void Delete(string name)
        {
            var collection = _db.GetCollection<Account>();
            collection.Delete(name);
            Accounts.Remove(Accounts.FirstOrDefault(a => a.Name == name));
        }

    }
}
