using DocHost.Models;
using Microsoft.EntityFrameworkCore;

namespace DocHost.Database;

public class HostContext : DbContext
{
    public DbSet<Server> Servers { get; set; }
    
    public string DbPath { get; }
    
    public HostContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "blogging.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}