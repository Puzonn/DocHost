namespace DocHost.Models;

public class MinecraftCreationModel
{
    public string OwnerId { get; set; }
    public string ContainerId { get; set; }
    public int Port { get; set; }
    public string Version { get; set; }
    public int Memory { get; set; }
    public string ContainerName { get; set; }
    public string ImageName { get; set; }
}