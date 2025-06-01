using System.ComponentModel.DataAnnotations;

namespace DocHost.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    public string Username { get; set; }
    public string HashedPassword { get;set; }
    
    public List<Server> Servers { get; set; }
}