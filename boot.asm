[bits 32]
MALIGN   equ 1<<0
MEMINFO  equ 1<<1
FLAGS    equ MALIGN | MEMINFO
MAGIC    equ 0x1BADB002
CHECKSUM equ -(MAGIC + FLAGS)

section .multiboot
align 4
dd MAGIC
dd FLAGS
dd CHECKSUM


section .bootstrap_stack nobits write align=16
align 16
stack_bottom:
resb 32768
stack_top:

extern Main
section .text
global _start:function (_start.end - _start)
_start:
    mov esp, stack_top
    push ebx
	call Main
	;if the kernel returns, we will just halt
	hlt
.end
section .kend
global end_of_kernel
end_of_kernel: