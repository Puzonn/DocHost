using DocHost.Database;
using DocHost.Models;
using DocHost.Models.DTO;
using DocHost.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocHost.Controllers;

[Controller]
[Route("api/[controller]")]
public class ContainerController(ContainerService containerService, HostContext context, ILogger<ContainerController> logger) : ControllerBase
{
    [HttpGet("options")]
    public ActionResult<List<ContainerOption>> Options()
    {
        return Ok(ContainerOption.ContainerOptions);
    }

    [HttpPost("create")]
    public async Task<ActionResult<Server>> CreateContainerByName([FromBody] CreateServerRequest request)
    {
        var option = ContainerOption.ContainerOptions.FirstOrDefault(x => x.ContainerName == request.Type);

        if (option is null)
        {
            return BadRequest("Container name not found");
        }

        var model = new MinecraftCreation()
        {
            ContainerName = request.Name,
            ServerPort = request.ServerPort,
            Version = option.Version,
            Memory = option.Memory,
            OwnerId = Guid.NewGuid().ToString(),
            ContainerId = Guid.NewGuid().ToString(),
            ImageName = option.ImageName,
        };

        try
        {
            await containerService.Host(model);
            
            var server = await context.Servers.AddAsync(new Server()
            {
                Image = option.ImageName,
                CreatedAt = DateTime.UtcNow,
                Name = request.Name,
                Ports =
                [
                    new ContainerPort()
                    {
                        ExposedPort = request.ServerPort,
                        Port = 25565
                    },
                    new ContainerPort()
                    {
                        ExposedPort = request.FtpPort,
                        Port = 21
                    }
                ]
            });

            await context.SaveChangesAsync();

            return Ok(new ServerDto()
            {
                Name = request.Name,    
                CreatedAt = server.Entity.CreatedAt,    
                Image = server.Entity.Image,   
                Ports = server.Entity.Ports.Select(x => new ContainerPortDto()
                {
                    Port = request.ServerPort,
                    ExposedPort = x.ExposedPort,
                }).ToList(),
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString()); 
            return Problem("Something went wrong while creating container");
        }
    }

    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteContainerByName([FromQuery] string containerName)
    {
        var server = await context.Servers
            .FirstOrDefaultAsync(x => x.Name == containerName);

        if (server is not null)
        {
            context.Servers.Remove(server);
            
            await context.SaveChangesAsync();
        }

        var deleteResponse = await containerService.DeleteContainer(containerName);

        if (!deleteResponse.Success)
        {
            return BadRequest(deleteResponse.Error);
        }

        return Ok();
    }

    [HttpPost("start")]
    public async Task StartContainerByName([FromQuery] string containerName)
    {
        await containerService.Start(containerName);  
    }
}