using System;
using System.Runtime.InteropServices;
using RetroPortableUI.Wrapper.LibRetro.RetroStructures;

namespace RetroPortableUI.Wrapper.LibRetro
{
    public class RetroMethods
    {
        //public const string corefile = "retro.dll";
        public const string corefile = "libretro-089-bsnes-accuracy-x86_64.dll";

        [DllImport(corefile, EntryPoint = "retro_api_version")]
        public static extern int RetroApiVersion();

        [DllImport(corefile, EntryPoint = "retro_init")]
        public static extern void RetroInit();

        [DllImport(corefile, EntryPoint = "retro_get_system_info")]
        public static extern void RetroGetSystemInfo(ref RetroSystemInfo info);

        [DllImport(corefile, EntryPoint = "retro_get_system_av_info")]
        public static extern void RetroGetSystemAVInfo(ref RetroSystemAVInfo info);

        [DllImport(corefile, EntryPoint = "retro_load_game")]
        public static extern bool RetroLoadGame(ref RetroGameInfo game);

        [DllImport(corefile, EntryPoint = "retro_set_video_refresh")]
        public static extern void RetroSetVideoRefresh(DelegateDefinition.RetroVideoRefreshDelegate r);

        [DllImport(corefile, EntryPoint = "retro_set_audio_sample")]
        public static extern void RetroSetAudioSample(DelegateDefinition.RetroAudioSampleDelegate r);

        [DllImport(corefile, EntryPoint = "retro_set_audio_sample_batch")]
        public static extern void RetroSetAudioSampleBatch(DelegateDefinition.RetroAudioSampleBatchDelegate r);

        [DllImport(corefile, EntryPoint = "retro_set_input_poll")]
        public static extern void RetroSetInputPoll(DelegateDefinition.RetroInputPollDelegate r);

        [DllImport(corefile, EntryPoint = "retro_set_input_state")]
        public static extern void RetroSetInputState(DelegateDefinition.RetroInputStateDelegate r);

        [DllImport(corefile, EntryPoint = "retro_set_environment")]
        public static extern bool RetroSetEnvironment(DelegateDefinition.RetroEnvironmentDelegate r);

        [DllImport(corefile, EntryPoint = "retro_run")]
        public static extern void RetroRun();

        [DllImport(corefile, EntryPoint = "retro_deinit")]
        public static extern void RetroDeInit();
    }
}
