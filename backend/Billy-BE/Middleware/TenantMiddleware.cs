using Billy_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public TenantMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // If TenantId is already set, move to the next middleware
        if (context.Items.ContainsKey("TenantId"))
        {
            await _next(context);
            return;
        }

        var pathsToExclude = new List<string>
        {
            "/api/Tenant/create-tenant", // Add paths you want to exclude
            "/api/Tenant/some-other-path" // Example for other paths
            ,
        };

        // Check if the request path matches any of the excluded paths
        if (pathsToExclude.Contains(context.Request.Path.Value))
        {
            await _next(context);
            return;
        }

        // Create a scope to resolve scoped services
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<BillyContext>();

            // Check if the X-Tenant-ID header exists
            if (context.Request.Headers.TryGetValue("X-Tenant-ID", out var tenantNameHeader))
            {
                var tenantName = tenantNameHeader.ToString();

                if (!string.IsNullOrEmpty(tenantName))
                {
                    // Query the database to find the corresponding TenantId
                    var tenant = await _context.Tenants.FirstOrDefaultAsync(t =>
                        t.Name == tenantName
                    );

                    if (tenant != null)
                    {
                        // Store the TenantId in the HttpContext for later use
                        context.Items["TenantId"] = tenant.Id;
                    }
                    else
                    {
                        context.Response.StatusCode = 404; // Tenant not found
                        context.Response.ContentType = "text/plain";
                        await context.Response.WriteAsync("Tenant not found.");
                        await context.Response.CompleteAsync(); // Flush and complete the response
                        return;
                    }
                }
                else
                {
                    context.Response.StatusCode = 400; // Bad request if TenantId is empty
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("Invalid TenantId format.");
                    await context.Response.CompleteAsync(); // Flush and complete the response
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = 400; // Bad request if TenantId header is missing
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("X-Tenant-ID header is missing.");
                await context.Response.CompleteAsync(); // Flush and complete the response
                return;
            }
        }

        // Proceed to the next middleware
        await _next(context);
    }
}
