using System.Diagnostics;

namespace IL2ASM {

    public static class Entry
    {
        public static string Input = "..\\..\\..\\..\\Kernel\\bin\\Debug\\net6.0\\Kernel.dll";
        public static string Output = "..\\..\\..\\..\\Binary\\Kernel";

        public static void Main() {

            Directory.CreateDirectory("..\\..\\..\\..\\Binary\\");
            File.WriteAllText(Output + ".asm", Compiler.Compile(Input));
            Process.Start("..\\..\\..\\..\\nasm.exe", Output + ".asm " + " -o " + Output + ".bin -IBinary\\Libraries\\");
            Process.Start("C:\\qemu\\qemu-system-i386.exe", "..\\..\\..\\..\\Binary\\Kernel.bin");
        }
    }
}
