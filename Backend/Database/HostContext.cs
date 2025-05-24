using DocHost.Models;
using Microsoft.EntityFrameworkCore;

namespace DocHost.Database;

public class HostContext(DbContextOptions<HostContext> options, IConfiguration configuration) : DbContext
{
    public DbSet<Server> Servers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DocHost"));
    }
}