using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeviceInfo : MonoBehaviour {

	public Text txtlog;


	// Use this for initialization
	void Start () {
	
		txtlog.text = "";

		#if UNITY_IOS
		txtlog.text += "Device Model = "+iPhone.generation.ToString();		
		#elif UNITY_ANDROID
			txtlog.text += "Device Model = "+SystemInfo.deviceModel;
		#else
		txtlog.text += "Device Model = "+SystemInfo.deviceModel;
		#endif


		txtlog.text += "\n";
		txtlog.text += "Device name = "+SystemInfo.deviceName;
		txtlog.text += "\n";		
		txtlog.text += "Device type = "+SystemInfo.deviceType;
		txtlog.text += "\n";
		txtlog.text += "Device UniqueId = "+SystemInfo.deviceUniqueIdentifier;
		txtlog.text += "\n";
		txtlog.text += "Graphic Device Id = "+SystemInfo.graphicsDeviceID;
		txtlog.text += "\n";
		txtlog.text += "Graphic Device Name = "+SystemInfo.graphicsDeviceName;
		txtlog.text += "\n";
		txtlog.text += "Graphic Device Vendor = "+SystemInfo.graphicsDeviceVendor;
		txtlog.text += "\n";
		txtlog.text += "Graphic Device VendorID = "+SystemInfo.graphicsDeviceVendorID;
		txtlog.text += "\n";
		txtlog.text += "Graphic Memory Size = "+SystemInfo.graphicsMemorySize;
		txtlog.text += "\n";
		txtlog.text += "Graphic Shader Level = "+SystemInfo.graphicsShaderLevel;
		txtlog.text += "\n";
		txtlog.text += "Max Texture Size = "+SystemInfo.maxTextureSize;
		txtlog.text += "\n";
		txtlog.text += "Npot Support = "+SystemInfo.npotSupport;
		txtlog.text += "\n";
		txtlog.text += "Os Version = "+SystemInfo.operatingSystem;
		txtlog.text += "\n";
		txtlog.text += "Processor Count =" + SystemInfo.processorCount;
		txtlog.text += "\n";
		txtlog.text += "Processor Type =" + SystemInfo.processorType;
		txtlog.text += "\n";	
		txtlog.text += "Support Render TargetCount = "+SystemInfo.supportedRenderTargetCount;
		txtlog.text += "\n";
		txtlog.text += "Support 3D Texture =" + SystemInfo.supports3DTextures;
		txtlog.text += "\n";
		txtlog.text += "Support Accelerometer =" + SystemInfo.supportsAccelerometer;
		txtlog.text += "\n";
		txtlog.text += "Support Compute Shader =" + SystemInfo.supportsComputeShaders;
		txtlog.text += "\n";
		txtlog.text += "Support Gyroscope =" + SystemInfo.supportsGyroscope;
		txtlog.text += "\n";

	
		txtlog.text += "Processor Type =" + SystemInfo.processorType;
		txtlog.text += "\n";
		txtlog.text += "Npot Support = "+SystemInfo.npotSupport;
		txtlog.text += "\n";

			
	
		txtlog.text += "System Memory =" + SystemInfo.systemMemorySize;
		txtlog.text += "\n";	
		txtlog.text += "Graphic Fill Rate =" + SystemInfo.graphicsPixelFillrate;
		txtlog.text += "\n";


			

		txtlog.text += "Support Shadow =" + SystemInfo.supportsShadows;
		txtlog.text += "\n";
		
		
		/*

				Type: " + SystemInfo.deviceType + "
				OS Version: " + SystemInfo.operatingSystem + "
				System Memory: " + SystemInfo.systemMemorySize + "
				Graphic Device: " + SystemInfo.graphicsDeviceName + " (" + SystemInfo.graphicsDeviceVersion + ")" + "
				Graphic Memory: " + SystemInfo.graphicsMemorySize + "
				Graphic Fill Rate: " + SystemInfo.graphicsPixelFillrate + "
				Graphic Max TexSize: " + SystemInfo.maxTextureSize + // "
				Graphic Shader Levl: " + SystemInfo.graphicsShaderLevel + "
				Support Compute Shader: " + SystemInfo.supportsComputeShaders + "
				Processor Count: " + SystemInfo.processorCount + "
				Processor Type: " + SystemInfo.processorType + // "
				Support 3D Texture: " + SystemInfo.supports3DTextures + "
				Support Shadow: " + SystemInfo.supportsShadows; 
				*/

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
