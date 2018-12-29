using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;

namespace WebApp.Infrastructure.SqlDatabase
{
    public class DiaryDataAccess : BaseDataAccess, IDiaryDataAccess
    {
        public DiaryDataAccess(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> AddAsync(Diary diary)
        {
            return await ExecuteAsync<Diary>("Diary_Add", diary);
        }

        public async Task<bool> DeleteByIdAsync(Diary diary)
        {
            return await ExecuteAsync("Diary_Delete", diary);
        }

        public async Task<IEnumerable<Diary>> GetByUsernameAsync(Diary diary)
        {
            return await QueryAsync<Diary>("Diary_GetByUserId", diary);
        }

        public async Task<Diary> GetByIdAsync(Diary diary)
        {
            return (await QueryAsync<Diary>("Diary_GetById", diary)).ToList().FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(Diary diary)
        {
            return await ExecuteAsync<Diary>("Diary_Update", diary);
        }
    }
}
