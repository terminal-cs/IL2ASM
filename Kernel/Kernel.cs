namespace Kernel
{
    public unsafe class Kernel
    {
        public static void Main()
        {
            for (int I = 0; I < 80 * 25; I++)
            {
                *(byte*)(0x8b00 + I) = 0;
            }
            Console.WriteLine("Hello, World!");
        }
    }
}