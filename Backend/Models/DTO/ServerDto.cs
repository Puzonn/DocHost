namespace DocHost.Models.DTO;

public class ServerDto
{
    public string Name { get; set; }
    public string Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ContainerPortDto> Ports { get; set; }
}