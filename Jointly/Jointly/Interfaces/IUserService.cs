using Jointly.Models;
using Jointly.Models.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jointly.Interfaces
{
    public interface IUserService
    {
        User User { get; }

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="signIn">Model with email and password</param>
        /// <returns>AuthResponse</returns>
        Task<APIResponse<TokenObject>> SignInAsync(SignInModel signIn, CancellationToken cToken = default);

        /// <summary>
        /// Sign up
        /// </summary>
        /// <param name="signUp">Model with person, email and password</param>
        /// <returns>AuthResponse</returns>
        Task<APIResponse<TokenObject>> SignUpAsync(SignUpModel signUp, CancellationToken cToken = default);

        /// <summary>
        /// Logout
        /// </summary>
        Task LogOutAsync(CancellationToken cToken = default);

        Task<APIResponse<User>> GetUserAsync(CancellationToken cToken = default);

        Task<APIResponse<RefreshTokenObject>> RefreshTokenAsync(CancellationToken cToken = default);

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
