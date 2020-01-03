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

        public const string projectKey = "project";

        static readonly LiteDatabase db = new LiteDatabase(DatabaseLocation);

        LiteCollection<Favorite> Collection => db.GetCollection<Favorite>();

        private void Add(string id)
        {
            Favorite favorite = new Favorite { Id = id };
            bool found = Collection.Exists(FindQuery(favorite));
            if (!found)
            {
                Collection.Insert(favorite);
                Collection.EnsureIndex(nameof(favorite.Id));
            }
        }

        public void Remove(string id)
        {
            Collection.Delete(FindQuery(new Favorite { Id = id }));
        }

        private Query FindQuery(Favorite favorite)
            => Query.EQ(nameof(favorite.Id), favorite.Id);

        #region projects 

        public void AddProject(string id) => Add($"{projectKey}-{id}");
        public void RemoveProject(string id) => Remove($"{projectKey}-{id}");

        public void UpdateProject(Project project)
        {
            if (project.IsFavorite)
                AddProject(project.Id);
            else
                RemoveProject(project.Id);
        }

        public void UpdateToProjects(IEnumerable<Project> projects)
        {
            BsonValue[] ids = projects.Select(p => (BsonValue) $"{projectKey}-{p.Id}").ToArray();
            string[] found = Collection.Find(Query.In(nameof(Favorite.Id), ids))
                                                        .Select(f => f.Id.Substring(projectKey.Length + 1)).ToArray();
            foreach (var project in projects)
            {
                if (found.Contains(project.Id))
                    project.IsFavorite = true;
                else
                    project.IsFavorite = false;
            }
        }

        #endregion

    }
}
