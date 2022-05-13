namespace Kernel
{
    public unsafe class Kernel
    {
        public static void Main()
        {
            *(char*)0x00000000 = 'H';
            Console.WriteLine("Hello, World!");
        }
    }
}