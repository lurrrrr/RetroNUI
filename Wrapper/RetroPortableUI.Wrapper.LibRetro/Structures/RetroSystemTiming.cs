using System;
using System.Runtime.InteropServices;

namespace RetroPortableUI.Wrapper.LibRetro.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RetroSystemTiming
    {
        public double fps;
        public double sample_rate;
    }
}
