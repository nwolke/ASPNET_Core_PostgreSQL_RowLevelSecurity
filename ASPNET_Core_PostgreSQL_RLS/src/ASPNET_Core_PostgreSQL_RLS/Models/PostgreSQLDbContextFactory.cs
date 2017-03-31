using ASPNET_Core_PostgreSQL_RLS.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ASPNET_Core_PostgreSQL_RLS.Models
{
    /// <summary>
    /// This class inherits from IDbContextFactory which is actually only used during migrations, despite the poor naming convention
    /// </summary>
    public class PostgreSQLDbContextFactory : IDbContextFactory<PostgreSQLDbContext>
    {
        public PostgreSQLDbContext Create(DbContextFactoryOptions options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgreSQLDbContext>();
            var config = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json")
                     .AddEnvironmentVariables()
                     .Build();
            var env = config.GetChildren().Where(c => c.Key == "ASPNETCORE_ENVIRONMENT").FirstOrDefault();
            if (env == null)
                throw new KeyNotFoundException("Environment variable ASPNETCORE_ENVIRONMENT not set");

            string envName = env.Value;
            optionsBuilder.UseNpgsql(ConnStrings.GetEnvironmentDBConnectionString(envName, config));

            return new PostgreSQLDbContext(optionsBuilder.Options);
        }
    }
}
