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
            Image = minecraftCreation.ImageName,
            Name = minecraftCreation.ContainerName,
            ExposedPorts = new Dictionary<string, EmptyStruct>
            {
                { "21/tcp", default(EmptyStruct) },
            },
            HostConfig = new HostConfig
            {
                PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    { $"{minecraftCreation.Port}/tcp", new List<PortBinding> { new PortBinding { HostPort = minecraftCreation.Port.ToString() } } },
                    {
                    "21/tcp", new List<PortBinding>
                    {
                        new PortBinding { HostPort = "2121" }
                    }
                },
                },
            },
        });
    }
}