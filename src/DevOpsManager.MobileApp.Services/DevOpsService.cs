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
        private readonly PersistantState _persistantState;

        public DevOpsService(HttpClient client, PersistantState persistantState)
        {
            _client = client;
            _persistantState = persistantState;
        }

        public void Authroize(string personalAccessToken)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                                                                    Convert.ToBase64String(
                                                                        Encoding.ASCII.GetBytes(
                                                                            string.Format("{0}:{1}", "", personalAccessToken))));
        }

        public async Task<DevOpsListingResponse<Project>> GetProjectsAsync() => await GetProjectsAsync(_persistantState.Organization);

        public async Task<DevOpsListingResponse<Project>> GetProjectsAsync(string organization)
        {
            var result = await _client.GetAsync($"{organization}/_apis/projects?api-version=5.1");
            result.EnsureSuccessStatusCode();
            return await result.ReadAsync<DevOpsListingResponse<Project>>();
        }

        public async Task<DevOpsListingResponse<ReleaseDefinition>> GetReleaseDefinitionsAsync() => await GetReleaseDefinitionsAsync(_persistantState.Organization, _persistantState.Project);
        
        public async Task<DevOpsListingResponse<ReleaseDefinition>> GetReleaseDefinitionsAsync(string organization, string project)
        {
            var result = await _client.GetAsync($"https://vsrm.dev.azure.com/{organization}/{project}/_apis/release/definitions?api-version=5.1");
            result.EnsureSuccessStatusCode();
            return await result.ReadAsync<DevOpsListingResponse<ReleaseDefinition>>();
        }

        public async Task<DevOpsListingResponse<Release>> GetReleasesAsync(string definitionId)
            => await GetReleasesAsync(_persistantState.Organization, _persistantState.Project, definitionId);
        public async Task<DevOpsListingResponse<Release>> GetReleasesAsync(string organization, string project, string definitionId)
        {
            var result = await _client.GetAsync($"https://vsrm.dev.azure.com/{organization}/{project}/_apis/release/releases?definitionId={definitionId}&api-version=5.1");
            result.EnsureSuccessStatusCode();
            return await result.ReadAsync<DevOpsListingResponse<Release>>();
        }
    }
}
