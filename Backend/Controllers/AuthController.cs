using DocHost.Database;
using DocHost.Models;
using DocHost.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocHost.Controllers;

[Controller]
[Route("api/[controller]")]
public class AuthController(HostContext context) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] LoginRequest login)
    {
        /* Temporary register functionality will stay here until its properly implemented */

        var hasher = new PasswordHasher<User>();
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username == login.Username);
        
        if (user == null)
        {
            user = new User()
            {
                Username = login.Username
            };

            user.HashedPassword = hasher.HashPassword(user, login.Password);

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
        else
        {
            var result = hasher.VerifyHashedPassword(user, user.HashedPassword, login.Password);
            
            if(result != PasswordVerificationResult.Success)
            {
                return Unauthorized("Invalid username or password");
            }
        }
        
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);

        return Ok();
    }
    
    [Authorize]
    [HttpGet("me")]
    public ActionResult<UserDto> Info()
    {
        var username = HttpContext.Session.GetString("Username")!;
        var userId = HttpContext.Session.GetInt32("UserId")!;
        
        return Ok(new UserDto()
        {
            UserId = userId.Value,
            Username = username
        });
    }
}