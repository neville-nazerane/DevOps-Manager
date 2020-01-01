using System;
using System.Collections.Generic;
using System.Text;

namespace DevOpsManager.MobileApp.Models
{
    public class ProjectNode
    {

        public string Id { get; set; }

        public string Title { get; set; }

        public NodeLevel Level { get; set; }

        public enum NodeLevel
        {
            Project, Provider, Repository, Branch
        }

    }
}
