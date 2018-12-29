using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;

namespace WebApp.Core.Commands
{
    public class DiaryCommand : IDiaryCommand
    {
        public readonly IDiaryDataAccess _diaryDataAccess;

        public DiaryCommand(IDiaryDataAccess autogiroDataAccess)
        {
            _diaryDataAccess = autogiroDataAccess;
        }

        public async Task<bool> StoreDiaryAsync(Diary diary)
        {
            return await _diaryDataAccess.AddAsync(diary);
        }

        public async Task<bool> ChangeDiaryAsync(Diary diary)
        {
            return await _diaryDataAccess.UpdateAsync(diary);
        }

        public async Task<bool> DeleteDiaryAsync(Diary diary)
        {
            return await _diaryDataAccess.DeleteByIdAsync(diary);
        }

    }
}
