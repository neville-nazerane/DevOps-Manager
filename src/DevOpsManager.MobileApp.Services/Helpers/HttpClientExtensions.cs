using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevOpsManager.MobileApp.Services.Helpers
{
    public static class HttpClientExtensions
    {

        public static async Task<T> ReadAsync<T>(this HttpResponseMessage response)
        {
            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, string url, object data)
        {
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            return await client.PostAsync(url, content);
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient client, string url, object data)
        {
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            return await client.PutAsync(url, content);
        }

    }
}
