using DocHost.Models;
using Microsoft.EntityFrameworkCore;

namespace DocHost.Database;

public class HostContext(DbContextOptions<HostContext> options, IConfiguration configuration) : DbContext
{
    public DbSet<Server> Servers { get; set; }
    public DbSet<ContainerPort> ExposedPorts { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DocHost"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContainerPort>()
            .HasOne(p => p.Server)
            .WithMany(c => c.Ports)
            .HasForeignKey(p => p.ServerId);

        modelBuilder.Entity<Server>()
            .HasOne(s => s.Owner)
            .WithMany(u => u.Servers)
            .HasForeignKey(s => s.OwnerId);
    }
}