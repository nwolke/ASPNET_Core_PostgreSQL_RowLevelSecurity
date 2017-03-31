using ASPNET_Core_PostgreSQL_RLS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ASPNET_Core_PostgreSQL_RLS.Middleware
{
    /// <summary>
    /// This middleware grabs the subdomain from the Request to identify the tenant
    /// </summary>
    public class TenantIdentifier
    {
        private readonly RequestDelegate _next;

        public TenantIdentifier(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IOptions<RLS_Options> options)
        {
            var tenant = context.Request.Host.Host.Split('.')[0];
            if (tenant.Contains("localhost"))
                options.Value.TenantName = "tenantadmin";
            else
                options.Value.TenantName = tenant;

            await _next.Invoke(context);
        }
    }
}
