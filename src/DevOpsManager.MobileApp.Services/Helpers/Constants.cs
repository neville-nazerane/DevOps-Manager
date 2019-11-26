using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace DevOpsManager.MobileApp.Services.Helpers
{
    public class Constants
    {

        public readonly static string DatabaseLocation = Path.Combine(FileSystem.AppDataDirectory, "store.db");

        public const string accountCollectionName = "account";

    }
}
