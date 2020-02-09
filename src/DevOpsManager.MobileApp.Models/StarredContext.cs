using System;
using System.Collections.Generic;
using System.Text;

namespace DevOpsManager.MobileApp.Models
{
    public class StarredContext
    {

        public string Identifier { get; set; }

        public int Id { get => int.Parse(Identifier); set => Identifier.ToString(); }

        public bool IsStarred { get; set; }

    }
}
