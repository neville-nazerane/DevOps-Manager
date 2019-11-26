using DevOpsManager.MobileApp.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace DevOpsManager.MobileApp.Services
{
    public class LocalDataAccess
    {

        public LiteDatabase GetDatabase() => new LiteDatabase(Path.Combine(FileSystem.AppDataDirectory, "store.db"));

        public CollectionContext<Account> GetAccountCollection() => new CollectionContext<Account>(GetDatabase(), "account");
        
        public class CollectionContext<T> : IDisposable
        {
            private readonly LiteDatabase _db;

            public LiteCollection<T> Collection { get; }

            public CollectionContext(LiteDatabase db, string collectionName)
            {
                _db = db;
                Collection = _db.GetCollection<T>(collectionName);
            }

            public void Dispose()
            {
                _db.Dispose();
            }
        }

    }
}
