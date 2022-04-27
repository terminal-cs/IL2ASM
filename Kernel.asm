%include "Libraries\System\Runtime.asm"
%include "Libraries\System\Console.asm"
jmp Kernel.Main

Kernel:
	.Main:
    push "Hello, World!"
    call System.Void System.Console::WriteLine(System.String)
    ret 

