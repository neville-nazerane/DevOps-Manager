using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DevOpsManager.MobileApp.Services.Helpers
{
    public static class DevOpsHttpClientExtension
    {

        public static async Task<HttpResponseMessage> GetDevOpsAsync(this HttpClient client, string organization, string resource, string version, string area = null, string project = null)
        {

            string path;
            if (area is null)
                path = $"{organization}/_apis/{resource}?api-version={version}";
            else path = $"{organization}/_apis/{area}/{resource}?api-version={version}";

            return await client.GetAsync(path);
        }

    }
}
