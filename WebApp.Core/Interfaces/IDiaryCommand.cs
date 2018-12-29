using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Core.Interfaces
{
    public interface IDiaryCommand
    {
        Task<bool> StoreDiaryAsync(Diary diary);
        Task<bool> DeleteDiaryAsync(Diary diary);
        Task<bool> ChangeDiaryAsync(Diary diary);
    }
}
