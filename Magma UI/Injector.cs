using System;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Magma
{
    public class Injection
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        const uint PROCESS_CREATE_THREAD = 0x0002;
        const uint PROCESS_QUERY_INFORMATION = 0x0400;
        const uint PROCESS_VM_OPERATION = 0x0008;
        const uint PROCESS_VW_WRITE = 0x0020;
        const uint PROCESS_VW_READ = 0x00010;

        const uint MEM_COMMIT = 0x1000;
        const uint MEM_RESERVE = 0x2000;
        const uint PAGE_READWRITE = 0x04;

        public static void BasicInject(string dllPath, int procid)
        {
            IntPtr hProc = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION, false, procid);
            IntPtr Address = VirtualAllocEx(hProc, IntPtr.Zero, (uint)dllPath.Length, MEM_COMMIT, PAGE_READWRITE);
            
            byte[] buffer = Encoding.ASCII.GetBytes(dllPath);
            WriteProcessMemory(hProc, Address, buffer, (uint)buffer.Length, out int lpNumberOfBytesWritten);

            CreateRemoteThread(hProc, IntPtr.Zero, 0, LoadLibrary(dllPath), Address, 0, IntPtr.Zero);
        }

        public static Process[] GetProcesses(string name)
        {
            Process[] processes = Process.GetProcessesByName(name);
            if (processes.Length > 0)
            {
                return processes;
            }
            return null;
        }
    }
}
