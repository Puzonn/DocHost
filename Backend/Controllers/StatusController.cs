using DocHost.Models;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocHost.Controllers;

[Controller]
[Route("api/[controller]")]
public class StatusController(DockerClient client) : ControllerBase
{
    private readonly DockerClient _client = client;

    [HttpGet("get-all")]
    public async Task<IEnumerable<ContainerStatusModel>> GetStatus()
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
}