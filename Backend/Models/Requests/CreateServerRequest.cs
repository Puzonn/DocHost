namespace DocHost.Models;

/// <summary>
/// Used for creation a server in /admin
/// </summary>
public class CreateServerRequest
{
    public string Name { get; set; }
    public int ServerPort { get; set; }
    public string Type { get; set; }
    public int FtpPort { get; set; }
}