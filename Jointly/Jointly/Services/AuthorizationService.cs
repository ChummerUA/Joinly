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
    public class AuthorizationService : IAuthorizationService
    {
        readonly string ApiURL = @"https://dev.jointly.space";

        public AuthorizationService() { }

        #region Auth
        public async Task<HttpResponseMessage> SignInAsync(SignInModel model)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                var str = JsonConvert.SerializeObject(model);
                var content = new StringContent(str);

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);

                    var session = await client.PostAsync($"{ApiURL}/session", content);
                    if (!session.IsSuccessStatusCode)
                    {
                        await SecureStorage.SetAsync("login", model.Login);
                        await SecureStorage.SetAsync("pass", model.Password);
                        return session;
                    }

                    result = await client.GetAsync($"{ApiURL}/session");

                    await SecureStorage.SetAsync("SessionKey", await result.Content.ReadAsStringAsync());
                }
            }
            catch(Exception e)
            {

            }

            return result;
        }

        public async Task<HttpResponseMessage> SignUpAsync(SignUpModel model)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                var str = JsonConvert.SerializeObject(model);
                var content = new StringContent(str);

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    result = await client.PostAsync($"{ApiURL}/users", content);
                }
            }
            catch(Exception e)
            {

            }
            
            return result;
        }
        #endregion

        public async Task<HttpResponseMessage> SessionStatusAsync()
        {
            HttpResponseMessage result = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                var key = await SecureStorage.GetAsync("SessionKey");
                var content = new StringContent(key);
                result = await client.PostAsync($"{ApiURL}/session/{key}/status", content);
            }
            if(result.StatusCode != System.Net.HttpStatusCode.RequestTimeout)
            {
                await LogoutAsync();
            }
            return result;
        }

        public async Task LogoutAsync()
        {
            SecureStorage.Remove("SessionKey");
            SecureStorage.Remove("login");
            SecureStorage.Remove("pass");
        }
    }
}
