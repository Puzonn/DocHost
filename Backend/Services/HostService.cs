using DocHost.Models;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace DocHost.Services;

public class HostService(IConfiguration configuration, DockerClient client)
{
    public async Task<(bool Success, string? Error)> DeleteContainer(string containerName)
    {
        try
        {
            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters
            {
                All = true
            });

            var container = containers.FirstOrDefault(c =>
                c.Names.Any(n => n.TrimStart('/').Equals(containerName, StringComparison.OrdinalIgnoreCase)));

            if (container == null)
            {
                return (false, $"Container '{containerName}' not found.");
            }

            if (container.State == "running")
            {
                await client.Containers.StopContainerAsync(container.ID, new ContainerStopParameters());
            }

            await client.Containers.RemoveContainerAsync(container.ID, new ContainerRemoveParameters
            {
                Force = true
            });

            return (true, null);
        }
        catch (DockerApiException ex)
        {
            return (false, $"Docker API error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return (false, $"Unexpected error: {ex.Message}");
        }
    }
    
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