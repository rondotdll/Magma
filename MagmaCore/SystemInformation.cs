
using System.Management;

namespace MagmaCore
{
    public enum HardwareVendor
    {
        Unknown,
        Amd,
        Nvidia,
        Intel,
    }

    public readonly struct GpuMeta
    {
        public HardwareVendor Vendor { get; }
        public string Model { get; }

        public GpuMeta(HardwareVendor vendor, string model)
        {
            Vendor = vendor;
            Model = model;
        }
    }

    public static class SystemInformation
    {

        private static HardwareVendor ParseVendor(string vendorName)
        {
            return vendorName.ToLower() switch
            {
                { } name when name.Contains("amd") => HardwareVendor.Amd,
                { } name when name.Contains("nvidia") => HardwareVendor.Nvidia,
                { } name when name.Contains("intel") => HardwareVendor.Intel,
                _ => HardwareVendor.Unknown,
            };
        }
        
        public static GpuMeta GetGpu()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        string model = obj["Name"]?.ToString() ?? "Unknown";
                        string vendorName = obj["AdapterCompatibility"]?.ToString() ?? "Unknown";

                        HardwareVendor vendor = ParseVendor(vendorName);
                        return new GpuMeta(vendor, model);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving GPU information: {ex.Message}");
            }
            
            return new GpuMeta(HardwareVendor.Unknown, "Unknown");
        }
    }
}
