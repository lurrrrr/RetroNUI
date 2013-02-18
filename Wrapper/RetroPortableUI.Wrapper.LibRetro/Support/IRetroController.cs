using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroPortableUI.Wrapper.LibRetro.Support
{
    public interface IRetroController
    {
        bool Update(double elapsedTime);

        bool Initialize();
    }
}
