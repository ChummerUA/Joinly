using Jointly.Interfaces;
using Jointly.Models;
using Jointly.Models.Responses;
using Jointly.Utils;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Jointly.Services
{
    public class UserService : IUserService
    {
        public User User { get; private set; }

        #region services

        private readonly APIClient API;

        #endregion

        public UserService(
            APIClient api) 
        {
            API = api;
        }

        public Task<APIResponse<TokenObject>> SignUpAsync(SignUpModel model, CancellationToken cToken = default)
        {
            try
            {
                return API.PostAsync<TokenObject>("users", model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<APIResponse<TokenObject>> SignInAsync(SignInModel model, CancellationToken cToken = default)
        {
            try
            {
                return API.PostAsync<TokenObject>("session", model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
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
    }
}
