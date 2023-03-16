using API_COVID19.Models;
using Microsoft.EntityFrameworkCore;

namespace API_COVID19
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //modelBuilder.Entity<Country>()
            //    .HasMany(s => s.ProvinceStates)
            //    .WithOne(s => s.Country)
            //    .HasForeignKey(c => c.CountryId);
        }

        public DbSet<Country> Country { get; set; }
        public DbSet<Cases> Cases { get; set; }
        public DbSet<ProvinceState> ProvinceState { get; set; }
        public DbSet<Vaccinateds> Vaccinateds { get; set; }
        public DbSet<WorldmapData> WorldmapData { get; set; }
        public DbSet<Frequency> Frequency { get; set; }
        public DbSet<FrequencyType> FrequencyType { get; set; }

    }
}
