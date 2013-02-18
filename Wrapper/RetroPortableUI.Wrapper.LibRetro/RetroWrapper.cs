using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RetroPortableUI.Wrapper.LibRetro.Structures;

namespace RetroPortableUI.Wrapper.LibRetro
{
	public class RetroWrapper
	{
        struct Color
        {
            public int R;
            public int G;
            public int B;
        };

        private RetroPixelFormat pixelFormat;

        public RetroWrapper()
        {
            Debug.WriteLine("Initializing libretro instance");
            return;
        }

		private unsafe void RetroVideoRefresh(void *data, uint width, uint  height, uint pitch)
		{
            Debug.WriteLine("Video Refresh Callback");
            //Process Pixels one by one for now...
            if (pixelFormat == RetroPixelFormat.RETRO_PIXEL_FORMAT_RGB565)
            {
                List<Color> pixelColors = new List<Color>();
                IntPtr pixels = (IntPtr)data;
                var size = Marshal.SizeOf(typeof(Int16));

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Int16 packed = Marshal.ReadInt16(pixels);
                        pixelColors.Add(new Color() 
                        { 
                            R = (0xF800 & packed) >> 11
                            , G = (0x07E0 & packed) >> 5
                            , B = (0x001F & packed)
                        });

                        pixels = (IntPtr)((int)pixels + size);
                    }
                }

                for (int i = 0; i < height; i++)
                {
                    Debug.Write("[ ");
                    for (int j = 0; j < width; j++)
                    {
                        int index = i * (int)width + j;
                        Debug.Write(string.Format("({0},{1},{2})", pixelColors[index].R.ToString("00"), pixelColors[index].G.ToString("00"), pixelColors[index].B.ToString("00")));
                    }
                    Debug.WriteLine(" ]");
                }
            }

			return;
		}

		private unsafe void RetroAudioSample(Int16 left, Int16 right)
		{
            Debug.WriteLine("Audio Sample Callback");
			return;
		}

		public unsafe void RetroAudioSampleBatch(Int16 *data, uint frames)
		{
            Debug.WriteLine("Audio Sample Batch Callback");
			return;
		}

		public unsafe void RetroInputPoll()
		{
            Debug.WriteLine("Input Poll Callback");
			return;
		}

		public unsafe Int16 RetroInputState(uint port, uint device, uint index, uint id)
		{
            Debug.WriteLine("Input State Callback");
			return 0;
		}

		public unsafe bool RetroEnvironment(uint cmd, void *data)
		{
            Debug.WriteLine("Environment Callback");
			switch (cmd)
			{
			case ConfigurationConstants.RETRO_ENVIRONMENT_GET_OVERSCAN:
				break;
            case ConfigurationConstants.RETRO_ENVIRONMENT_GET_VARIABLE:
				break;
            case ConfigurationConstants.RETRO_ENVIRONMENT_SET_VARIABLES:
				break;
            case ConfigurationConstants.RETRO_ENVIRONMENT_SET_MESSAGE:
				break;
            case ConfigurationConstants.RETRO_ENVIRONMENT_SET_ROTATION:
				break;
            case ConfigurationConstants.RETRO_ENVIRONMENT_SHUTDOWN:
				break;
            case ConfigurationConstants.RETRO_ENVIRONMENT_SET_PERFORMANCE_LEVEL:
				break;
            case ConfigurationConstants.RETRO_ENVIRONMENT_GET_SYSTEM_DIRECTORY:
				break;
            case ConfigurationConstants.RETRO_ENVIRONMENT_SET_PIXEL_FORMAT:
                pixelFormat = *(RetroPixelFormat*)data;
                switch (pixelFormat)
                {
                    case RetroPixelFormat.RETRO_PIXEL_FORMAT_0RGB1555:
                        Debug.WriteLine("Environ SET_PIXEL_FORMAT: 0RGB1555.\n");
                        break;
                    case RetroPixelFormat.RETRO_PIXEL_FORMAT_RGB565:
                        Debug.WriteLine("Environ SET_PIXEL_FORMAT: RGB565.\n");
                        break;
                    case RetroPixelFormat.RETRO_PIXEL_FORMAT_XRGB8888:
                        Debug.WriteLine("Environ SET_PIXEL_FORMAT: XRGB8888.\n");
                        break;
                    default:
                        return false;
                }
				break;
            case ConfigurationConstants.RETRO_ENVIRONMENT_SET_INPUT_DESCRIPTORS:
				break;
            case ConfigurationConstants.RETRO_ENVIRONMENT_SET_KEYBOARD_CALLBACK:
				break;
			default:
				return false;
			}
			return true;
		}

		public unsafe void Init ()
		{
			int _apiVersion = RetroMethods.retro_api_version();
            RetroSystemInfo info = new RetroSystemInfo();
            RetroMethods.retro_get_system_info(ref info);
	
			string _coreName = Marshal.PtrToStringAnsi((IntPtr)info.library_name);
			string _coreVersion = Marshal.PtrToStringAnsi((IntPtr)info.library_version);
			string _validExtensions = Marshal.PtrToStringAnsi((IntPtr)info.valid_extensions);
			bool _requiresFullPath = info.need_fullpath;
			bool _blockExtract = info.block_extract;

            Debug.WriteLine("Core information:");
            Debug.WriteLine("API Version: " + _apiVersion);
            Debug.WriteLine("Core Name: " + _coreName);
            Debug.WriteLine("Core Version: " + _coreVersion);
            Debug.WriteLine("Valid Extensions: " + _validExtensions);
            Debug.WriteLine("Block Extraction: " + _blockExtract);
            Debug.WriteLine("Requires Full Path: " + _requiresFullPath);

            DelegateDefinition.RetroEnvironmentDelegate _environment = new DelegateDefinition.RetroEnvironmentDelegate(RetroEnvironment);
            DelegateDefinition.RetroVideoRefreshDelegate _videoRefresh = new DelegateDefinition.RetroVideoRefreshDelegate(RetroVideoRefresh);
            DelegateDefinition.RetroAudioSampleDelegate _audioSample = new DelegateDefinition.RetroAudioSampleDelegate(RetroAudioSample);
            DelegateDefinition.RetroAudioSampleBatchDelegate _audioSampleBatch = new DelegateDefinition.RetroAudioSampleBatchDelegate(RetroAudioSampleBatch);
            DelegateDefinition.RetroInputPollDelegate _inputPoll = new DelegateDefinition.RetroInputPollDelegate(RetroInputPoll);
            DelegateDefinition.RetroInputStateDelegate _inputState = new DelegateDefinition.RetroInputStateDelegate(RetroInputState);

            Debug.WriteLine("\nSetting up environment:");
            RetroMethods.retro_set_environment(_environment);
            RetroMethods.retro_set_video_refresh(_videoRefresh);
            RetroMethods.retro_set_audio_sample(_audioSample);
            RetroMethods.retro_set_audio_sample_batch(_audioSampleBatch);
            RetroMethods.retro_set_input_poll(_inputPoll);
            RetroMethods.retro_set_input_state(_inputState);

            Debug.WriteLine("\nInitializing:");
            RetroMethods.retro_init();

            Debug.WriteLine("\nLoading rom:");
            RetroGameInfo gameInfo = new RetroGameInfo();
            RetroMethods.retro_load_game(ref gameInfo);

            Debug.WriteLine("\nSystem information:");

            RetroSystemAVInfo av = new RetroSystemAVInfo();
            RetroMethods.retro_get_system_av_info(ref av);
            
            Debug.WriteLine("Geometry:");
            Debug.WriteLine("Base width: " + av.geometry.base_width);
            Debug.WriteLine("Base height: " + av.geometry.base_height);
            Debug.WriteLine("Max width: " + av.geometry.max_width);
            Debug.WriteLine("Max height: " + av.geometry.max_height);
            Debug.WriteLine("Aspect ratio: " + av.geometry.aspect_ratio);
            Debug.WriteLine("Geometry:");
            Debug.WriteLine("Target fps: " + av.timing.fps);
            Debug.WriteLine("Sample rate " + av.timing.sample_rate);               
		}

        public void Run()
        {
            Debug.WriteLine("Processing Frame:");
            RetroMethods.retro_run();
        }
	}
}

