using System.Reflection;
using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Persistence.Context;

public class AppDbContext : IdentityDbContext<User,Role, Guid>
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("User ID =postgres ; Password = eselk00101; Host = localhost;Port =5432; Database = postgres");
    }

    public DbSet<Product>  Products{ get; set; }
    public DbSet<Category>  Categories{ get; set; }
    public DbSet<Brand> Brands{ get; set; }
    public DbSet<Detail> Details { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public override int SaveChanges()
    {
      
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            
            ((BaseEntity)entityEntry.Entity).CreateTime = DateTimeOffset.UtcNow;

           
        }

        return base.SaveChanges();
    }

}