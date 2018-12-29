using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Core.Interfaces
{
    public interface IDiaryDataAccess
    {
        Task<IEnumerable<Diary>> GetByUsernameAsync(Diary diary);
        Task<Diary> GetByIdAsync(Diary diary);
        Task<bool> AddAsync(Diary diary);
        Task<bool> UpdateAsync(Diary diary);
        Task<bool> DeleteByIdAsync(Diary diary);
    }
}
