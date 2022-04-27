using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace IL2ASM.Instructions {
    class Ldstr : Inst {
        public Ldstr(string name) : base(name) {}

        public override void Execute(MethodDef Method, Instruction Instruction, int Index, string classname, Dictionary<string, Dictionary<string, List<string>>> ASM) {
            Code[] codes = { Code.Stloc, Code.Stloc_0, Code.Stloc_1, Code.Stloc_2, Code.Stloc_3, Code.Stloc_S };
            if (codes.Contains(Method.Body.Instructions[Index + 1].GetOpCode().Code))
            {
                //it's a var
                string vname = "\t"+Method.Name + ".str_" + Method.Body.Variables[Compiler.VIndex];
                ASM[classname].Add(vname, new());
                ASM[classname][vname].Add($"\t{vname}.size dd {Instruction.GetOperand().ToString().Length}");
                ASM[classname][vname].Add($"\t{vname}.data db \"{Instruction.GetOperand()}\", 13, 10, 0");
                ASM[classname][vname].Add($"\t{vname}.type db \"{Instruction.GetOperand().GetType()}\"");
                Compiler.VIndex++;
            }
            else
            {
                string vname = "\t"+Method.Name + ".strtemp_" + Compiler.SIndex;
                if (Method.Body.Instructions[Index + 1].OpCode.Code == Code.Call)
                {
                    ASM[classname][Method.Name].Add($"mov si, {vname}.data");
                }
                ASM[classname].Add(vname, new());
                ASM[classname][vname].Add($"\t{vname}.size dd {Instruction.Operand.ToString().Length}");
                ASM[classname][vname].Add($"\t{vname}.data db \"{Instruction.Operand}\", 13, 10, 0");
                ASM[classname][vname].Add($"\t{vname}.type db \"{Instruction.Operand.GetType()}\"");
                Compiler.SIndex++;
            }
        }
    }
}