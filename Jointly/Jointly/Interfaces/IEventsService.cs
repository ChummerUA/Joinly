using Jointly.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jointly.Interfaces
{
    public interface IEventsService
    {
        Task CreateEventAsync(Event @event);

        Task<List<Event>> GetSavedEventsAsync();
    }
}
