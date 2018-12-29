using DbUp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WebApp.Core.Interfaces;

namespace WebApp.Infrastructure.SqlDatabase.Migration
{
    public static class MigrationService
    {
        public static void Migrate(string _connectionString)
        {
            EnsureDatabase.For.SqlDatabase(_connectionString);

            var upgrader = DeployChanges.To.SqlDatabase(_connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .WithExecutionTimeout(TimeSpan.FromSeconds(700))
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw new Exception($"DbUp: migration failed: {result.Error}");
            }
        }

        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {

            }
        }
    }
}
