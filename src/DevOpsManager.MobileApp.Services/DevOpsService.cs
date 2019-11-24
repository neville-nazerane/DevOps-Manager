using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DevOpsManager.MobileApp.Services
{
    public class DevOpsService
    {
        private readonly HttpClient _client;

        public DevOpsService(HttpClient client)
        {
            _client = client;
        }

        public async Task AuthroizeAsync(string personalAccessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                                                                    Convert.ToBase64String(
                                                                        Encoding.ASCII.GetBytes(
                                                                            string.Format("{0}:{1}", "", personalAccessToken))));
            //await SecureStorage.SetAsync("pat", personalAccessToken);
        }

        public async Task<bool> TryRefreshAuthAsync()
        {
            string token = "";// await SecureStorage.GetAsync("pat");
            if (string.IsNullOrEmpty(token)) return false;
            await AuthroizeAsync(token);
            return true;
        }

    }
}
