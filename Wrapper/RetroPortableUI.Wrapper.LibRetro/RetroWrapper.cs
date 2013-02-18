using System;
using System.Runtime.InteropServices;
using RetroPortableUI.Wrapper.LibRetro.Structures;
using System.Diagnostics;

namespace RetroPortableUI.Wrapper.LibRetro
{
	public class RetroWrapper
	{
        public RetroWrapper()
        {
            Debug.WriteLine("Initializing libretro instance");
            return;
        }

		private unsafe void RetroVideoRefresh(void *data, uint width, uint  height, uint pitch)
		{
            Debug.WriteLine("Video Refresh Callback");
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

