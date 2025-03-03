using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Builder.Core
{
    internal class Obfuscator
    {
        private static Random random = new Random();
        public static void PerformObfuscationCommon(string outputFile, Action<ModuleDef> obfuscationAction)
        {
            string directory = Path.GetDirectoryName(outputFile);
            string originalFileName = Path.GetFileName(outputFile);
            string moduleNew = Path.Combine(directory, $"tmp_{originalFileName}");
            try
            {
                File.Copy(outputFile, moduleNew, overwrite: true);
                using (ModuleDef module = ModuleDefMD.Load(moduleNew))
                {
                    obfuscationAction(module);
                    module.Write(outputFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Obfuscation failed: {ex.Message}\nFailed method: {ex.TargetSite}");
            }
            finally
            {
                File.Delete(moduleNew);
            }
        }
        public static void PerformObfuscatePacker(string outputFile)
        {
            PerformObfuscationCommon(outputFile, module =>
            {
                JunkMethods.Execute(module, RandomUtils.RandomNumber(4, 8), RandomUtils.RandomNumber(5, 10), 7);
                RenameProtector.Execute(module);
                AntiDe4dot.Execute(module);
                ControlFlow.Execute(module);
            });
        }
        public static void PerformObfuscation(string outputFile)
        {
            PerformObfuscationCommon(outputFile, module =>
            {
                JunkMethods.Execute(module, RandomUtils.RandomNumber(20, 30), RandomUtils.RandomNumber(10, 20), 16);
                RenameProtector.Execute(module);
                FakeAttributes.Execute(module);
                EncryptStrings.Execute(module);
                AntiDe4dot.Execute(module);
                ProxyString.Execute(module);
                ControlFlow.Execute(module);
            });
        }

        public static class RandomUtils
        {
            public static string RandomString(int length)
            {
                const string chars =
                    "abcdefghijklmnopqrstuvwxyz" +
                    "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                    "0123456789" +
                    "∪ ƒ ∫ ∬ ∭ ∮ ∯ ∰ ∱ ∲ ∳" +
                    "⋦ ⋧ ⋨ ⋩ ⋪ ⋫ ⋬ ⋭ ⋮ ⋯ ⋰ ⋱" +
                    "!@#$%^&*()_-+={[}]|:;<,>.?" +
                    "M.Y.N.A.M.E....I.S...I.D.I.N.A.H.U.I" +
                    "Ցց Ււ Փփ Քք Օօ Ֆֆ Φ φ Χ χ Ψ ψ Ω ω ρ Σ σ ς Τ τ Υ Ϋ υ ϋ (ם) נ (ן) ס ע פ (ף) צ (ץ) ק ר ש ת";

                char[] result = new char[length];
                for (int i = 0; i < length; i++)
                {
                    result[i] = chars[random.Next(chars.Length)];
                }

                return new string(result.OrderBy(_ => random.Next()).ToArray());
            }

            public static int RandomNumber(int minValue, int maxValue)
            {
                if (minValue >= maxValue)
                    throw new ArgumentOutOfRangeException("minValue", "minValue must be less than maxValue");

                return random.Next(minValue, maxValue);
            }
        }
        public class RenameProtector
        {
            public static int count_xxx = 0;

            public static void Execute(ModuleDef module)
            {
                try
                {
                    module.Name = RandomUtils.RandomString(7);

                    foreach (var type in module.Types)
                    {
                        if (type.IsGlobalModuleType || type.IsRuntimeSpecialName || type.IsSpecialName || type.IsWindowsRuntime || type.IsInterface)
                            continue;

                        count_xxx++;
                        type.Name = RandomUtils.RandomString(40);
                        type.Namespace = "";

                        foreach (var property in type.Properties)
                        {
                            count_xxx++;
                            property.Name = RandomUtils.RandomString(40);
                        }

                        foreach (var field in type.Fields)
                        {
                            count_xxx++;
                            field.Name = RandomUtils.RandomString(40);
                        }

                        foreach (var eventDef in type.Events)
                        {
                            count_xxx++;
                            eventDef.Name = RandomUtils.RandomString(40);
                        }

                        foreach (var method in type.Methods)
                        {
                            if (method.IsConstructor) continue;
                            count_xxx++;
                            method.Name = RandomUtils.RandomString(40);

                            foreach (var param in method.ParamDefs)
                            {
                                count_xxx++;
                                param.Name = RandomUtils.RandomString(40);
                            }

                            if (method.HasBody)
                            {
                                foreach (var local in method.Body.Variables)
                                {
                                    count_xxx++;
                                    local.Name = RandomUtils.RandomString(40);
                                }

                                foreach (var instr in method.Body.Instructions)
                                {
                                    if (instr.OpCode == OpCodes.Ldloc || instr.OpCode == OpCodes.Stloc)
                                    {
                                        var localVar = instr.Operand as Local;
                                        if (localVar != null && localVar.Name != null)
                                        {
                                            count_xxx++;
                                            localVar.Name = RandomUtils.RandomString(40);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during renaming: {ex.Message}");
                }
            }
        }
        public class JunkMethods
        {
            public static void Execute(ModuleDef module, int junkClasses, int junkMethodsPerClass, int junkInstructionsPerMethod)
            {
                Random random = new Random();

                for (int i = 0; i < junkClasses; i++)
                {
                    TypeDef junkClass = new TypeDefUser($"JunkClass_{Guid.NewGuid().ToString("N")}");
                    module.Types.Add(junkClass);

                    for (int j = 0; j < junkMethodsPerClass; j++)
                    {
                        MethodDef junkMethod = new MethodDefUser($"JunkMethod_{Guid.NewGuid().ToString("N")}",
                            MethodSig.CreateStatic(module.CorLibTypes.Void),
                            MethodAttributes.Public | MethodAttributes.Static);

                        junkClass.Methods.Add(junkMethod);

                        for (int k = 0; k < junkInstructionsPerMethod; k++)
                        {
                            junkMethod.Body = new CilBody();
                            junkMethod.Body.Instructions.Add(OpCodes.Nop.ToInstruction());
                            junkMethod.Body.Instructions.Add(OpCodes.Ldc_I4_0.ToInstruction());
                            junkMethod.Body.Instructions.Add(OpCodes.Pop.ToInstruction());
                        }

                        junkMethod.Body.Instructions.Add(OpCodes.Ret.ToInstruction());
                    }
                }
            }
        }
        public class EncryptStrings
        {
            public static void Execute(ModuleDef module)
            {
                foreach (var type in module.Types)
                {
                    foreach (var method in type.Methods)
                    {
                        if (method.Body == null) continue;

                        var instructions = method.Body.Instructions;

                        for (int i = 0; i < instructions.Count; i++)
                        {
                            if (instructions[i].OpCode == OpCodes.Ldstr)
                            {
                                string oldString = instructions[i].Operand.ToString();
                                string newString = Convert.ToBase64String(Encoding.UTF8.GetBytes(oldString));

                                instructions[i].OpCode = OpCodes.Nop;

                                instructions.Insert(i + 1, new Instruction(OpCodes.Call, module.Import(typeof(Encoding).GetMethod("get_UTF8"))));
                                instructions.Insert(i + 2, new Instruction(OpCodes.Ldstr, newString));
                                instructions.Insert(i + 3, new Instruction(OpCodes.Call, module.Import(typeof(Convert).GetMethod("FromBase64String", new Type[] { typeof(string) }))));
                                instructions.Insert(i + 4, new Instruction(OpCodes.Callvirt, module.Import(typeof(Encoding).GetMethod("GetString", new Type[] { typeof(byte[]) }))));
                                i += 4;
                            }
                        }
                    }
                }
            }
        }
        public class AntiDe4dot
        {
            public static void Execute(ModuleDef md)
            {
                foreach (var moduleDef in md.Assembly.Modules)
                {
                    var interfaceM = new InterfaceImplUser(md.GlobalType);
                    for (var i = 0; i < 4; i++)
                    {
                        var typeDef1 = new TypeDefUser(string.Empty, $"Form{i}", md.CorLibTypes.GetTypeRef("System", "Attribute"));
                        var interface1 = new InterfaceImplUser(typeDef1);
                        md.Types.Add(typeDef1);
                        typeDef1.Interfaces.Add(interface1);
                        typeDef1.Interfaces.Add(interfaceM);
                    }
                }
            }
        }
        public class FakeAttributes
        {
            private static string[] ProtectorAttributes = { 
                "Xenocode.Client.Attributes.AssemblyAttributes.ProcessedByXenocode",
                "CryptoObfuscator.ProtectedWithCryptoObfuscatorAttribute",
                "SecureTeam.Attributes.ObfuscatedByAgileDotNetAttribute",
                "Xenocode.Client.Attributes.AssemblyAttributes",
                "SmartAssembly.Attributes.PoweredByAttribute",
                "ObfuscatedByAgileDotNetAttribute",
                "NineRays.Obfuscator.Evaluation",
                "ObfuscatedByCliSecureAttribute",
                "BabelObfuscatorAttribute",
                "AsStrongAsFuckAttribute",
                "ProtectedByDotnetsafer",
                "Macrobject.Obfuscator",
                "DotfuscatorAttribute",
                "CodeWallTrialVersion",
                "ConfusedByAttribute",
                "ObfuscatedByGoliath",
                "NETSpider.Attribute",
                "NineRays.Obfuscator",
                "PoweredByAttribute",
                "RustemSoft.Skater",
                "BabelAttribute",
                "MRuntime3.dll",
                "YanoAttribute",
                "EMyPID_8234_",
                "ZYXDNGuarder",
                "SkiDzEX",
                "Sixxpack",
                "____KILL",
                "GeneratedWithBatchToApp",
                "ObfuscationAttribute",
                "'Obfuscated'",
                "'ByAttribute'",
                "'ObfuscatorAttribute'",
                "'obfuscated'",
                "CheckRemoteDebuggerPresent",
                "IsDebuggerPresent",
                "VirtualProtect",
                "Reactor",
            };

            public static void Execute(ModuleDef module)
            {
                for (int i = 0; i < ProtectorAttributes.Length; i++)
                {
                    var fakeAttrib = new TypeDefUser(ProtectorAttributes[i], ProtectorAttributes[i], module.CorLibTypes.Object.TypeDefOrRef)
                    {
                        Attributes = TypeAttributes.Class | TypeAttributes.NotPublic | TypeAttributes.WindowsRuntime
                    };
                    module.Types.Add(fakeAttrib);
                }
            }
        }
        public class ProxyString
        {
            public static void Execute(ModuleDef module)
            {
                try
                {
                    foreach (var type in module.GetTypes())
                    {
                        if (type.IsGlobalModuleType) continue;

                        foreach (var meth in type.Methods)
                        {
                            if (!meth.HasBody) continue;

                            var instr = meth.Body.Instructions;
                            var newInstructions = new List<Instruction>();

                            foreach (var t in instr)
                            {
                                if (t.OpCode == OpCodes.Ldstr)
                                {
                                    var methImplFlags = MethodImplAttributes.IL | MethodImplAttributes.Managed;
                                    var methFlags = MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig | MethodAttributes.ReuseSlot;
                                    var meth1 = new MethodDefUser(RandomUtils.RandomString(10),
                                        MethodSig.CreateStatic(module.CorLibTypes.String),
                                        methImplFlags, methFlags);

                                    module.GlobalType.Methods.Add(meth1);

                                    meth1.Body = new CilBody();
                                    meth1.Body.Variables.Add(new Local(module.CorLibTypes.String));
                                    meth1.Body.Instructions.Add(Instruction.Create(OpCodes.Ldstr, t.Operand.ToString()));
                                    meth1.Body.Instructions.Add(Instruction.Create(OpCodes.Ret));

                                    t.OpCode = OpCodes.Call;
                                    t.Operand = meth1;
                                }
                                newInstructions.Add(t);
                            }

                            meth.Body.Instructions.Clear();
                            foreach (var instruction in newInstructions)
                            {
                                meth.Body.Instructions.Add(instruction);
                            }

                            meth.Body.UpdateInstructionOffsets();
                            meth.Body.SimplifyBranches();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during string proxying: {ex.Message}");
                }
            }
        }
        public class ControlFlow
        {
            private static Random random = new Random();

            public static void Execute(ModuleDef module)
            {
                try
                {
                    foreach (var type in module.Types)
                    {
                        foreach (var method in type.Methods)
                        {
                            if (!method.HasBody || method.Body.Instructions.Count == 0) continue;

                            var instructions = method.Body.Instructions;
                            var local1 = new Local(method.Module.CorLibTypes.Int32);
                            var local2 = new Local(method.Module.CorLibTypes.Int32);
                            method.Body.Variables.Add(local1);
                            method.Body.Variables.Add(local2);

                            var firstInstruction = instructions[0];

                            instructions.Insert(0, Instruction.Create(OpCodes.Ldc_I4, random.Next(1, 100)));
                            instructions.Insert(1, Instruction.Create(OpCodes.Stloc, local1));
                            instructions.Insert(2, Instruction.Create(OpCodes.Ldc_I4, random.Next(1, 100)));
                            instructions.Insert(3, Instruction.Create(OpCodes.Stloc, local2));

                            var newInstructions = new List<Instruction>
                            {
                                Instruction.Create(OpCodes.Ldloc, local1),
                                Instruction.Create(OpCodes.Ldloc, local2),
                                Instruction.Create(OpCodes.Add),
                                Instruction.Create(OpCodes.Stloc, local1),
                                Instruction.Create(OpCodes.Ldloc, local1),
                                Instruction.Create(OpCodes.Ldc_I4, random.Next(1, 50)),
                                Instruction.Create(OpCodes.Bgt, firstInstruction),
                                Instruction.Create(OpCodes.Ldloc, local2),
                                Instruction.Create(OpCodes.Ldc_I4, random.Next(1, 50)),
                                Instruction.Create(OpCodes.Blt, firstInstruction),
                                Instruction.Create(OpCodes.Ldloc, local1),
                                Instruction.Create(OpCodes.Ldloc, local2),
                                Instruction.Create(OpCodes.Mul),
                                Instruction.Create(OpCodes.Stloc, local1),
                                Instruction.Create(OpCodes.Ldloc, local1),
                                Instruction.Create(OpCodes.Ldc_I4, random.Next(1, 100)),
                                Instruction.Create(OpCodes.Beq, firstInstruction),
                            };

                            for (int i = 0; i < newInstructions.Count; i++)
                            {
                                instructions.Insert(4 + i, newInstructions[i]);
                            }

                            instructions.Insert(0, Instruction.Create(OpCodes.Nop));
                            instructions.Insert(1, Instruction.Create(OpCodes.Ldc_I4, 12345));
                            instructions.Insert(2, Instruction.Create(OpCodes.Pop));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during control flow obfuscation: {ex.Message}");
                }
            }
        }
    }
}