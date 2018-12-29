using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;

namespace WebApp.Infrastructure.SqlDatabase
{
    public class AttendantDataAccess : BaseDataAccess, IAttendantDataAccess
    {
        public AttendantDataAccess(IConfiguration configuration):base(configuration)
        {
        }

        public async Task<IEnumerable<Attendant>> GetByUsernameAsync(Attendant attendant)
        {
            return await QueryAsync<Attendant>("Attendant_GetByUsername", attendant);
        }

        public async Task<bool> AddAsync(Attendant attendant)
        {
            return await ExecuteAsync<Attendant>("Attendant_Add", attendant);
        }

        public async Task<bool> UpdateAsync(Attendant attendant)
        {
            return await ExecuteAsync<Attendant>("Attendant_Update", attendant);
        }
    }
}
