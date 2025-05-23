using DocHost.Database;
using DocHost.Services;
using Docker.DotNet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(HostService));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(e =>
    {
        e.AllowAnyHeader();
        e.AllowAnyMethod();
        e.AllowAnyOrigin();
    });
});

builder.Services.AddDbContext<HostContext>();

builder.Services.AddSingleton<DockerClient>((e) => new DockerClientConfiguration(
        new Uri(builder.Configuration["Docker:DockerUrl"]!))
    .CreateClient());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();