using System.ComponentModel.DataAnnotations;

namespace DocHost.Models;

public class LoginRequest
{
    [MaxLength(50)]
    public string Username { get; set; }
    
    [MaxLength(50)]
    public string Password { get; set; }
}