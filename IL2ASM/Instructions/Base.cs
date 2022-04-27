using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace IL2ASM.Instructions {
    class Inst {
        public string IName;
        public Inst(string IName) { this.IName=IName; }
        public virtual void Execute(MethodDef Method, Instruction Instruction, int Index, string classname, Dictionary<string, Dictionary<string, List<string>>> ASM) {}
    }
}