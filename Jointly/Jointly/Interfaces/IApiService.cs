using Jointly.Models;
using Jointly.Models.Responses;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jointly.Interfaces
{
    public interface IBaseAPI
    {
        /// <summary>
        /// Send GET request
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="url">Method route</param>
        /// <param name="query">Collection of variables and parameters to send in url</param>
        /// <param name="headers">Custom http headers</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>BaseResponse object</returns>
        Task<APIResponse<T>> GetAsync<T>(string url, NameValueCollection query = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default);

        /// <summary>
        /// Send POST request
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="url">Method route</param>
        /// <param name="obj">Request body</param>
        /// <param name="headers">Custom http headers</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>BaseResponse object</returns>
        Task<APIResponse<T>> PostAsync<T>(string url, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default);

        /// <summary>
        /// Send PUT request
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="url">Method route</param>
        /// <param name="obj">Request body</param>
        /// <param name="headers">Custom http headers</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>BaseResponse object</returns>
        Task<APIResponse<T>> PutAsync<T>(string url, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default);

        /// <summary>
        /// Send DELETE request
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="url">Method route</param>
        /// <param name="obj">Request body</param>
        /// <param name="headers">Custom http headers</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>BaseResponse object</returns>
        Task<APIResponse<T>> DeleteAsync<T>(string url, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default);

        /// <summary>
        /// Send PATCH request
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="url">Method route</param>
        /// <param name="obj">Request body</param>
        /// <param name="headers">Custom http headers</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>BaseResponse object</returns>
        Task<APIResponse<T>> PatchAsync<T>(string url, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default);

        /// <summary>
        /// Send POST request with file
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="url">Method route</param>
        /// <param name="filepath">Path of file to be uploaded</param>
        /// <param name="headers">Custom http headers</param>
        /// <param name="obj">Request body</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>BaseResponse object</returns>
        Task<APIResponse<T>> UploadAsync<T>(string url, string filepath, object obj = null, List<KeyValuePair<string, string>> headers = null, CancellationToken cToken = default);
    }
}
