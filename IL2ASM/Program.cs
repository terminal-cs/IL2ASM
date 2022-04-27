using System.Diagnostics;

namespace IL2ASM {

    public static class Entry {
        static string Input = Tools.Format("Kernel/bin/Debug/net6.0/Kernel.dll");
        static string Output = Tools.Format("bin/Kernel.asm");
        static string OutputElf = Tools.Format("bin/kernel.elf");
        static string Nasm = Tools.IsLinux ? "nasm" : (Tools.Format("bin/nasm.exe"));

        public static void Main() {

            File.WriteAllText(Output, IL2ASM.Compiler.Compile(Input));
            Process.Start(Nasm, Output+" -o "+OutputElf);
        }
    }
}
