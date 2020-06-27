using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Interfaces
{
    public interface IPreferencesService
    {
        void Clear();

        T GetFromPreferences<T>(string key);

        Task<T> GetFromStorageAsync<T>(string key);

        void PutInPreferences(string key, object obj);

        Task PutInStorageAsync(string key, object obj);

        void RemoveFromPreferences(string key);

        void RemoveFromStorage(string key);
    }
}
