using dnlib.DotNet;
using dnlib.DotNet.Emit;
using IL2ASM.Instructions;

namespace IL2ASM
{
    public static class Compiler
    {
        static List<Inst> Instructions=new() {
            new Ldstr("ldstr")
        };

        public static int SIndex=0;
        public static int VIndex=0;


        //                  class             method       instructions
        static Dictionary<string, Dictionary<string, List<string>>> ASM=new();

        public static string Compile(string Path)
        {
            StringWriter Writer = new();
            ModuleDefMD Code = ModuleDefMD.Load(Path);

            foreach (var Import in Code.GetAssemblyRefs())
            {
                Writer.WriteLine("%include \"Libraries" + Tools.Separator + Import.Name.Replace(".", Tools.Separator) + ".asm\"");
            }
            Writer.WriteLine("\njmp Kernel.Main");
            Writer.WriteLine("");
            int Index=0;
            foreach (var Class in Code.Types)
            {
                if (Class.Name == "<Module>")
                {
                    continue;
                }
                ASM.Add(Class.Name, new());
                foreach (MethodDef Method in Class.Methods)
                {
                    if (Method.Name == ".ctor")
                    {
                        continue;
                    }
                    Method.Name=FormatMName(Method.Name);

                    ASM[Class.Name].Add(Method.Name, new());


                    foreach (Instruction Instr in Method.Body.Instructions)
                    {
                        foreach (Inst instr in Instructions) {
                            if (instr.IName==Instr.OpCode.Name) {
                                instr.Execute(Method, Instr, Index, Class.Name, ASM);
                            }
                        }
                        Index++;
                    }
                }
            }

            foreach (KeyValuePair<string, Dictionary<string, List<string>>> Classes in ASM) {
                Writer.WriteLine(Classes.Key+":");
                foreach (KeyValuePair<string, List<string>> Methods in Classes.Value) {
                    Writer.WriteLine("\t."+Methods.Key+":");
                    foreach (string inst in Methods.Value) {
                        Writer.WriteLine("\t\t"+inst);
                    }
                }
            }

            return Writer.ToString();
        }

        public static string FormatMName(string name) {
            return name.Replace("<", "").Replace(">", "").Replace("$", "");
        }
    }
}
