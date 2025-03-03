using System.IO;
using System.IO.Compression;

namespace Builder.Core
{
    internal class HelperCompress
    {
        public static byte[] Compress(byte[] data)
        {
            using (var compressedStream = new MemoryStream())
            using (var compressionStream = new DeflateStream(compressedStream, CompressionMode.Compress))
            {
                compressionStream.Write(data, 0, data.Length);
                compressionStream.Close();
                return compressedStream.ToArray();
            }
        }

    }
}
