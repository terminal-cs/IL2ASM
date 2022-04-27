string Input = "..\\..\\..\\..\\Kernel\\bin\\Debug\\net6.0\\Kernel.dll";
string Output = "..\\..\\..\\..\\Kernel.asm";

File.WriteAllText(Output, IL2ASM.IL2ASM.Compile(Input));