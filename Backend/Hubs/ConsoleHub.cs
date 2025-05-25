using System.Text;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.SignalR;

namespace DocHost.Hubs;

public class ConsoleHub(DockerClient client) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var id = httpContext.Request.Query["containerid"];
        
        var containerId = id;

        var parameters = new ContainerLogsParameters
        {
            ShowStdout = true,
            ShowStderr = true,
            Tail = "all",
            Follow = true
        };

        var attachParams = new ContainerAttachParameters
        {
            Stream = true,
            Stdin = true,
            Stdout = true,
            Stderr = true,
        };
        
        await client.Containers.GetContainerLogsAsync(containerId, parameters, Context.ConnectionAborted, new Progress<string>(async (message) =>
        {
            await Task.Delay(50);
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveLog", message);
        }));
    }
}