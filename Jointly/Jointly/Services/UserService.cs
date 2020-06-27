using Jointly.Extensions;
using Jointly.Interfaces;
using Jointly.Models;
using Jointly.Models.Domain;
using Jointly.Models.DTO;
using Jointly.Models.Responses;
using Jointly.Resources;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Jointly.Services
{
    public class UserService : BaseAPIService, IUserService
    {
        public User User { get; private set; }

        #region services
        private readonly INative Native;
        private readonly IPreferencesService PreferencesService;
        #endregion

        public UserService(
            HttpClient client, 
            IAnalyticsService analyticsService,
            IPreferencesService preferencesService,
            INative native) : base(client, analyticsService)
        {
            Native = native;
            PreferencesService = preferencesService;
        }

        public async Task SignUpAsync(SignUpModel model, CancellationToken cToken = default)
        {
            var body = new
            {
                name = model.FirstName,
                surname = model.LastName,
                email = model.Email,
                phone = model.Phone,
                password = model.Password.ToBase64()
            };
            //TODO: save token if success
            var response = await PostAsync<TokenObject>("users", body, cToken: cToken);
            await PreferencesService.PutInStorageAsync(Constants.Preferences.TokenKey, response);
        }

        public async Task SignInAsync(SignInModel model, CancellationToken cToken = default)
        {
            var body = new 
            {
                login = model.Login.ToBase64(),
                code = model.Password.ToBase64(),
                uuid = Native.GetUUID()
            };

            //TODO: save token if success
            var response = await PostAsync<TokenObject>("session", body, cToken: cToken);
            await PreferencesService.PutInStorageAsync(Constants.Preferences.TokenKey, response);
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserAsync(CancellationToken cToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RefreshTokenAsync(CancellationToken cToken = default)
        {
            var token = await PreferencesService.GetFromStorageAsync<TokenObject>(Constants.Preferences.TokenKey);
            throw new NotImplementedException();
        }

        public Task<string> GetTokenAsync()
        {
            throw new NotImplementedException();
        }

        protected override async Task<T> ProcessResponseAsync<T>(HttpResponseMessage responseMessage, Task<T> retryIfTokenRefreshed)
        {
            var processDefaultTask = ProcessResponseAsync<T>(responseMessage);
            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    var refreshResponse = await RefreshTokenAsync();
                    return refreshResponse != null ? await retryIfTokenRefreshed : await processDefaultTask;
                default:
                    return await processDefaultTask;
            }
        }
    }
}
