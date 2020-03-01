using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace DevOpsManager.MobileApp.Services
{
    public class PersistantState
    {

        private const string ProjectKey = "Project";
        private const string OrganizationKey = "Organization";

        public virtual string Project { get => Preferences.Get(ProjectKey, null); set => Preferences.Set(ProjectKey, value); }
        public virtual string Organization { get => Preferences.Get(OrganizationKey, null); set => Preferences.Set(OrganizationKey, value); }


    }
}
