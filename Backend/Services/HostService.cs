using DocHost.Models;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace DocHost.Services;

public class HostService(IConfiguration configuration, DockerClient client)
{
    public async Task<CreateContainerResponse> Host(MinecraftCreation request)
    {
        return await client.Containers.CreateContainerAsync(new CreateContainerParameters
        {
            OpenStdin = true,
            AttachStdin = true,
            AttachStdout = true,
            AttachStderr = true,
            Image = request.ImageName,
            Name = request.ContainerName,
            ExposedPorts = new Dictionary<string, EmptyStruct>
            {
                { "21/tcp", default(EmptyStruct) },
            },
            HostConfig = new HostConfig
            {
                PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    {
                        "25565/tcp", //TODO: Check if default minecraft server will be always on this port
                        new List<PortBinding>
                        {
                            new PortBinding
                            {
                                HostPort = request.ServerPort.ToString()
                            }
                        }
                    },
                    {
                    "21/tcp", new List<PortBinding>
                    {
                        new PortBinding
                        {
                            HostPort = request.FtpPort.ToString()
                        },
                    }
                },
                },
            },
        });
    }
}