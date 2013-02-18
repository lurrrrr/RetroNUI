using System;
using System.Runtime.InteropServices;

namespace RetroPortableUI.Wrapper.LibRetro.RetroStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RetroSystemInfo
    {

        public char* library_name;
        public char* library_version;
        public char* valid_extensions;

        [MarshalAs(UnmanagedType.U1)]
        public bool need_fullpath;

        [MarshalAs(UnmanagedType.U1)]
        public bool block_extract;
    }
}
