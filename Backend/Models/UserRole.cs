using System.ComponentModel.DataAnnotations;

namespace DocHost.Models;

public class UserRole
{
    [Key]
    public int Id { get; set; }
    public string RoleName { get; set; }
}