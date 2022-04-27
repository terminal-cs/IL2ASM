using System.Diagnostics;

string Input = "..\\..\\..\\..\\Kernel\\bin\\Debug\\net6.0\\Kernel.dll";
string OutputASM = "..\\..\\..\\..\\Kernel.asm";
string OutputELF = "..\\..\\..\\..\\Kernel.elf";
Process Nasm = new()
{
    StartInfo = new()
    {
        FileName = "..\\..\\..\\..\\nasm.exe",
        Arguments = OutputASM + " -o " + OutputELF,
    },
};

File.WriteAllText(OutputASM, IL2ASM.IL2ASM.Compile(Input));
Nasm.Start();
