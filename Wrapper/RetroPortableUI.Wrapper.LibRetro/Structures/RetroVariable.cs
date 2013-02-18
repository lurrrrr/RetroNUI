using System;
using System.Runtime.InteropServices;

namespace RetroPortableUI.Wrapper.LibRetro.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RetroVariable
    {
        public char* key;
        public char* value;
    }
}
