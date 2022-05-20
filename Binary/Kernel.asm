%include "..\..\..\..\Binary\Libraries\System.Runtime.asm"
%include "..\..\..\..\Binary\Libraries\System.Console.asm"

[bits 32]
[global Main]

Main:
  jmp .cctor
  call System.Console.WriteLine
  ret
.cctor:
  db "Hello, World!", 0xa
  push byte 0xa
  push 13
  ret

times 510-($-$$) db 0
dw 0xAA55