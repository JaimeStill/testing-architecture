using App.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Model
            .GetEntityTypes()
            .Where(x => x.BaseType == null)
            .ToList()
            .ForEach(x =>
            {
                modelBuilder
                    .Entity(x.Name)
                    .ToTable(x.Name.Split('.').Last());
            });
    }
}