﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.HealthCheck
{
    public class ExampleHealthCheck : IHealthCheck
    {
        public ExampleHealthCheck()
        {
            // Use dependency injection (DI) to supply any required services to the
            // health check.
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            // Execute health check logic here. This example sets a dummy
            // variable to true.
            var healthCheckResultHealthy = true;

            if (healthCheckResultHealthy)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("The check indicates a healthy result."));
            }

            return Task.FromResult(
                HealthCheckResult.Unhealthy("The check indicates an unhealthy result."));
        }
    }
}