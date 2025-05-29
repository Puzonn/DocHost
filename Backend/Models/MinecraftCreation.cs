namespace DocHost.Models;

public class MinecraftCreation
{
    public string OwnerId { get; set; }
    public string ContainerId { get; set; }
    public int ServerPort { get; set; }
    public int FtpPort { get; set; }
    public string Version { get; set; }
    public int Memory { get; set; }
    public string ContainerName { get; set; }
    public string ImageName { get; set; }
}