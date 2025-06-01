namespace DocHost.Models.DTO;

public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Role { get; set; } 
    public DateTime CreatedAt { get; set; } 
}