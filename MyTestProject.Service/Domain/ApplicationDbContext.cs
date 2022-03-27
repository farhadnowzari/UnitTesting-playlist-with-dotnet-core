using Microsoft.EntityFrameworkCore;
using MyTestProject.Service.Domain.Entities;

namespace MyTestProject.Service.Domain;

public class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; private set; }
    public DbSet<OrderItem> OrderItems { get; private set; }
    public DbSet<Product> Products { get; private set; }
    public DbSet<Stock> Stocks { get; private set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}