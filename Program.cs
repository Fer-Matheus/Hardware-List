using System.Management;

double ByteToGB(double value)
{
    return Math.Round(value / 1024d / 1024d / 1024d, 2);
}

GetGeneralInfo();
GetRAMInfo();
GetVideoInfo();
GetCPUInfo();

void GetGeneralInfo()
{
    using (ManagementObjectSearcher searcher = new("SELECT * FROM Win32_ComputerSystem"))
    {
        foreach (var mo in searcher.Get())
        {
            Console.WriteLine($"Name: {mo["Name"]}");
            Console.WriteLine($"Manufacturer: {mo["Manufacturer"]}");
            Console.WriteLine($"Model: {mo["Model"]}");
            Console.WriteLine($"Total physical memory: {ByteToGB((ulong)mo["TotalPhysicalMemory"])} GB");
            Console.WriteLine($"Number of processors: {mo["NumberOfProcessors"]}");
            Console.WriteLine();
        }
    }
}

void GetRAMInfo()
{
    using (ManagementObjectSearcher searcher = new("SELECT * FROM Win32_PhysicalMemory"))
    {

        foreach (var mo in searcher.Get())
        {
            Console.WriteLine($"RAM: {ByteToGB((ulong)mo["Capacity"])} GB");
            Console.WriteLine($"Type: {mo["MemoryType"]}");
            Console.WriteLine($"Speed: {mo["Speed"]} MHz");
            Console.WriteLine();
        }
    }
}

static void GetVideoInfo()
{
    using (ManagementObjectSearcher searcher = new("SELECT * FROM Win32_VideoController"))
    {

        foreach (var mo in searcher.Get())
        {
            Console.WriteLine($"Graphics card: {mo["Name"]}");
            Console.WriteLine();
        }
    }
}

static void GetCPUInfo()
{
    using (ManagementObjectSearcher searcher = new("SELECT * FROM Win32_Processor"))
    {

        foreach (var mo in searcher.Get())
        {
            Console.WriteLine($"Processor: {mo["Name"]}");
            Console.WriteLine($"Number of cores: {mo["NumberOfCores"]}");
            Console.WriteLine($"Number of logical processors: {mo["NumberOfLogicalProcessors"]}");
            Console.WriteLine($"Max clock speed: {mo["MaxClockSpeed"]} MHz");
            Console.WriteLine();
        }
    }
}