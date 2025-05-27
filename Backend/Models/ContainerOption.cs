namespace DocHost.Models;

public class ContainerOption(string name, int memory, int vcpus, string version, string imageName)
{
    public static List<ContainerOption> ContainerOptions { get; } = 
        [
            new ContainerOption("1.21.1 2GB 1V", 2, 1, "1.21.1", "mc-1.21.5"),
            new ContainerOption("1.21.1 4GB 1V", 4, 1, "1.21.1", "mc-1.21.5"),
            new ContainerOption("1.21.1 6GB 2V", 6, 2, "1.21.1", "mc-1.21.5"),
        ];

    public string ImageName { get; set; } = imageName;
    public string ContainerName { get; set; } = name;
    public string Version { get; set; } = version;
    public int Memory { get; set; } = memory;
    public int VCpus { get; set; } = vcpus;
}