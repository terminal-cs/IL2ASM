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

            foreach (TypeDef Class in Code.Types)
            {
                foreach (MethodDef Method in Class.Methods)
                {
                    Writer.WriteLine(Method.FullName + ":");
                    foreach (Local Variable in Method.Body.Variables)
                    {
                        Writer.WriteLine("  " + Variable.Name + ":");
                        Writer.WriteLine("    db Type " + Variable.Type.ToString());
                        Writer.WriteLine("    db Value " + Variable.ToString());
                        Writer.WriteLine("    d");
                    }
                    foreach (var Instruction in Method.Body.Instructions)
                    {
                        if (Instruction.OpCode == OpCodes.Nop)
                        {
                            continue;
                        }
                        Writer.WriteLine(Instruction.ToString());
                    }
                }
            }

            return Writer.ToString();
        }
    }
}