using System;
using System.Runtime.InteropServices;

namespace RetroPortableUI.Wrapper.LibRetro.RetroStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RetroSystemTiming
    {
        public double fps;
        public double sample_rate;
    }
}
