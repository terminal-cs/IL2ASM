#!/bin/bash
if [ -x "$(command -v figlet)" ]; then
    figlet -f slant "IL2ASM Linux"
else
    echo "IL2ASM Linux"
fi
NASM=`which nasm`
if [ -x "$NASM" ]; then
    echo "NASM found at $NASM"
else
    echo "NASM not found. Please install NASM."
fi
LD=`which ld`
if [ -x "$LD" ]; then
    echo "LD found at $LD"
else
    echo "LD not found. Please install LD."
fi
DOTNET=`which dotnet`
if [ -x "$DOTNET" ]; then
    echo "DOTNET found at $DOTNET"
else
    echo "DOTNET not found. Please install DOTNET CORE 6.0"
fi