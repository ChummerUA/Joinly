using Jointly.Models;
using Jointly.Models.Domain;
using Jointly.Models.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jointly.Interfaces
{
    public interface IUserService
    {
        User User { get; }

        Task SignInAsync(SignInModel signIn, CancellationToken cToken = default);

        Task SignUpAsync(SignUpModel signUp, CancellationToken cToken = default);

        void Logout();

        Task<User> GetUserAsync(CancellationToken cToken = default);

        Task<bool> RefreshTokenAsync(CancellationToken cToken = default);

        Task<string> GetTokenAsync();
    }

    public class TokenChangedEventArgs : EventArgs
    {
        public string Token { get; set; }

        public TokenChangedEventArgs()
        {
            Token = "";
        }

        public TokenChangedEventArgs(string token) =>
            Token = token;
    }


}
