using System.Text;
using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.SignalR;

namespace DocHost.Hubs;

public class ConsoleHub(DockerClient client) : Hub
{
    public override async Task OnConnectedAsync()
    {
        for (int i = 0; i < 2; i++)
        {
            var containerId = "6f1e74519366c6e6a5c67169d9fa5de24b60e391a2974256fdef4fece819917a";

            var parameters = new ContainerLogsParameters
            {
                ShowStdout = true,
                ShowStderr = true,
                Tail = "100"
            };

            using (var stream = await client.Containers.GetContainerLogsAsync(containerId, parameters))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string log = await reader.ReadToEndAsync();
                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveLog", log);
            }
        }
        
        await Task.Delay(1000);
    }
}