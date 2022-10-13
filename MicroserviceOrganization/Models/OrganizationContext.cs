using Microsoft.EntityFrameworkCore;
namespace MicroserviceOrganization.Models
{
    public class OrganizationContext : DbContext
    {
        public OrganizationContext(DbContextOptions<OrganizationContext> options)
            : base(options) { }
        public DbSet<Organization>? Organizations { get; set; }
    }
}
