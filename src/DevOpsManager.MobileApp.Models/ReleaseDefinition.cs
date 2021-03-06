﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DevOpsManager.MobileApp.Models
{
    public class ReleaseDefinition
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFavorite { get; set; }
        public List<Release> Releases { get; set; }

    }
}
