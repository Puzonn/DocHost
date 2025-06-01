using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using DocHost.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace DocHost.Middlewares;

public class AuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock, IDistributedCache cache)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var sessionCookie = Request.Cookies["doc_host-session"];
        if (string.IsNullOrEmpty(sessionCookie))
        {
            return AuthenticateResult.NoResult();
        }
        
        var userId = Context.Session.GetInt32("UserId");
        var username = Context.Session.GetString("Username");

        if (string.IsNullOrEmpty(username) || userId is null)
        {
            return AuthenticateResult.NoResult();
        }
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.Value.ToString()),
            new Claim(ClaimTypes.Name, username)
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}