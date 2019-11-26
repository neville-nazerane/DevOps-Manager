using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace DevOpsManager.MobileApp.Services.Helpers
{
    class Constants
    {

        internal readonly static string DbLocation = Path.Combine(FileSystem.AppDataDirectory, "store.db");

    }
}
