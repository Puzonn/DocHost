using DocHost.Database;
using DocHost.Hubs;
using DocHost.Middlewares;
using DocHost.Services;
using Docker.DotNet;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:RedisUrl"]; 
    options.InstanceName = "DcoHost";
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "doc_host-session";
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

builder.Services.AddSignalR();
builder.Services.AddAuthentication("Session")
    .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>("Session", null);
builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(ContainerService));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(e =>
    {
        e.WithOrigins("http://localhost:5173");
        e.AllowAnyHeader();
        e.AllowAnyMethod();
        e.AllowCredentials();
    });
});

builder.Services.AddSingleton<DockerClient>((e) => new DockerClientConfiguration(
        new Uri(builder.Configuration["Docker:DockerUrl"]!))
    .CreateClient());

builder.Services.AddDbContextPool<HostContext>(opt => { });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseSession();
app.UseMiddleware<RedisSessionAuthenticationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<ConsoleHub>("/hubs/console");

app.Run();