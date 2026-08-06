using BaglantiApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BaglantiApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Startup> Startups { get; set; }
        public DbSet<Investor> Investors { get; set; }
    }
}