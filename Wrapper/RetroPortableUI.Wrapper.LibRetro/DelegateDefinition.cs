using System;

namespace RetroPortableUI.Wrapper.LibRetro
{
    public class DelegateDefinition
    {
        //typedef void (*retro_video_refresh_t)(const void *data, unsigned width, unsigned height, size_t pitch);
        public unsafe delegate void RetroVideoRefreshDelegate(void* data, uint width, uint height, uint pitch);

        //typedef void (*retro_audio_sample_t)(int16_t left, int16_t right);
        public unsafe delegate void RetroAudioSampleDelegate(Int16 left, Int16 right);

        //typedef size_t (*retro_audio_sample_batch_t)(const int16_t *data, size_t frames);
        public unsafe delegate void RetroAudioSampleBatchDelegate(Int16* data, uint frames);

        //typedef void (*retro_input_poll_t)(void);
        public delegate void RetroInputPollDelegate();

        //typedef int16_t (*retro_input_state_t)(unsigned port, unsigned device, unsigned index, unsigned id);
        public delegate Int16 RetroInputStateDelegate(uint port, uint device, uint index, uint id);

        //typedef bool (*retro_environment_t)(unsigned cmd, void *data);
        public unsafe delegate bool RetroEnvironmentDelegate(uint cmd, void* data);
    }
}
