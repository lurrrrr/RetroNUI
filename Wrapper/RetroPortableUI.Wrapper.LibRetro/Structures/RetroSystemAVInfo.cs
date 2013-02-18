﻿using System;
using System.Runtime.InteropServices;

namespace RetroPortableUI.Wrapper.LibRetro.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RetroSystemAVInfo
    {
        public RetroGameGeometry geometry;
        public RetroSystemTiming timing;
    }
}
