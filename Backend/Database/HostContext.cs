using System.ComponentModel;
using DocHost.Models;
using Microsoft.EntityFrameworkCore;

namespace DocHost.Database;

public class HostContext(DbContextOptions<HostContext> options, IConfiguration configuration) : DbContext
{
    public DbSet<Server> Servers { get; set; }
    public DbSet<ContainerPort> ExposedPorts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DocHost"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContainerPort>()
            .HasKey(p => p.Id);
        
        modelBuilder.Entity<ContainerPort>()
            .HasOne(p => p.Server)
            .WithMany(c => c.Ports)
            .HasForeignKey(p => p.ServerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}