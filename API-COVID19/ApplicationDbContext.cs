using API_COVID19.Models;
using Microsoft.EntityFrameworkCore;

namespace API_COVID19
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Country>()
                .HasMany(s => s.ProvinceStates)
                .WithOne(s => s.Country)
                .HasForeignKey(c => c.CountryId);
        }

        public DbSet<Country> Country { get; set; }
        public DbSet<CountryDataCovid> CountryDataCovid { get; set; }
        public DbSet<ProvinceState> ProvinceState { get; set; }
    }
}
