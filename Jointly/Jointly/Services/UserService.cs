using Jointly.Interfaces;
using Jointly.Models;
using Jointly.Models.Responses;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Jointly.Services
{
    public class UserService : BaseAPIService, IUserService
    {
        public User User { get; private set; }

        #region services

        #endregion

        public UserService(HttpClient client, IAnalyticsService analyticsService) : base(client, analyticsService)
        {
        }

        public Task<APIResponse<TokenObject>> SignUpAsync(SignUpModel model, CancellationToken cToken = default)
        {
            return PostAsync<TokenObject>("users", model);
        }

        public Task<APIResponse<TokenObject>> SignInAsync(SignInModel model, CancellationToken cToken = default)
        {
            return PostAsync<TokenObject>("session", model);
        }

        public async Task LogOutAsync(CancellationToken cToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<APIResponse<User>> GetUserAsync(CancellationToken cToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<APIResponse<RefreshTokenObject>> RefreshTokenAsync(CancellationToken cToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTokenAsync()
        {
            throw new NotImplementedException();
        }

        protected override async Task<APIResponse<T>> ProcessResponseAsync<T>(HttpResponseMessage responseMessage, Task<APIResponse<T>> retryIfTokenRefreshed)
        {
            var processDefaultTask = ProcessResponseAsync<T>(responseMessage);
            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    var refreshResponse = await RefreshTokenAsync();
                    return refreshResponse.IsSuccess ? await retryIfTokenRefreshed : await processDefaultTask;
                default:
                    return await processDefaultTask;
            }
        }
    }
}
