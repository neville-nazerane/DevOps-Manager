using DevOpsManager.MobileApp.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DevOpsManager.MobileApp.Services.Helpers.Constants;

namespace DevOpsManager.MobileApp.Services
{
    public class FavoriteService
    {

        private const string projectKey = "project";
        private const string accountKey = "accounts";

        private readonly ILiteDatabase _database;

        public FavoriteService(ILiteDatabase database)
        {
            _database = database;
        }

        private ICollection<string> Get(string id)
        {
            var coll = _database.GetCollection<Favorited>();
            var result = coll.FindById(id)?.SelectedIds;
            if (result is null)
                result = new HashSet<string>();
            return result;
        }

        private void Update(string id, ICollection<string> ids)
        {
            var coll = _database.GetCollection<Favorited>();
            var fav = coll.FindById(id);
            if (fav is null)
            {
                coll.Insert(new Favorited
                {
                    Id = id,
                    SelectedIds = ids
                });
            }
            else
            {
                fav.SelectedIds = ids;
                coll.Update(fav);
            }
        }

        #region accounts 

        public ICollection<string> GetAccounts() => Get(accountKey);

        public void UpdateAccounts(ICollection<string> updated) => Update(accountKey, updated);

        #endregion

        #region projects

        public ICollection<string> GetProjects(string accountId) => Get($"{projectKey}-{accountId}");

        public void UpdateProjects(string orgId, ICollection<string> updated) => Update($"{projectKey}-{orgId}", updated);

        #endregion

    }
}
