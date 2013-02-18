using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroPortableUI.Wrapper.LibRetro.RetroStructures
{
    public enum RetroPixelFormat
    {
       // 0RGB1555, native endian. 0 bit must be set to 0.
       // This pixel format is default for compatibility concerns only.
       // If a 15/16-bit pixel format is desired, consider using RGB565.
       RETRO_PIXEL_FORMAT_0RGB1555 = 0,

       // XRGB8888, native endian. X bits are ignored.
       RETRO_PIXEL_FORMAT_XRGB8888 = 1,

       // RGB565, native endian. This pixel format is the recommended format to use if a 15/16-bit format is desired
       // as it is the pixel format that is typically available on a wide range of low-power devices.
       // It is also natively supported in APIs like OpenGL ES.
       RETRO_PIXEL_FORMAT_RGB565   = 2,

       // Ensure sizeof() == sizeof(int).
       RETRO_PIXEL_FORMAT_UNKNOWN  = int.MaxValue
    }
}
