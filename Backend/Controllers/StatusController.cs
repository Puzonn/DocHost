using System.Text;
using DocHost.Models;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocHost.Controllers;

[Controller]
[Route("api/[controller]/")]
public class StatusController(DockerClient client) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<ContainerStatusModel>> GetAllStatus()
    {
        IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
            new ContainersListParameters()
            {
                All = true
            });

        return containers.Select(x => new ContainerStatusModel()
        {
            ContainerId = x.ImageID,
            Id = x.ID,
            Name = string.Join(", ", x.Names),
            State = x.State,
            Status = x.Status,
            CreatedAt = x.Created,
            Ports = x.Ports.ToList()
        });
    }

    [HttpPost("send-input")]
    [Authorize]
    public async Task SendInput([FromQuery]string command, [FromQuery]string id)
    {
        string containerId = id;

        var attachParams = new ContainerAttachParameters
        {
            Stream = true,
            Stdin = true,
            Stdout = true,
            Stderr = true,
        };

        using var muxedStream = await client.Containers.AttachContainerAsync(
            containerId, true, attachParams, CancellationToken.None);

        byte[] input = Encoding.UTF8.GetBytes(command + "\n");
        await muxedStream.WriteAsync(input, 0, input.Length, CancellationToken.None);
    }
    
    [HttpGet("{id}")]
    [Authorize]
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