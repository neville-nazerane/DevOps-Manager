using System;
using System.Collections.Generic;
using System.Text;

namespace DevOpsManager.MobileApp.Models
{
    public class Account : IFavourable
    {

        public string Name { get; set; }

        public string Key { get; set; }

        public bool IsFavorite { get; set; }
    }
}
