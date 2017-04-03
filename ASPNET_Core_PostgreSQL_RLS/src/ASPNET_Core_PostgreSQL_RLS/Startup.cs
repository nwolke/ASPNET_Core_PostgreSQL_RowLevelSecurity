using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ASPNET_Core_PostgreSQL_RLS.Utility;
using ASPNET_Core_PostgreSQL_RLS.Middleware;

namespace ASPNET_Core_PostgreSQL_RLS
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        /// <summary>
        /// configuration
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// constructor. sets up the appsettings.json file for configuration
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(env.ContentRootPath)
                          .AddJsonFile("appsettings.json")
                          .AddEnvironmentVariables();
            Configuration = builder.Build();
            _environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<RLS_Options>(options =>
            {
                options.ConnectionString = ConnStrings.GetEnvironmentDBConnectionString(_environment.EnvironmentName, Configuration);
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseMiddleware<TenantIdentifier>();

            app.UseMvc();
        }
    }
}
