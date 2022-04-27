using dnlib.DotNet;

namespace IL2ASM
{
    public static class IL2ASM
    {
        public static string Compile(string Path)
        {
            StringWriter Writer = new();
            ModuleDefMD Code = ModuleDefMD.Load(Path);

            foreach (var Import in Code.GetAssemblyRefs())
            {
                Writer.WriteLine("%include \"Libraries" + Tools.Separator + Import.Name.Replace(".", Tools.Separator) + ".asm\"");
            }
            Writer.WriteLine("jmp Kernel.Main");
            Writer.WriteLine("");
            foreach (var Class in Code.Types)
            {
                if (Class.Name == "<Module>")
                {
                    continue;
                }
                Writer.WriteLine(Class.Name + ":");
                foreach (var Method in Class.Methods)
                {
                    if (Method.Name == ".ctor")
                    {
                        continue;
                    }
                    Method.Name = FormatMName(Method.Name);
                    Writer.WriteLine("\t." + Method.Name + ":");

                    foreach (var Call in Method.Body.Instructions)
                    {
                        if (Call.OpCode.Name == "ldstr")
                        {
                            Writer.WriteLine("    push \"" + Call.Operand + "\"");
                            continue;
                        }
                        if (Call.IsStarg() || Call.IsStloc() || Call.IsLdarg() || Call.IsLdloc() || Call.OpCode.Name == "nop")
                        {
                            continue;
                        }
                        Writer.WriteLine("    " + Call.OpCode + " " + Call.Operand);
                    }
                    Writer.WriteLine("");
                }
            }

            return Writer.ToString();
        }

        public static string FormatMName(string name)
        {
            return name.Replace("<", "").Replace(">", "").Replace("$", "");
        }
    }
}
