using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RetroPortableUI.Wrapper.LibRetro
{
    public class ConfigurationConstants
    {
        public const uint RETRO_ENVIRONMENT_SET_ROTATION = 1;
        public const uint RETRO_ENVIRONMENT_GET_OVERSCAN = 2;
        public const uint RETRO_ENVIRONMENT_GET_CAN_DUPE = 3;
        public const uint RETRO_ENVIRONMENT_GET_VARIABLE = 4;
        public const uint RETRO_ENVIRONMENT_SET_VARIABLES = 5;
        public const uint RETRO_ENVIRONMENT_SET_MESSAGE = 6;
        public const uint RETRO_ENVIRONMENT_SHUTDOWN = 7;
        public const uint RETRO_ENVIRONMENT_SET_PERFORMANCE_LEVEL = 8;
        public const uint RETRO_ENVIRONMENT_GET_SYSTEM_DIRECTORY = 9;
        public const uint RETRO_ENVIRONMENT_SET_PIXEL_FORMAT = 10;
        public const uint RETRO_ENVIRONMENT_SET_INPUT_DESCRIPTORS = 11;
        public const uint RETRO_ENVIRONMENT_SET_KEYBOARD_CALLBACK = 12;
    }
}
