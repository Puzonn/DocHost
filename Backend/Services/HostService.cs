using DocHost.Models;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace DocHost.Services;

public class HostService(IConfiguration configuration, DockerClient client)
{
    public async Task<CreateContainerResponse> Host(MinecraftCreationModel minecraftCreation)
    {
        await client.Images.CreateImageAsync(
            new ImagesCreateParameters { FromImage = "itzg/minecraft-server", Tag = "latest" },
            null,
            new Progress<JSONMessage>(m => Console.WriteLine(m.Status))
        );

        return await client.Containers.CreateContainerAsync(new CreateContainerParameters
        {
            Image = "itzg/minecraft-server:latest",
            Name = minecraftCreation.ContainerName,
            Env = new List<string>
            {
                "EULA=TRUE",
                $"VERSION={minecraftCreation.Version}",
                $"MEMORY={minecraftCreation.Memory}G"
            },
            HostConfig = new HostConfig
            {
                PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    { $"{minecraftCreation.Port}/tcp", new List<PortBinding> { new PortBinding { HostPort = minecraftCreation.Port.ToString() } } }
                },
            },
            ExposedPorts = new Dictionary<string, EmptyStruct>
            {
                { $"{minecraftCreation.Port}/tcp", default }
            }
        });
    }
}