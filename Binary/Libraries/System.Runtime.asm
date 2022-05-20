System.Console.WriteLine:
  pop cx
  .Loop1:
    pop si
    int 0x10
    loop .Loop1
    push eax
    ret