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

        Task<string> GetFromStorageAsync(string key);

        void PutInPreferences(string key, object obj);

        Task PutInStorageAsync(string key, string obj);

        void RemoveFromPreferences(string key);

        void RemoveFromStorage(string key);
    }
}
