using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
    public class AttendantController : BaseController
    {
        private readonly IAttendantCommand _attendCommand;
        private readonly IAttendantQuery _attendantQuery;

        public AttendantController(IAttendantCommand attendCommand, IAttendantQuery attendantQuery)
        {
            _attendCommand = attendCommand;
            _attendantQuery = attendantQuery;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AttendantViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var attend = Mapper.Map<AttendantViewModel, Attendant>(vm);
                attend.Attend = true;
                var userId = GetClaimValue("sub");
                attend.Username = userId;
                var result = await _attendCommand.AttendAsync(attend);

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
        public async Task<IActionResult> Put(AttendantViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var attend = Mapper.Map<AttendantViewModel, Attendant>(vm);
                var userId = GetClaimValue("sub");
                attend.Username = userId;
                var result = await _attendCommand.ChangeAttendStatusAsync(attend);

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
        public async Task<List<Attendant>> Get()
        {
            var userId = GetClaimValue("sub");
            var attendant = new Attendant { Username = userId };
            return await _attendantQuery.GetAttendatiesAsync(attendant);
        }

        [HttpGet]
        public async Task<List<Attendant>> GetByLocationAndDate(string location, DateTime date)
        {
            var userId = GetClaimValue("sub");
            var attendant = new Attendant
            {
                Username = userId,
                Location = location,
                Date = date
            };
            return await _attendantQuery.GetAttendatiesAsync(attendant);
        }
    }
}
