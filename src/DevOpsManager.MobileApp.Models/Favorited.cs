using System;
using System.Collections.Generic;
using System.Text;

namespace DevOpsManager.MobileApp.Models
{
    public class Favorited
    {

        public string Id { get; set; }

        public ICollection<string> SelectedIds { get; set; } = new HashSet<string>();

    }
}
