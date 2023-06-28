using Microsoft.EntityFrameworkCore;

namespace Main.Models;

public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }

    public DbSet<Counter> Counters { get; set; } = null!;
}
