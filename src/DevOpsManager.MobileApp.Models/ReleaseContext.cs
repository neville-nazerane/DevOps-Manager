using System;
using System.Collections.Generic;
using System.Text;

namespace DevOpsManager.MobileApp.Models
{
    public class ReleaseContext
    {

        public int ReleaseId { get; set; }

        public IEnumerable<Release> Releases { get; set; }

    }
}
