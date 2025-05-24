using System.Text;
using DocHost.Models;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocHost.Controllers;

[Controller]
[Route("api/[controller]/")]
public class StatusController(DockerClient client) : ControllerBase
{
    [HttpGet("get-console")]
    public async Task<string> GetConsole()
    {
        var containerId = "e50614fe0e6e";

        var parameters = new ContainerLogsParameters
        {
            ShowStdout = true,
            ShowStderr = true,
            Tail = "100" // Number of last lines
        };

        using (var stream = await client.Containers.GetContainerLogsAsync(containerId, parameters))
        using (var reader = new StreamReader(stream, Encoding.UTF8))
        {
            string log = await reader.ReadToEndAsync();
            return log;
        }
    }

    [HttpPost("send-input")]
    public async Task SendInput([FromQuery]string command)
    {
        string containerId = "e50614fe0e6e";

        var attachParams = new ContainerAttachParameters
        {
            Stream = true,
            Stdin = true,
            Stdout = true,
            Stderr = true,
            
        };

        using var muxedStream = await client.Containers.AttachContainerAsync(
            containerId, true, attachParams, default);

        byte[] input = Encoding.UTF8.GetBytes(command + "\n");
        await muxedStream.WriteAsync(input, 0, input.Length, default);
    }
    
    [HttpGet]
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