using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;

namespace WebApp.Core.Commands
{
    public class AttendantCommand : IAttendantCommand
    {
        public readonly IAttendantDataAccess _attendantDataAcess;

        public AttendantCommand(IAttendantDataAccess attendantDataAcess)
        {
            _attendantDataAcess = attendantDataAcess;
        }

        public async Task<bool> AttendAsync(Attendant attendant)
        {
            return await _attendantDataAcess.AddAsync(attendant);

        }

        public async Task<bool> ChangeAttendStatusAsync(Attendant attendant)
        {
            return await _attendantDataAcess.UpdateAsync(attendant);
        }
    }
}
