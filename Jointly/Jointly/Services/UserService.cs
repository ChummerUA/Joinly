using Jointly.Interfaces;
using Jointly.Models;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Jointly.Services
{
    public class UserService : IUserService
    {
        public UserModel User { get; private set; }

        #region services

        private readonly IApiService ApiService;

        #endregion

        public UserService(
            IApiService apiService) 
        {
            ApiService = apiService;
        }

        public async Task<ResponseModel> SignUpAsync(SignUpModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> SignInAsync(SignInModel model)
        {
            throw new NotImplementedException();
        }

        public async Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
