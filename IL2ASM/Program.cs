Console.WriteLine("Ready!");
string Input = Console.ReadLine();
string Output = IL2ASM.IL2ASM.Compile(Input);
File.WriteAllText("Out.asm", Output);