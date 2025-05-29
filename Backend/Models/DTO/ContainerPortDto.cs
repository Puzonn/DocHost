namespace DocHost.Models.DTO;

/// <summary>
/// Used for exposing port in container mapped like ExposedPort:Port
/// </summary>
public class ContainerPortDto
{
    /// <summary>
    /// Port on the host machine that will be exposed publicly.
    /// </summary>
    public int ExposedPort { get; set; }
    
    /// <summary>
    /// Internal port inside container to which the ExposedPort maps
    /// </summary>
    public int Port { get; set; } 
}