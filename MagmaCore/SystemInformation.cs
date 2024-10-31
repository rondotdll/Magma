
using System.Management;

namespace MagmaCore
{
    public enum GpuVendor
    {
        Unknown,
        Amd,
        Nvidia,
        Intel
    }

    public readonly struct GpuMeta
    {
        public GpuVendor Vendor { get; }
        public string Model { get; }

        public GpuMeta(GpuVendor vendor, string model)
        {
            Vendor = vendor;
            Model = model;
        }
    }

    public static class SystemInformation
    {

        private static GpuVendor ParseVendor(string vendorName)
        {
            return vendorName.ToLower() switch
            {
                { } name when name.Contains("amd") => GpuVendor.Amd,
                { } name when name.Contains("nvidia") => GpuVendor.Nvidia,
                { } name when name.Contains("intel") => GpuVendor.Intel,
                _ => GpuVendor.Unknown,
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

                        GpuVendor vendor = ParseVendor(vendorName);
                        return new GpuMeta(vendor, model);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving GPU information: {ex.Message}");
            }
            
            return new GpuMeta(GpuVendor.Unknown, "Unknown");
        }
    }
}
