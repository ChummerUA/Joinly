using Jointly.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Interfaces
{
    public interface IUserService
    {
        UserModel User { get; }

        Task<ResponseModel> SignInAsync(SignInModel model);

        Task<ResponseModel> SignUpAsync(SignUpModel model);

        Task LogoutAsync();
    }
}
