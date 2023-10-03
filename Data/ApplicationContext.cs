using Contabilizacao.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contabilizacao.Data;
public class ApplicationContext: DbContext
{
    private readonly string _connectionString;
    
    public ApplicationContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(_connectionString, ServerVersion.Parse("8.0.31"));
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductToSupermarketToShift> ProductsToSupermarketToShifts => Set<ProductToSupermarketToShift>();
    public DbSet<Shift> Shifts => Set<Shift>();
    public DbSet<Supermarket> Supermarkets => Set<Supermarket>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Event> Events => Set<Event>();
}