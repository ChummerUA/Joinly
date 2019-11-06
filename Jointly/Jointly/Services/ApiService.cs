using Jointly.Interfaces;
using Jointly.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Services
{
    public class ApiService : IApiService
    {
        readonly string ApiURL = @"https://dev.jointly.space";

        public Task<ResponseModel> GetAsync(BindableBase model = null)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> PostAsync(BindableBase model = null)
        {
            throw new NotImplementedException();
        }
    }
}
