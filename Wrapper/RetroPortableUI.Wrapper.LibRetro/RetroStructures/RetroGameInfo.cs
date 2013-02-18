using System;
using System.Runtime.InteropServices;

namespace RetroPortableUI.Wrapper.LibRetro.RetroStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RetroGameInfo
    {
        public char* path;
        public void* data;
        public byte size;
        public char* meta;
    }
}
