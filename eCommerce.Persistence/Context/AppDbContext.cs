using System.Reflection;
using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Persistence.Context;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("User ID =postgres ; Password = eselk00101; Host = localhost;Port =5432; Database = postgres");
    }

    public DbSet<Product>  Products{ get; set; }
    public DbSet<Category>  Categories{ get; set; }
    public DbSet<Brand> Brands{ get; set; }
    public DbSet<Detail> Details { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}