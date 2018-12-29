using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Controllers.Base;
using WebApp.Core.Helpers;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;
using WebApp.Core.Models.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class DiaryController : BaseController
    {
        private readonly IDiaryCommand _diaryCommand;
        private readonly IDiaryQuery _diaryQuery;

        public DiaryController(IDiaryCommand diaryCommand, IDiaryQuery diaryQuery)
        {
            _diaryCommand = diaryCommand;
            _diaryQuery = diaryQuery;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DiaryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var diary = Mapper.Map<DiaryViewModel, Diary>(vm);
                diary.UserId = GetClaimValue("sub");

                var result = await _diaryCommand.StoreDiaryAsync(diary);

                if (result)
                {
                    return Ok();
                }
            }

            var errors = ModelState.Select(x => x.Value.Errors)
           .Where(y => y.Count > 0)
           .ToList();

            return BadRequest(errors);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]DiaryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var diary = Mapper.Map<DiaryViewModel, Diary>(vm);
                diary.UserId = GetClaimValue("sub"); ;

                var result = await _diaryCommand.ChangeDiaryAsync(diary);

                if (result)
                {
                    return Ok();
                }
            }

            var errors = ModelState.Select(x => x.Value.Errors)
           .Where(y => y.Count > 0)
           .ToList();

            return BadRequest(errors);
        }

        [HttpGet]
        public async Task<Diary> GetById(int id)
        {
            var userID = GetClaimValue("sub");
            var diary = new Diary { UserId = userID, Id = id };

            return (await _diaryQuery.GetDariesAsync(diary)).FirstOrDefault();
        }

        [HttpGet]
        public async Task<List<Diary>> Get()
        {
            var userID = GetClaimValue("sub");
            var diary = new Diary { UserId = userID };

            return await _diaryQuery.GetDariesAsync(diary);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var diary = new Diary
            {
                Id = id,
                UserId = GetClaimValue("sub")
            };
            var result = await _diaryCommand.DeleteDiaryAsync(diary);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
