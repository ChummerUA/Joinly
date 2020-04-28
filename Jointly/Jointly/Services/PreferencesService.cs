using Jointly.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Services
{
    public class PreferencesService : IPreferencesService
    {
        private readonly IAnalyticsService AnalyticsService;

        public PreferencesService(IAnalyticsService analyticsService)
        {
            AnalyticsService = analyticsService;
        }

        public async Task<string> GetFromStorageAsync(string key)
        {
            try
            {
                return await Xamarin.Essentials.SecureStorage.GetAsync(key);
            }
            catch (Exception ex)
            {
                AnalyticsService.TrackError(ex, new Dictionary<string, string>
                {
                    { "Source", nameof(PreferencesService) },
                    { "Method", nameof(GetFromStorageAsync) }
                });
                return "";
            }
        }

        public T GetFromPreferences<T>(string key)
        {
            try
            {
                var json = "";
                json = Xamarin.Essentials.Preferences.Get(key, json);
                if (string.IsNullOrEmpty(json))
                    return default;
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                AnalyticsService.TrackError(ex, new Dictionary<string, string>
                {
                    { "Source", nameof(PreferencesService) },
                    { "Method", nameof(PutInPreferences) }
                });
                throw ex;
            }
        }

        public void PutInPreferences(string key, object obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                Xamarin.Essentials.Preferences.Set(key, json);
            }
            catch (Exception ex)
            {
                AnalyticsService.TrackError(ex, new Dictionary<string, string>
                {
                    { "Source", nameof(PreferencesService) },
                    { "Method", nameof(PutInPreferences) }
                });
            }
        }

        public async Task PutInStorageAsync(string key, string obj)
        {
            try
            {
                await Xamarin.Essentials.SecureStorage.SetAsync(key, obj);
            }
            catch (Exception ex)
            {
                AnalyticsService.TrackError(ex, new Dictionary<string, string>
                {
                    { "Source", nameof(PreferencesService) },
                    { "Method", nameof(PutInStorageAsync) }
                });
            }
        }

        public void RemoveFromStorage(string key)
        {
            Xamarin.Essentials.SecureStorage.Remove(key);
        }

        public void RemoveFromPreferences(string key)
        {
            Xamarin.Essentials.Preferences.Remove(key);
        }

        public void Clear()
        {
            Xamarin.Essentials.Preferences.Clear();
            Xamarin.Essentials.SecureStorage.RemoveAll();
        }
    }
}
