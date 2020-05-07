using Jointly.Interfaces;
using Jointly.Models.Responses;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Jointly.Services
{
    public class ApiService : BaseAPIService
    {
        private readonly IUserService UserService;

        public ApiService(
            HttpClient client,
            IAnalyticsService analyticsService,
            IUserService userService) : base(client, analyticsService)
        {
            UserService = userService;
        }

        protected async override Task<APIResponse<T>> ProcessResponseAsync<T>(HttpResponseMessage responseMessage, Task<APIResponse<T>> retryIfTokenRefreshed)
        {
            var processDefaultTask = ProcessResponseAsync<T>(responseMessage);
            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    var refreshResponse = await UserService.RefreshTokenAsync();
                    return refreshResponse.IsSuccess ? await retryIfTokenRefreshed : await processDefaultTask;
                default:
                    return await processDefaultTask;
            }
        }
    }
}
