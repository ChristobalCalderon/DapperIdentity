using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Core.Interfaces
{
    public interface IAttendantDataAccess
    {
        Task<IEnumerable<Attendant>> GetByUsernameAsync(Attendant attendant);
        Task<bool> AddAsync(Attendant attendant);
        Task<bool> UpdateAsync(Attendant attendant);
    }
}
