using Microsoft.Extensions.Configuration;
using System;

namespace ASPNET_Core_PostgreSQL_RLS.Utility
{
    public static class ConnStrings
    {
        public static string GetEnvironmentDBConnectionString(string environment, IConfiguration config)
        {
            switch (environment)
            {
                case "Staging":
                    return config.GetConnectionString("StagingConnection");
                case "Sandbox":
                    return config.GetConnectionString("SandboxConnection");
                case "Production":
                    return config.GetConnectionString("ProductionConnection");
                case "Development":
                default:
                    return config.GetConnectionString("DevelopmentConnection");

            }
        }
    }
}
