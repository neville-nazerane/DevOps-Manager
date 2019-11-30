using DevOpsManager.MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using DevOpsManager.MobileApp.Services.Helpers;
using System.Net;

namespace DevOpsManager.MobileApp.Services
{
    public class DevOpsService
    {
        private readonly HttpClient _client;

        private string Organization => Preferences.Get("org", null);

        public DevOpsService(HttpClient client)
        {
            _client = client;
        }

        public void Authroize(string personalAccessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                                                                    Convert.ToBase64String(
                                                                        Encoding.ASCII.GetBytes(
                                                                            string.Format("{0}:{1}", "", personalAccessToken))));
        }

        public async Task<DevOpsListingResponse<Project>> GetProjectsAsync()
        {
            var result = await _client.GetAsync($"{Organization}/_apis/projects?api-version=5.1");
            result.EnsureSuccessStatusCode();
            return await result.ReadAsync<DevOpsListingResponse<Project>>();
        }


    }
}
