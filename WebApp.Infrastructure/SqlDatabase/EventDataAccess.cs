using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;

namespace WebApp.Infrastructure.SqlDatabase
{
    public class EventDataAccess : BaseDataAccess, IEventDataAccess
    {
        public EventDataAccess(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> AddAsync(Event eve)
        {
            return await ExecuteAsync<Event>("Event_Add", eve);
        }

        public async Task<bool> DeleteByIdAsync(Event eve)
        {
            return await ExecuteAsync("Event_Delete", eve);
        }

        public async Task<IEnumerable<Event>> GetByIdAsync(Event eve)
        {
            return await QueryAsync<Event>("Event_GetById", eve);
        }

        public async Task<IEnumerable<Event>> GetAsync()
        {
            return await QueryAsync<Event>("Event_Get", null);
        }

        public async Task<bool> UpdateAsync(Event eve)
        {
            return await ExecuteAsync<Event>("Event_Update", eve);
        }
    }
}
