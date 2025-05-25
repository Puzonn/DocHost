using DocHost.Database;
using DocHost.Hubs;
using DocHost.Services;
using Docker.DotNet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(HostService));
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
app.MapControllers();

app.MapHub<ConsoleHub>("/hubs/console");

app.Run();