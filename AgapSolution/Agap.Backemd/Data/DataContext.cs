using Agap.Shared.Entities;
using Agap.Shared.Entities.Agap.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agap.Backemd.Data
{
    public class DataContext : IdentityDbContext<User>

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ProjectReport> ProjectReports { get; set; }

        public DbSet<Fertilizer> Fertilizers { get; set; }

        public DbSet<CropType> CropTypes { get; set; }

        public DbSet<Pesticide> Pesticides { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Crop> Crops { get; set; }

        public DbSet<Project> Projects { get; set; }


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
            modelBuilder.Entity<CropType>().HasIndex(cropType => cropType.Name).IsUnique();
            modelBuilder.Entity<Pesticide>().HasIndex(pesticide => new { pesticide.Name, pesticide.Brand }).IsUnique();
            DisableCascadingDelete(modelBuilder);
        }

        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}