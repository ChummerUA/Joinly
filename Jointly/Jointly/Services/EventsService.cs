using Jointly.Interfaces;
using Jointly.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Services
{
    public class EventsService : IEventsService
    {
        public Task CreateEventAsync(Event @event)
        {
            return Task.FromResult(true);
        }

        public async Task<List<Event>> GetSavedEventsAsync()
        {
            return new List<Event>();
        }
    }
}
