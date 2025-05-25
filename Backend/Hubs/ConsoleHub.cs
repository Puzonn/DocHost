using System.Text;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.SignalR;

namespace DocHost.Hubs;

public class ConsoleHub(DockerClient client) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var containerId = "8da03f40e403cd0bd1e3b9de397ed08fd0b3d2236a2dfab41a67dae8427bb746";

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