using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using RetroPortableUI.Wrapper.LibRetro.RetroStructures;
using RetroPortableUI.Wrapper.LibRetro.Support;

namespace RetroPortableUI.Wrapper.LibRetro
{
	public class RetroWrapper : IRetroController
	{
		private RetroPixelFormat pixelFormat;
		private IRenderer renderDriver;

		//Prevent GC on delegates as long as the wrapper is running
		private DelegateDefinition.RetroEnvironmentDelegate _environment;
		private DelegateDefinition.RetroVideoRefreshDelegate _videoRefresh;
		private DelegateDefinition.RetroAudioSampleDelegate _audioSample;
		private DelegateDefinition.RetroAudioSampleBatchDelegate _audioSampleBatch;
		private DelegateDefinition.RetroInputPollDelegate _inputPoll;
		private DelegateDefinition.RetroInputStateDelegate _inputState;

		public RetroWrapper(IRenderer renderer)
		{
			PrintDebugInformation("Initializing libretro instance");
#if ! DEBUG
			if (renderer == null)
			{
				throw new Exception("Renderer Cannot be null")
			}
#endif
			renderDriver = renderer;
		}

		private unsafe void RetroVideoRefresh(void *data, uint width, uint  height, uint pitch)
		{
			PrintDebugInformation("Video Refresh Callback");

			// Process Pixels one by one for now...this is not the best way to do it 
			// should be using memory streams or something

			//Declare the pixel buffer to pass on to the renderer
			PixelDefinition[] pixelData = new PixelDefinition[width * height];

			//Get the array from unmanaged memory as a pointer
			IntPtr pixels = (IntPtr)data;

			//Get the size to move the pointer
			Int32 size = 0;

			uint i = 0;
			uint j = 0;

			switch (pixelFormat)
			{
				case RetroPixelFormat.RETRO_PIXEL_FORMAT_0RGB1555:
					size = Marshal.SizeOf(typeof(Int16));
					for (i = 0; i < height; i++)
					{
						for (j = 0; j < width; j++)
						{
							Int16 packed = Marshal.ReadInt16(pixels);
							pixelData[i * width + j] = new PixelDefinition()
							{
								Alpha = 1
								,
								Red = (((packed >> 10) & 0x001F) * 31) / 255.0f
								,
								Green = (((packed >> 5) & 0x001F) * 31) / 255.0f
								,
								Blue = ((packed & 0x001F) * 31) / 255.0f
							};

							pixels = (IntPtr)((int)pixels + size);
						}
					}
					break;
				case RetroPixelFormat.RETRO_PIXEL_FORMAT_XRGB8888:
					size = Marshal.SizeOf(typeof(Int32));
					for (i = 0; i < height; i++)
					{
						for (j = 0; j < width; j++)
						{
							Int32 packed = Marshal.ReadInt32(pixels);
							pixelData[i * width + j] = new PixelDefinition()
							{
								Alpha = 1
								,
								Red = (packed >> 16) & 0x00FF
								,
								Green = (packed >> 8) & 0x00FF
								,
								Blue = (packed & 0x00FF)
							};

							pixels = (IntPtr)((int)pixels + size);
						}
					}
					break;
				case RetroPixelFormat.RETRO_PIXEL_FORMAT_RGB565:
					size = Marshal.SizeOf(typeof(Int16));
					for (i = 0; i < height; i++)
					{
						for (j = 0; j < width; j++)
						{
							Int16 packed = Marshal.ReadInt16(pixels);
							pixelData[i * width + j] = new PixelDefinition()
							{
								Alpha = 1
								,
								Red = (((packed >> 11) & 0x001F) * 31) / 255.0f
								,
								Green = (((packed >> 5) & 0x003F) * 63) / 255.0f
								,
								Blue = ((packed & 0x001F) * 31) / 255.0f
							};

							pixels = (IntPtr)((int)pixels + size);
						}
					}
					break;
				case RetroPixelFormat.RETRO_PIXEL_FORMAT_UNKNOWN:
					pixelData = null;
					break;
			}

			//Call renderer implementation
#if DEBUG
			if (renderDriver != null)
			{
#endif
				renderDriver.RenderFrame(pixelData, width, height, pitch);
#if DEBUG
			}
#endif
		}

		private unsafe void RetroAudioSample(Int16 left, Int16 right)
		{
			PrintDebugInformation("Audio Sample Callback");
			return;
		}

		public unsafe void RetroAudioSampleBatch(Int16 *data, uint frames)
		{
			PrintDebugInformation("Audio Sample Batch Callback");
			return;
		}

		public unsafe void RetroInputPoll()
		{
			PrintDebugInformation("Input Poll Callback");
			return;
		}

		public unsafe Int16 RetroInputState(uint port, uint device, uint index, uint id)
		{
			PrintDebugInformation("Input State Callback");
			return 0;
		}

		public unsafe bool RetroEnvironment(uint cmd, void *data)
		{
			PrintDebugInformation("Environment Callback");
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
						PrintDebugInformation("Environ SET_PIXEL_FORMAT: 0RGB1555.\n");
						break;
					case RetroPixelFormat.RETRO_PIXEL_FORMAT_RGB565:
						PrintDebugInformation("Environ SET_PIXEL_FORMAT: RGB565.\n");
						break;
					case RetroPixelFormat.RETRO_PIXEL_FORMAT_XRGB8888:
						PrintDebugInformation("Environ SET_PIXEL_FORMAT: XRGB8888.\n");
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

		private unsafe void Init ()
		{
			int _apiVersion = RetroMethods.retro_api_version();
			RetroSystemInfo info = new RetroSystemInfo();
			RetroMethods.retro_get_system_info(ref info);
	
			string _coreName = Marshal.PtrToStringAnsi((IntPtr)info.library_name);
			string _coreVersion = Marshal.PtrToStringAnsi((IntPtr)info.library_version);
			string _validExtensions = Marshal.PtrToStringAnsi((IntPtr)info.valid_extensions);
			bool _requiresFullPath = info.need_fullpath;
			bool _blockExtract = info.block_extract;

			PrintDebugInformation("Core information:");
			PrintDebugInformation("API Version: " + _apiVersion);
			PrintDebugInformation("Core Name: " + _coreName);
			PrintDebugInformation("Core Version: " + _coreVersion);
			PrintDebugInformation("Valid Extensions: " + _validExtensions);
			PrintDebugInformation("Block Extraction: " + _blockExtract);
			PrintDebugInformation("Requires Full Path: " + _requiresFullPath);

			_environment = new DelegateDefinition.RetroEnvironmentDelegate(RetroEnvironment);
			_videoRefresh = new DelegateDefinition.RetroVideoRefreshDelegate(RetroVideoRefresh);
			_audioSample = new DelegateDefinition.RetroAudioSampleDelegate(RetroAudioSample);
			_audioSampleBatch = new DelegateDefinition.RetroAudioSampleBatchDelegate(RetroAudioSampleBatch);
			_inputPoll = new DelegateDefinition.RetroInputPollDelegate(RetroInputPoll);
			_inputState = new DelegateDefinition.RetroInputStateDelegate(RetroInputState);

			PrintDebugInformation("\nSetting up environment:");
			RetroMethods.retro_set_environment(_environment);
			RetroMethods.retro_set_video_refresh(_videoRefresh);
			RetroMethods.retro_set_audio_sample(_audioSample);
			RetroMethods.retro_set_audio_sample_batch(_audioSampleBatch);
			RetroMethods.retro_set_input_poll(_inputPoll);
			RetroMethods.retro_set_input_state(_inputState);

			PrintDebugInformation("\nInitializing:");
			RetroMethods.retro_init();

			PrintDebugInformation("\nLoading rom:");
			RetroGameInfo gameInfo = new RetroGameInfo();
			RetroMethods.retro_load_game(ref gameInfo);

			PrintDebugInformation("\nSystem information:");

			RetroSystemAVInfo av = new RetroSystemAVInfo();
			RetroMethods.retro_get_system_av_info(ref av);
			
			PrintDebugInformation("Geometry:");
			PrintDebugInformation("Base width: " + av.geometry.base_width);
			PrintDebugInformation("Base height: " + av.geometry.base_height);
			PrintDebugInformation("Max width: " + av.geometry.max_width);
			PrintDebugInformation("Max height: " + av.geometry.max_height);
			PrintDebugInformation("Aspect ratio: " + av.geometry.aspect_ratio);
			PrintDebugInformation("Geometry:");
			PrintDebugInformation("Target fps: " + av.timing.fps);
			PrintDebugInformation("Sample rate " + av.timing.sample_rate);               
		}

		bool IRetroController.Initialize()
		{
			Init();
			return true;
		}

		bool IRetroController.Update(double elapsedTime)
		{
			PrintDebugInformation("Processing Frame:");
			RetroMethods.retro_run();
			return true;
		}

		private void PrintDebugInformation(string message)
		{
			//Debug.WriteLine(message);
		}
	}
}

