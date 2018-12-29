using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Logging;
using WebApp.Infrastructure;
using WebApp.Core;
using WebApp.Core.Models;
using WebApp.Core.Commands;
using WebApp.Core.Interfaces;
using WebApp.Infrastructure.SqlDatabase;
using WebApp.Core.Queries;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Collections.Generic;
using WebApp.MiddleWare;
using WebApp.Infrastructure.BlobStorage;
using WebApp.HealthCheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MediatR;

namespace WebApp
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(ILogger<Startup> logger, IConfiguration configuration)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddDefaultTokenProviders();

            // Blob storage
            var connectionString = Configuration.GetConnectionString("Storage");
            services.AddTransient<IBlobCommand, BlobCommand>();
            services.AddTransient<IBlobStorage, BlobStorage>(a => new BlobStorage(connectionString));

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            // Add application services.
            services.AddTransient<IEmailSender, SmtpMailService>();
            services.AddTransient<IAutogiroCommand, AutogiroCommand>();
            services.AddTransient<IAutogiroDataAccess, AutogiroDataAccess>();
            services.AddTransient<IAttendantCommand, AttendantCommand>();
            services.AddTransient<IAttendantQuery, AttendantQuery>();
            services.AddTransient<IAttendantDataAccess, AttendantDataAccess>();
            services.AddTransient<IDiaryCommand, DiaryCommand>();
            services.AddTransient<IDiaryDataAccess, DiaryDataAccess>();
            services.AddTransient<IDiaryQuery, DiaryQuery>();
            services.AddTransient<IEventCommand, EventCommand>();
            services.AddTransient<IEventDataAccess, EventDataAccess>();
            services.AddTransient<IEventQuery, EventQuery>();

            //Adding Emailsettings services to the services container.
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Team Leites", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });

            services.AddMvcCore()
                .AddApiExplorer()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddCors();

            services.AddHealthChecks()
                .AddCheck<ExampleHealthCheck>(
                "example_health_check",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "example" });

            services.AddMediatR();
            // if you have handlers/events in other assemblies
            // services.AddMediatR(typeof(SomeHandler).Assembly, 
            //                     typeof(SomeOtherHandler).Assembly);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHealthChecks("/health");
            app.UseStaticFiles();
            app.UseAuthentication();

            //For development only
            app.UseCORSMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1");
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
