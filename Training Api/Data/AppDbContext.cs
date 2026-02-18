using Microsoft.EntityFrameworkCore;

namespace Training_Api.Data;

// public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<State> State { get; init; }
    public DbSet<City> City { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Type t = typeof(AppDbContext);
        modelBuilder.ApplyConfigurationsFromAssembly(t.Assembly);

        base.OnModelCreating(modelBuilder);
    }
}