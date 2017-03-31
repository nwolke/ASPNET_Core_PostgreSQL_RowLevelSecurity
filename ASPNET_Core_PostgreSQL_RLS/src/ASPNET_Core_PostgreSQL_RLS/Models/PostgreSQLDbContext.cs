using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ASPNET_Core_PostgreSQL_RLS.Utility;

namespace ASPNET_Core_PostgreSQL_RLS.Models
{
    public class PostgreSQLDbContext : DbContext
    {
        private readonly IOptions<RLS_Options> _options;

        /// <summary>
        /// this constructor is used by the PostgreSQLContextFactory for migrations only
        /// </summary>
        /// <param name="options"></param>
        public PostgreSQLDbContext(DbContextOptions<PostgreSQLDbContext> options) : base(options) { }

        /// <summary>
        /// this constructor is used during normal requests
        /// </summary>
        /// <param name="options"></param>
        public PostgreSQLDbContext(IOptions<RLS_Options> options)
        {
            _options = options;
        }

        /// <summary>
        /// this is always called, but during migration we don't want it to attempt to use the RLS_Options object
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (_options != null)
            {
                var optionsvalues = _options.Value;
                options.UseNpgsql(string.Format(optionsvalues.ConnectionString, optionsvalues.TenantName));
            }
        }

        public DbSet<TestModel> TestModels { get; set; }
    }
}
