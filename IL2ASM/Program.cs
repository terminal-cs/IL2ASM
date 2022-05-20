using System.Diagnostics;

namespace IL2ASM {

    public static class Entry
    {
        public static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
        public static string SP = IsLinux ? "/" : "\\";
        public static string Root = IsLinux ? "../../../../" : "..\\..\\..\\..\\";
        public static string Nasm = IsLinux ? "/bin/nasm" : Root + "nasm.exe";
        public static string Qemu = IsLinux ? "/bin/qemu" : $"C:{SP}Program Files{SP}qemu{SP}qemu-system-i386.exe";
        public static string Input = $"{Root}Kernel{SP}bin{SP}Debug{SP}net6.0{SP}Kernel.dll";
        public static string Output = $"{Root}Binary{SP}Kernel";

        public static void Main() {

            Directory.CreateDirectory($"{Root}Binary{SP}");
            File.WriteAllText(Output + ".asm", Compiler.Compile(Input));
            Process.Start(Nasm, Output + ".asm " + " -o " + Output + $".bin -IBinary{SP}Libraries{SP}");
            Process.Start(Qemu, $"{Root}Binary{SP}Kernel.bin");
        }
    }
}
