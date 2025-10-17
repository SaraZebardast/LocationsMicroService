using Microsoft.EntityFrameworkCore;

namespace Locations.APP.Domain;

public class LocationsDb : DbContext
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }

    public LocationsDb(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().HasIndex(country => country.CountryName).IsUnique();
        modelBuilder.Entity<City>().HasIndex(cityEntity => cityEntity.CityName).IsUnique();
    }
}