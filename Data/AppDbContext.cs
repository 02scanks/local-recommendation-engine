using LocalRecomendationEngine.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalRecomendationEngine.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BrowsingEvent> BrowsingEvents { get; set; }
    }
}