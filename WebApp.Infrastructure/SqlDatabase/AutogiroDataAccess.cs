using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Core.Models;

namespace WebApp.Infrastructure.SqlDatabase
{
    public class AutogiroDataAccess : BaseDataAccess,IAutogiroDataAccess
    {
        public AutogiroDataAccess(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> AddAsync(Autogiro giro)
        {
            return await ExecuteAsync<Autogiro>("Autogiro_Add", giro);
        }
    }
}
