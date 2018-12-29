using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;

namespace WebApp.Core.Commands
{
    public class AutogiroCommand : IAutogiroCommand
    {
        public readonly IAutogiroDataAccess _autogiroDataAccess;

        public AutogiroCommand(IAutogiroDataAccess autogiroDataAccess)
        {
            _autogiroDataAccess = autogiroDataAccess;
        }

        public async Task<bool> StoreAutogiroAsync(Autogiro giro)
        {
            return await _autogiroDataAccess.AddAsync(giro);
        }
    }
}
