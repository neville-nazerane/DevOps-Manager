using DevOpsManager.MobileApp.Models;
using DevOpsManager.MobileApp.Services.Helpers;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;
using static DevOpsManager.MobileApp.Services.Helpers.Constants;

namespace DevOpsManager.MobileApp.Services
{
    public class AccountService
    {

        //public IEnumerable<Account> Get()
        //{
        //    using var db = new LiteDatabase(DbLocation);
        //    var collection = db.GetCollection<Account>(accountCollectionName);
        //    return collection.FindAll();
        //}

        //public void Add(Account account)
        //{
        //    using var coll = _localDataAccess.GetAccountCollection();
        //    coll.Collection.Insert(account);
        //    coll.Collection.EnsureIndex(c => c.Name);
        //}

        //public void Remove(string name)
        //{
        //    using var coll = _localDataAccess.GetAccountCollection();
        //    coll.Collection.Delete(name);
        //}

    }
}
