﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RetroPortableUI.Wrapper.LibRetro;
using RetroPortableUI.Wrapper.LibRetro.Support;
using RetroPortableUI.Utils;

namespace RetroPortableUI.Wrapper.Console
{
    public class Program : IRenderer
    {
        public static void Main(string[] args)
        {
            Program prog = new Program();
            prog.Run();
        }

        public void Run()
        {
#if X86_64 
            EnvironmentPath.AddEnvironmentPaths("..\\..\\..\\..\\Cores\\x86_64");
#endif
#if X86
            EnvironmentPath.AddEnvironmentPaths("..\\..\\..\\..\\Cores\\x86");
#endif

            IRetroController wrapper = new RetroWrapper(this);

            wrapper.Initialize();

            System.Console.WriteLine("Library Initialized...");
            System.Console.ReadKey();

            wrapper.Update(0);

            System.Console.WriteLine("Press any key...");

            wrapper.Shutdown();
        }

        bool IRenderer.RenderFrame(PixelDefinition[] pixelData, uint width, uint height, uint pitch)
        {
            throw new NotImplementedException();
        }
    }
}
