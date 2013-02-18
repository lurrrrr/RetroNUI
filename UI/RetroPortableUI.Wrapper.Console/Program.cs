using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RetroPortableUI.Wrapper.LibRetro;

namespace RetroPortableUI.Wrapper.Console
{
    public class Program
    {
        public static void AddEnvironmentPaths(string paths)
        {
            string path = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
            path += ";" + string.Join(";", paths);

            Environment.SetEnvironmentVariable("PATH", path);
        }

        public static void Main(string[] args)
        {
#if X86_64 
            AddEnvironmentPaths("..\\..\\..\\..\\Cores\\x86_64");
#endif
#if X86
            AddEnvironmentPaths("..\\..\\..\\..\\Cores\\x86");
#endif

            RetroWrapper wrapper = new RetroWrapper();

            wrapper.Init();

            System.Console.WriteLine("Library Initialized...");
            System.Console.ReadKey();

            wrapper.Run();

            System.Console.WriteLine("Press any key...");
        }
    }
}
