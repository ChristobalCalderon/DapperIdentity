using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.HealthCheck;

namespace WebApp.Controllers
{
    [Route("[controller]/[action]")]
    public class StatisticAttendantController : Controller
    {
        private readonly IMediator _mediator;

        public StatisticAttendantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new ProductByIdQueryAsync { Id = 1 });
            return Ok(result.Id + result.Description);
        }
    }
}
