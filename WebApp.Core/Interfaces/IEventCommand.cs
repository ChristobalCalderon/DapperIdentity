using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Core.Interfaces
{
    public interface IEventCommand
    {
        Task<bool> StoreEventAsync(Event eve);
        Task<bool> DeleteEventAsync(Event eve);
        Task<bool> ChangeEventAsync(Event eve);
    }
}
