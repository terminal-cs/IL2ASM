using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace IL2ASM
{
    public static class Compiler
    {
        public static string Compile(string Path)
        {
            ModuleDefMD Code = ModuleDefMD.Load(Path);
            StringWriter Writer = new();
            int StringIndex = 0;

            foreach (AssemblyRef Include in Code.GetAssemblyRefs())
            {
                Console.WriteLine($"Including assembly {Include.Name}");
                Writer.WriteLine("%include \"..\\..\\..\\..\\Binary\\Libraries\\" + Include.Name + ".asm\"");
            }
            Writer.WriteLine("\n[bits 32]\n[global Main]\n");

            foreach (TypeDef Class in Code.Types)
            {
                foreach (MethodDef Method in Class.Methods)
                {
                    bool AlreadyInit = false;
                    if (Method.Name == ".ctor")
                    {
                        continue;
                    }
                    Writer.WriteLine(Method.Name + ":");
                    foreach (Instruction Instruction in Method.Body.Instructions)
                    {
                        string Line = Instruction.ToString();
                        if (Instruction.OpCode == OpCodes.Nop)
                        {
                            continue;
                        }
                        if (Instruction.OpCode == OpCodes.Ldstr)
                        {
                            string Data = Line[16..(Line.Length - 1)];
                            Writer.WriteLine("  db \"" + Data + "\", 0xa");
                            Writer.WriteLine("  push byte 0xa");
                            Writer.WriteLine("  push " + Data.Length);
                            continue;
                        }
                        if (Instruction.OpCode == OpCodes.Ldsfld && !AlreadyInit)
                        {
                            Writer.WriteLine("  jmp .cctor");
                            continue;
                        }
                        if (Instruction.OpCode == OpCodes.Stsfld)
                        {
                            continue;
                        }
                        string Arguments = Instruction.Operand == null ? "" : " " + Instruction.Operand.ToString().Split(" ")[1].Split("(")[0].Replace("::", ".");
                        Writer.WriteLine("  " + Instruction.OpCode + Arguments);
                    }
                }
            }

            Writer.WriteLine("\ntimes 510-($-$$) db 0\ndw 0xAA55");
            return Writer.ToString()[0..(Writer.ToString().Length - 2)];
        }
    }
}