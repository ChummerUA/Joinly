using Jointly.Interfaces;
using Jointly.Models;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Services
{
    public class ApiService : IApiService
    {
        readonly string ApiURL = @"https://dev.jointly.space";

        public async Task<ResponseModel> GetAsync(string action, BindableBase model = null)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(new HttpMethod("GET"), $"{ApiURL}/{action}");

                    var result = await client.SendAsync(request);
                    var resultStr = await result.Content.ReadAsStringAsync();

                    return new ResponseModel
                    {
                        IsSuccess = true,
                        ResultObject = JsonConvert.DeserializeObject(resultStr)
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    ResultObject = null
                };
            }
        }

        public async Task<ResponseModel> PostAsync(string action, BindableBase model = null)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(new HttpMethod("POST"), $"{ApiURL}/{action}");
                    if(model != null)
                    {
                        var content = JsonConvert.SerializeObject(model);
                        request.Content = new StringContent(content);
                    }

                    var result = await client.SendAsync(request);
                    var resultStr = await result.Content.ReadAsStringAsync();

                    return new ResponseModel
                    {
                        IsSuccess = true,
                        ResultObject = JsonConvert.DeserializeObject(resultStr)
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    ResultObject = null
                };
            }
        }
    }
}
