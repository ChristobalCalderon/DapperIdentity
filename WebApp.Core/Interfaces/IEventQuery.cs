using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Core.Interfaces
{
    public interface IEventQuery
    {
        Task<List<Event>> GetEventsAsync(Event eve);
        Task<List<Event>> GetAsync();
    }
}
