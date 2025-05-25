using DocHost.Models;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace DocHost.Services;

public class HostService(IConfiguration configuration, DockerClient client)
{
    public async Task<CreateContainerResponse> Host(MinecraftCreationModel minecraftCreation)
    {
        return await client.Containers.CreateContainerAsync(new CreateContainerParameters
        {
            OpenStdin = true,
            AttachStdin = true,
            AttachStdout = true,
            AttachStderr = true,
            Image = "mc-1.21.5",
            Name = minecraftCreation.ContainerName,
            HostConfig = new HostConfig
            {
                PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    { $"{minecraftCreation.Port}/tcp", new List<PortBinding> { new PortBinding { HostPort = minecraftCreation.Port.ToString() } } }
                },
            },
        });
    }
}