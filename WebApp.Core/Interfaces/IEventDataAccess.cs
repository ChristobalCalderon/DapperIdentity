using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Core.Interfaces
{
    public interface IEventDataAccess
    {
        Task<IEnumerable<Event>> GetByIdAsync(Event eve);
        Task<IEnumerable<Event>> GetAsync();
        Task<bool> AddAsync(Event eve);
        Task<bool> UpdateAsync(Event eve);
        Task<bool> DeleteByIdAsync(Event eve);
    }
}
