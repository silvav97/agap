using Agap.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agap.Backemd.Data
{
    public class DataContext : IdentityDbContext<User>

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Fertilizer> Fertilizers { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(country => country.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(state => new { state.CountryId, state.Name }).IsUnique();
            modelBuilder.Entity<City>().HasIndex(city => new { city.StateId, city.Name }).IsUnique();
            modelBuilder.Entity<Fertilizer>().HasIndex(fertilizer => new { fertilizer.Name, fertilizer.Brand }).IsUnique();

        }
    }
}
