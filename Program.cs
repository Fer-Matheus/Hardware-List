#pragma warning disable CA1416 // Validate platform compatibility

using System.Management;

List<string> output = [GetGeneralInfo(), GetCPUInfo(), GetVideoInfo(), GetRAMInfo()];

File.WriteAllLines("./file/log.txt", output);

static double ByteToGB(double value)
{
    return Math.Round(value / 1024d / 1024d / 1024d, 2);
}

static string GetGeneralInfo()
{
    string temp = "\n";
    using (ManagementObjectSearcher searcher = new("SELECT * FROM Win32_ComputerSystem"))
    {
        temp += $"###### General Section ######\n\n";
        foreach (var mo in searcher.Get())
        {
            temp += $"Name: {mo["Name"]}\n";
            temp += $"Manufacturer: {mo["Manufacturer"]}\n";
            temp += $"Model: {mo["Model"]}\n";
            temp += $"Total physical memory: {ByteToGB((ulong)mo["TotalPhysicalMemory"])} GB\n";
            temp += $"Number of processors: {mo["NumberOfProcessors"]}\n";
            temp += "\n";
        }
    }
    return temp;
}

static string GetRAMInfo()
{
    string temp = "\n";
    using (ManagementObjectSearcher searcher = new("SELECT * FROM Win32_PhysicalMemory"))
    {
        temp += $"###### RAM Section ######\n\n";
        foreach (var mo in searcher.Get())
        {
            temp += $"RAM: {ByteToGB((ulong)mo["Capacity"])} GB\n";
            temp += $"Type: {mo["MemoryType"]}\n";
            temp += $"Speed: {mo["Speed"]} MHz\n"; 
            temp += "\n";
        }
    }
    return temp;
}

static string GetVideoInfo()
{
    string temp = "\n";
    using (ManagementObjectSearcher searcher = new("SELECT * FROM Win32_VideoController"))
    {
        temp += $"###### Video Section ######\n\n";
        foreach (var mo in searcher.Get())
        {
            temp += $"Graphics card: {mo["Name"]}\n";
            temp += "\n";
        }
    }
    return temp;
}

static string GetCPUInfo()
{
    string temp ="\n";
    using (ManagementObjectSearcher searcher = new("SELECT * FROM Win32_Processor"))
    {
        temp += $"###### General Section ######\n\n";
        foreach (var mo in searcher.Get())
        {
            temp += $"Processor: {mo["Name"]}\n";
            temp += $"Number of cores: {mo["NumberOfCores"]}\n";
            temp += $"Number of logical processors: {mo["NumberOfLogicalProcessors"]}\n";
            temp += $"Max clock speed: {mo["MaxClockSpeed"]} MHz\n";
            temp += "\n";
        }
    }
    return temp;
}
#pragma warning restore CA1416 // Validate platform compatibility