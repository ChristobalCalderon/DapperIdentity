using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Infrastructure.SqlDatabase
{
    public class BaseDataAccess
    {
        protected readonly string _connectionString;

        public BaseDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> ExecuteAsync<T>(string fileName, T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = await File.ReadAllTextAsync(GetLocalPath(fileName));
                return await connection.ExecuteAsync(sql, entity) > 0;
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string fileName, T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = await File.ReadAllTextAsync(GetLocalPath(fileName));
                return await connection.QueryAsync<T>(sql, entity);
            }
        }

        #region private
        private string GetLocalPath(string fileName)
        {
            return new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"SqlScripts\{fileName}.sql").LocalPath;
        }
        #endregion
    }
}
