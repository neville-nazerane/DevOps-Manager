using DevOpsManager.MobileApp.Models;
using DevOpsManager.MobileApp.Services.Helpers;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevOpsManager.MobileApp.Services
{
    public class AccountService
    {

        const string collectionName = "account";
        private readonly LocalDataAccess _localDataAccess;

        public AccountService(LocalDataAccess localDataAccess)
        {
            _localDataAccess = localDataAccess;
        }

        public IEnumerable<Account> Get()
        {
            using var collection = _localDataAccess.GetAccountCollection();
            return collection.Collection.FindAll();
        }

        public void Add(Account account)
        {
            using var coll = _localDataAccess.GetAccountCollection();
            coll.Collection.Insert(account);
            coll.Collection.EnsureIndex(c => c.Name);
        }

        public void Remove(string name)
        {
            using var coll = _localDataAccess.GetAccountCollection();
            coll.Collection.Delete(name);
        }

    }
}
