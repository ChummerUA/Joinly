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
        Task<ResponseModel> GetAsync(BindableBase model = null);

        Task<ResponseModel> PostAsync(BindableBase model = null);
    }
}
