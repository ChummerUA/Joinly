using Jointly.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Services
{
    public interface IAuthorizationService
    {
        Task<HttpResponseMessage> SignInAsync(SignInModel model);

        Task<HttpResponseMessage> SignUpAsync(SignUpModel model);

        Task<HttpResponseMessage> SessionStatusAsync();

        Task LogoutAsync();
    }
}
