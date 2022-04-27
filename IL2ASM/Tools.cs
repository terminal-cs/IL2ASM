namespace IL2ASM
{
    public static class Tools
    {
        public static bool IsLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
        public static string Separator => (IsLinux ? "/" : "\\");
    }
}