using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;

namespace WebApp.Core.Queries
{
    public class DiaryQuery : IDiaryQuery
    {
        private readonly IDiaryDataAccess _diaryDataAccess;

        public DiaryQuery(IDiaryDataAccess diaryDataAccess)
        {
            _diaryDataAccess = diaryDataAccess;
        }

        public async Task<List<Diary>> GetDariesAsync(Diary diary)
        {
            return (await _diaryDataAccess.GetByUsernameAsync(diary)).ToList();
        }

        public async Task<Diary> GetDiaryAsync(Diary diary)
        {
            return await _diaryDataAccess.GetByIdAsync(diary);
        }
    }
}
