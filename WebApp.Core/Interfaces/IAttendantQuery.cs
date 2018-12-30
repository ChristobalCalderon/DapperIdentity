using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Core.Interfaces
{
    public interface IAttendantQuery
    {
        Task<List<Attendant>> GetAttendatiesAsync(Attendant attendant);
        Task<Attendant> GetAttendateAsync(Attendant attendant);
    }
}