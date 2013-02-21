using System;
using System.Runtime.InteropServices;

namespace RetroPortableUI.Wrapper.LibRetro.RetroStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RetroGameInfo
    {
        public char* path;
        public void* data;
        public uint size;
        public char* meta;
    }
}
