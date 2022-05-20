%include "..\..\..\..\Binary\Libraries\System.Runtime.asm"
%include "..\..\..\..\Binary\Libraries\System.Console.asm"

[org 0x7c00]
mov ah, 0x0e
jmp Main

Main:
  jmp .cctor
  call System.Console.WriteLine
  ret
.cctor:
  S0 db "Hello, World!", 0xa
  push byte S0
  push 13
  ret

times 510-($-$$) db 0
dw 0xAA55