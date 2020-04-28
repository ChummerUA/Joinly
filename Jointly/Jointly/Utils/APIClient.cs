using Jointly.Extensions;
using Jointly.Models.Responses;
using Newtonsoft.Json;
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

namespace Jointly.Utils
{
    public class APIClient : HttpClient
    {
        public APIClient()
        {
            DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Timeout = TimeSpan.FromSeconds(30);
        }

        private async Task<APIResponse<T>> SendAsync<T>(HttpRequestMessage message, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            var content = "";
            if (cToken == default)
            {
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                cToken = cts.Token;
            }
            try
            {
                if (headers != null)
                {
                    headers?.ForEach(x => message.Headers.Add(x.Key, x.Value));
                }

                var response = await SendAsync(message, cToken);

                switch (response.StatusCode)
                {
                    default:
                        {
                            content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings
                            {
                                MissingMemberHandling = MissingMemberHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore,
                            });

                            return new APIResponse<T>(result);
                        }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<APIResponse<T>> GetAsync<T>(string url, NameValueCollection nvc = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            if (nvc != null)
                url += nvc.ToQuery();
            var message = new HttpRequestMessage(HttpMethod.Get, $"{BaseAddress}{url}");
            return SendAsync<T>(message, headers, cToken);
        }

        public Task<APIResponse<T>> PostAsync<T>(string url, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            if (obj is NameValueCollection nvc)
                url += nvc.ToQuery();

            var message = new HttpRequestMessage(HttpMethod.Post, $"{BaseAddress}{url}");

            if (!(obj is NameValueCollection))
            {
                var json = JsonConvert.SerializeObject(obj);
                message.Content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
            }
            return SendAsync<T>(message, headers, cToken);
        }

        public Task<APIResponse<T>> PutAsync<T>(string url, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            if (obj is NameValueCollection nvc)
                url += nvc.ToQuery();

            var message = new HttpRequestMessage(HttpMethod.Put, $"{BaseAddress}{url}");

            if (!(obj is NameValueCollection))
            {
                var json = JsonConvert.SerializeObject(obj);
                message.Content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
            }
            return SendAsync<T>(message, headers, cToken);
        }

        public Task<APIResponse<T>> DeleteAsync<T>(string url, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            if (obj is NameValueCollection nvc)
                url += nvc.ToQuery();

            var message = new HttpRequestMessage(HttpMethod.Delete, $"{BaseAddress}{url}");

            if (!(obj is NameValueCollection))
            {
                var json = JsonConvert.SerializeObject(obj);
                message.Content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
            }
            return SendAsync<T>(message, headers, cToken);
        }

        public Task<APIResponse<T>> PatchAsync<T>(string url, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            if (obj is NameValueCollection nvc)
                url += nvc.ToQuery();

            var message = new HttpRequestMessage(new HttpMethod("PATCH"), $"{BaseAddress}{url}");

            if (!(obj is NameValueCollection))
            {
                var json = JsonConvert.SerializeObject(obj);
                message.Content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
            }

            return SendAsync<T>(message, headers, cToken);
        }

        public Task<APIResponse<T>> UploadAsync<T>(string url, string filepath, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default)
        {
            if (obj is NameValueCollection nvc)
                url += nvc.ToQuery();

            var bytes = File.ReadAllBytes(filepath);
            var stream = new MemoryStream(bytes);
            var strContent = new StreamContent(stream);
            var name = Path.GetFileName(filepath);
            var message = new HttpRequestMessage(HttpMethod.Post, $"{BaseAddress}{url}");

            using var formData = new MultipartFormDataContent
            {
                { strContent, name, name }
            };
            if (!(obj is NameValueCollection))
                formData.Add(
                    new StringContent(
                        JsonConvert.SerializeObject(obj),
                        Encoding.UTF8,
                        "application/json"),
                    "params");

            message.Content = formData;

            return SendAsync<T>(message, headers, cToken);
        }
    }
}
