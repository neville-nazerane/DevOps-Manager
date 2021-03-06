﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DevOpsManager.MobileApp.Models
{
    public class Release
    {

        public int Id { get; set; }

        public string Name { get; set; }

        //public string Status { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public IdentityRef CreatedBy { get; set; }

        public Release()
        {
            CreatedBy = new IdentityRef();
        }

    }
}
