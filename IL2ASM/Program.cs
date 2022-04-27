string Input = "..\\..\\..\\..\\Kernel\\bin\\Debug\\net6.0\\Kernel.dll";
string Output = "..\\..\\..\\..\\Kernel.asm";
string Output = "..\\..\\..\\..\\Kernel.elf";
string Nasm = "..\\..\\..\\..\\nasm.exe";

File.WriteAllText(Output, IL2ASM.IL2ASM.Compile(Input));
System.Diagnostics.Process.Start(Nasm, Output + " -o " + OutputElf);
