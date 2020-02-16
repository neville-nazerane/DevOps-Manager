using System;
using System.Collections.Generic;
using System.Text;

namespace DevOpsManager.MobileApp.Models
{
    public class Project : IFavourable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFavorite { get; set; }
    }
}
