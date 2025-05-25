using DocHost.Models;
using DocHost.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocHost.Controllers;

[Controller]
[Route("api/[controller]")]
public class HostController(HostService hostService) : ControllerBase
{
    [HttpPost("minecraft")]
    [Obsolete]
    public async Task CreateMinecraftServer(ServerCreationModel model)
    {
        await hostService.Host(new MinecraftCreationModel()
        {
            ContainerId = Guid.NewGuid().ToString(),
            OwnerId = Guid.NewGuid().ToString(),
            Memory = 1,
            Port = 25565,
            Version = "1.21.1"
        });
    }
}