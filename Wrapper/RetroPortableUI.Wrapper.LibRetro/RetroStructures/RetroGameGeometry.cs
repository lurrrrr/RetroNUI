using System;
using System.Runtime.InteropServices;

namespace RetroPortableUI.Wrapper.LibRetro.RetroStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RetroGameGeometry
    {
        public uint base_width;
        public uint base_height;
        public uint max_width;
        public uint max_height;
        public float aspect_ratio;
    }
}
