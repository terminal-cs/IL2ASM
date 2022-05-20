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
        public static string Root = IsLinux ? "../../../../" : "..\\..\\..\\..\\";
        public static string Nasm = IsLinux ? "/bin/nasm" : Root + "nasm.exe";
        public static string Qemu = IsLinux ? "/bin/qemu" : "C:\\Program Files\\qemu\\qemu-system-i386.exe";
        public static string Input = $"{Root}Kernel\\bin\\Debug\\net6.0\\Kernel.dll";
        public static string Output = $"{Root}Binary\\Kernel";

        public static void Main() {

            Directory.CreateDirectory($"{Root}Binary\\");
            File.WriteAllText(Output + ".asm", Compiler.Compile(Input));
            Process.Start(Nasm, Output + ".asm " + " -o " + Output + ".bin -IBinary\\Libraries\\");
            Process.Start(Qemu, $"{Root}Binary\\Kernel.bin");
        }
    }
}
