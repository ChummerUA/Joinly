using Jointly.Extensions;
using Jointly.Interfaces;
using Jointly.Models;
using Jointly.Models.Responses;
using Jointly.Resources;
using Jointly.Utils;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jointly.Services
{
    public class BaseAPI : IBaseAPI
    {
        private string BaseURL => Constants.APIURL;
        private readonly IPreferencesService PreferencesService;
        private readonly APIClient Client;
        private readonly IUserService UserService;

        public BaseAPI(
            APIClient client,
            IPreferencesService preferencesService,
            IUserService userService)
        {
            PreferencesService = preferencesService;
            UserService = userService;
            Client = client;
        }

        private async Task<APIResponse<T>> ProcessResponseAsync<T>(APIResponse<T> response, Task<APIResponse<T>> retryIfTokenRefreshed)
        {
            if (response.IsSuccess)
                return response;

            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshResponse = await UserService.RefreshTokenAsync();
                if (refreshResponse.IsSuccess)
                    return await retryIfTokenRefreshed;
            }
            return response;

        }

        public async Task<APIResponse<T>> GetAsync<T>(string url, NameValueCollection nvc = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            var task = Client.GetAsync<T>(url, nvc, headers, cToken);
            var result = await Client.GetAsync<T>(url, nvc, headers, cToken);
            return await ProcessResponseAsync(result, task);
        }

        public async Task<APIResponse<T>> PostAsync<T>(string url, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            var task = Client.PostAsync<T>(url, obj, headers, cToken);
            var result = await PostAsync<T>(url, obj, headers, cToken);
            return await ProcessResponseAsync(result, task);
        }

        public async Task<APIResponse<T>> PutAsync<T>(string url, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            var task = Client.PutAsync<T>(url, obj, headers, cToken);
            var result = await Client.PutAsync<T>(url, obj, headers, cToken);
            return await ProcessResponseAsync(result, task);
        }

        public async Task<APIResponse<T>> DeleteAsync<T>(string url, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            var task = Client.DeleteAsync<T>(url, obj, headers, cToken);
            var result = await Client.DeleteAsync<T>(url, obj, headers, cToken);
            return await ProcessResponseAsync(result, task);
        }

        public async Task<APIResponse<T>> PatchAsync<T>(string url, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            var task = Client.PatchAsync<T>(url, obj, headers, cToken);
            var result = await Client.PatchAsync<T>(url, obj, headers, cToken);
            return await ProcessResponseAsync(result, task);
        }

        public async Task<APIResponse<T>> UploadAsync<T>(string url, string filepath, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            var task = Client.UploadAsync<T>(url, filepath, obj, headers, cToken);
            var result = await Client.UploadAsync<T>(url, filepath, obj, headers, cToken);
            return await ProcessResponseAsync(result, task);
        }
    }
}
