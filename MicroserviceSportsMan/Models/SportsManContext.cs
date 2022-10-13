using Microsoft.EntityFrameworkCore;
namespace MicroserviceSportsMan.Models
{
    public class SportsManContext : DbContext
    {
        public SportsManContext(DbContextOptions<SportsManContext> options)
            : base(options) { }
        public DbSet<SportsMan> SportsMen { get; set; }
    }
}
