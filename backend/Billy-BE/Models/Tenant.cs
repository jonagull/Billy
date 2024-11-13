public class Tenant
{
    public int Id { get; set; } // Unique Tenant ID
    public string Name { get; set; } // Tenant Name
    public string Subdomain { get; set; } // Subdomain for this tenant
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // When the tenant was created
}
