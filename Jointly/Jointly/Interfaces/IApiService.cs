using Jointly.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Interfaces
{
    public interface IApiService
    {
        Task<ResponseModel> GetAsync(string action, BindableBase model = null);

        Task<ResponseModel> PostAsync(string action, BindableBase model = null);
    }
}
