using System;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Core.Interfaces
{
    public interface IAttendantCommand
    {
        Task<bool> AttendAsync(Attendant attendant);

        Task<bool> ChangeAttendStatusAsync(Attendant attendant);
    }
}