using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Controllers.Base;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;

namespace WebApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class EventController : BaseController
    {
        private readonly IEventCommand _eventCommand;
        private readonly IEventQuery _eventQuery;

        public EventController(IEventCommand eventCommand, IEventQuery eventQuery)
        {
            _eventCommand = eventCommand;
            _eventQuery = eventQuery;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Event eve)
        {
            if (ModelState.IsValid)
            {
                var result = await _eventCommand.StoreEventAsync(eve);

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
        public async Task<IActionResult> Put([FromBody]Event eve)
        {
            if (ModelState.IsValid)
            {
                var result = await _eventCommand.ChangeEventAsync(eve);

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
        public async Task<List<Event>> Get()
        {
            return await _eventQuery.GetAsync();
        }

        [HttpGet]
        public async Task<List<Event>> GetById(int id)
        {
            var eve = new Event { Id = id };
            return await _eventQuery.GetEventsAsync(eve);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var eve = new Event
            {
                Id = id,
            };
            var result = await _eventCommand.DeleteEventAsync(eve);

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
