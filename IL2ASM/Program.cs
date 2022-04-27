using System.Diagnostics;

string Root = "..\\..\\..\\..\\";
string Input = Root + "Kernel\\bin\\Debug\\net6.0\\Kernel.dll";
string OutputASM = Root + "Kernel.asm";
string OutputELF = Root + "Kernel.elf";
Process Nasm = new()
{
    StartInfo = new()
    {
        FileName = Root + "nasm.exe",
        Arguments = OutputASM + " -o " + OutputELF,
    },
};

File.WriteAllText(OutputASM, IL2ASM.IL2ASM.Compile(Input));
Nasm.Start();
