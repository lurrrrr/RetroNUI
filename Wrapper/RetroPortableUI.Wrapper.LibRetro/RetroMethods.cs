using System;
using System.Runtime.InteropServices;
using RetroPortableUI.Wrapper.LibRetro.RetroStructures;

namespace RetroPortableUI.Wrapper.LibRetro
{
    public class RetroMethods
    {
        //public const string corefile = "libretro-0926-mednafen-psx-x86.dll";
        //public const string corefile = "libretro-089-bsnes-compat-x86.dll";
        //public const string corefile = "libretro-test-lex-x86_64.dll";
        //public const string corefile = "retro_64.dll";
        //public const string corefile = "retro_32.dll";
        public const string corefile = "retro.dll";	

        [DllImport(corefile)]
        public static extern int retro_api_version();

        [DllImport(corefile)]
        public static extern void retro_init();

        [DllImport(corefile)]
        public static extern void retro_get_system_info(ref RetroSystemInfo info);

        [DllImport(corefile)]
        public static extern void retro_get_system_av_info(ref RetroSystemAVInfo info);

        [DllImport(corefile)]
        public static extern bool retro_load_game(ref RetroGameInfo game);

        [DllImport(corefile)]
        public static extern void retro_set_video_refresh(DelegateDefinition.RetroVideoRefreshDelegate r);

        [DllImport(corefile)]
        public static extern void retro_set_audio_sample(DelegateDefinition.RetroAudioSampleDelegate r);

        [DllImport(corefile)]
        public static extern void retro_set_audio_sample_batch(DelegateDefinition.RetroAudioSampleBatchDelegate r);

        [DllImport(corefile)]
        public static extern void retro_set_input_poll(DelegateDefinition.RetroInputPollDelegate r);

        [DllImport(corefile)]
        public static extern void retro_set_input_state(DelegateDefinition.RetroInputStateDelegate r);

        [DllImport(corefile)]
        public static extern bool retro_set_environment(DelegateDefinition.RetroEnvironmentDelegate r);

        [DllImport(corefile)]
        public static extern void retro_run();
    }
}
