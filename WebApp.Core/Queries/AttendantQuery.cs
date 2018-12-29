using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;

namespace WebApp.Core.Queries
{
    public class AttendantQuery : IAttendantQuery
    {
        private readonly IAttendantDataAccess _attendantDataAccess;

        public AttendantQuery(IAttendantDataAccess attendantDataAccess)
        {
            _attendantDataAccess = attendantDataAccess;
        }

        public async Task<List<Attendant>> GetAttendatiesAsync(Attendant attendant)
        {
           return (await _attendantDataAccess.GetByUsernameAsync(attendant)).ToList();
        }
    }
}
