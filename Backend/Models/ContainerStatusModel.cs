using Docker.DotNet.Models;

namespace DocHost.Models;

public class ContainerStatusModel
{
    public string Id { get; set; }
    public string Status { get; set; }
    public string State  { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Port> Ports  { get; set; }
    
    public static readonly ContainerStatusModel Empty = new ContainerStatusModel();
}