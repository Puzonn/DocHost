using System.ComponentModel.DataAnnotations;

namespace DocHost.Models;

public class Server
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}