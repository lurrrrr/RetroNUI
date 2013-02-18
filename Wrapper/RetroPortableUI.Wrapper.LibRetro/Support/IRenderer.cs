using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroPortableUI.Wrapper.LibRetro.Support
{
    public interface IRenderer
    {
        bool RenderFrame(PixelDefinition[] pixelData, uint width, uint height, uint pitch);
    }
}
