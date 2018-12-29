using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Controllers.Base;
using WebApp.Core;
using WebApp.Core.Helpers;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;
using WebApp.Core.Models.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AutoGiroController : BaseController
    {
        private readonly IAutogiroCommand _autogiroCommand;
        private readonly IEmailSender _emailSender;
        private readonly IBlobCommand _storage;

        public AutoGiroController(IAutogiroCommand autogiroCommand, IEmailSender emailSender, IBlobCommand storage)
        {
            _autogiroCommand = autogiroCommand;
            _emailSender = emailSender;
            _storage = storage;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody]AutoGiroViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var autoGiro = Mapper.Map<AutoGiroViewModel, Autogiro>(vm);
                    var result = await _autogiroCommand.StoreAutogiroAsync(autoGiro);

                    if (result)
                    {
                        await _emailSender.SendEmailAsync("christobal_c@hotmail.com", GetClaimValue("sub"), "Autogiro anmälning", vm.ToString(), vm.Signature);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }

            var errors = ModelState.Select(x => x.Value.Errors)
                       .Where(y => y.Count > 0)
                       .ToList();

            return BadRequest(errors);
        }
    }
}
