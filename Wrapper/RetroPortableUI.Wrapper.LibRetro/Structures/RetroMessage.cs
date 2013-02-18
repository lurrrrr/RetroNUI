using System;
using System.Runtime.InteropServices;

namespace RetroPortableUI.Wrapper.LibRetro.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RetroMessage
    {
        public char* msg;
        public uint frames;
    };
}
