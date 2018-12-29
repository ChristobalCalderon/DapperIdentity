using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;

namespace WebApp.Core.Commands
{
    public class EventCommand : IEventCommand
    {
        public readonly IEventDataAccess _eventDataAccess;

        public EventCommand(IEventDataAccess eventDataAccess)
        {
            _eventDataAccess = eventDataAccess;
        }

        public async Task<bool> ChangeEventAsync(Event eve)
        {
            return await _eventDataAccess.UpdateAsync(eve);
        }

        public async Task<bool> DeleteEventAsync(Event eve)
        {
            return await _eventDataAccess.DeleteByIdAsync(eve);
        }

        public async Task<bool> StoreEventAsync(Event eve)
        {
            return await _eventDataAccess.AddAsync(eve);
        }
    }
}
