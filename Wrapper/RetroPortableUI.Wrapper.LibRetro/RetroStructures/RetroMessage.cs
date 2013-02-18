using System;
using System.Runtime.InteropServices;

namespace RetroPortableUI.Wrapper.LibRetro.RetroStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RetroMessage
    {
        public char* msg;
        public uint frames;
    };
}
