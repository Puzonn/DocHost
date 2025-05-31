using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DocHost.Models;

/// <summary>
/// Used for exposing port in container mapped like ExposedPort:Port
/// </summary>
public class ContainerPort
{
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Port on the host machine that will be exposed publicly.
    /// </summary>
    public int ExposedPort { get; set; }
    
    /// <summary>
    /// Internal port inside container to which the ExposedPort maps
    /// </summary>
    public int Port { get; set; }

    [JsonIgnore]
    public int ServerId { get; set; }
    
    [JsonIgnore]
    public Server Server { get; set; } 
}