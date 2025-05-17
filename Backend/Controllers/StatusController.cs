using DocHost.Models;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocHost.Controllers;

[Controller]
[Route("api/[controller]/status")]
public class StatusController(DockerClient client) : ControllerBase
{
    [HttpGet("")]
    public async Task<IEnumerable<ContainerStatusModel>> GetAllStatus()
    {
        IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
            new ContainersListParameters()
            {
                All = true
            });

        return containers.Select(x => new ContainerStatusModel()
        {
            Id = x.ID,
            Name = string.Join(", ", x.Names),
            State = x.State,
            Status = x.Status,
            CreatedAt = x.Created,
            Ports = x.Ports.ToList()
        });
    }

    [HttpGet("{id}")]
    public async Task<ContainerStatusModel> GetStatus(string id)
    {
        IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(new());
        var container = containers.FirstOrDefault(x => x.ID == id);

        if (container == null)
        {
            return ContainerStatusModel.Empty;  
        }
        
        return new ContainerStatusModel()
        {
            Id = container.ID,
            Name = string.Join(", ", container.Names),
            State = container.State,
            Status = container.Status,
            CreatedAt = container.Created,
            Ports = container.Ports.ToList()
        };
    }
}