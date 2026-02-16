using Microsoft.EntityFrameworkCore;
using Training_Api.Data;

namespace Training_Api.Web.Data;

// public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<State> State { get; init; }
}