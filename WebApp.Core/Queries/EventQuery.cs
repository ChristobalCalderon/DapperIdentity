using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;

namespace WebApp.Core.Queries
{
    public class EventQuery : IEventQuery
    {
        private readonly IEventDataAccess _eventDataAccess;

        public EventQuery(IEventDataAccess eventDataAccess)
        {
            _eventDataAccess = eventDataAccess;
        }

        public async Task<List<Event>> GetAsync()
        {
            return (await _eventDataAccess.GetAsync()).ToList();
        }

        public async Task<List<Event>> GetEventsAsync(Event eve)
        {
            return (await _eventDataAccess.GetByIdAsync(eve)).ToList();
        }
    }
}
