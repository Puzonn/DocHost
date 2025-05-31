namespace DocHost.Models;

public class ServerStatus
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public int OwnerId { get; set; }
    public string OwnerUsername { get; set; }
    
    public string ContainerId { get; set; }
    public string Status { get; set; }
    public string State { get; set; }
    
    public List<ContainerPort> Ports { get; set; }
}