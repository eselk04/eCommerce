using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Persistence.Context;

public class AppDbContext : DbContext
{
    public DbSet<Product>  Products{ get; set; }
    
}