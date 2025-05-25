using DocHost.Models;
using DocHost.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocHost.Controllers;

[Controller]
[Route("api/[controller]")]
public class ContainerController(HostService hostService) : ControllerBase
{
    [HttpGet("options")]
    public ActionResult<List<ContainerOption>> Options()
    {
        return Ok(ContainerOption.ContainerOptions);
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateContainerByName([FromQuery] string type, [FromQuery] string name, [FromQuery] int port)
    {
        var option = ContainerOption.ContainerOptions.FirstOrDefault(x => x.ContainerName == type);

        if (option is null)
        {
            return BadRequest("Container name not found");
        }

        var model = new MinecraftCreationModel()
        {
            ContainerName = name,
            Port = port,
            Version = option.Version,
            Memory = option.Memory,
            OwnerId = Guid.NewGuid().ToString(),
            ContainerId = Guid.NewGuid().ToString(),
        };

        await hostService.Host(model);
        
        return Ok();
    }
}