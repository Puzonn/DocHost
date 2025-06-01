using DocHost.Database;
using DocHost.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocHost.Controllers;

[Controller]
[Route("api/[controller]")]
public class UserController(HostContext context) : ControllerBase
{
    [Authorize]
    [HttpGet("get-all")]
    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        return context.Users.Select(user => new UserDto()
        {
            UserId = user.Id,
            CreatedAt = user.CreatedAt,
            Username = user.Username,
            Role = user.Role,
        });
    }
}