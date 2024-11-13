using Billy_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class TenantController : ControllerBase
{
    private readonly BillyContext _context;

    public TenantController(BillyContext context)
    {
        _context = context;
    }

    [HttpPost("create-tenant")]
    public async Task<IActionResult> CreateTenant([FromBody] TenantCreateDto tenantDto)
    {
        if (string.IsNullOrEmpty(tenantDto.Name) || string.IsNullOrEmpty(tenantDto.Subdomain))
        {
            return BadRequest("Tenant name and subdomain are required.");
        }

        // Check if the subdomain is already taken
        var existingTenant = await _context.Tenants.FirstOrDefaultAsync(t =>
            t.Subdomain == tenantDto.Subdomain
        );

        if (existingTenant != null)
        {
            return Conflict("Subdomain is already in use.");
        }

        // Create the new tenant
        var newTenant = new Tenant
        {
            Name = tenantDto.Name,
            Subdomain = tenantDto.Subdomain,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Tenants.Add(newTenant);
        await _context.SaveChangesAsync();

        return Ok(
            new
            {
                TenantId = newTenant.Id,
                newTenant.Name,
                newTenant.Subdomain,
            }
        );
    }
}
