using System.Runtime.InteropServices;
using System;

using UnityEngine;

/*
        NativeLibAdapter is an example communication layer between managed C# and native C++
    */
public class NativeLibAdapter
{
	#if !UNITY_EDITOR
	/*
	[DllImport("native-lib")]
	private static extern int TestFunction_Internal();
	*/
	#elif UNITY_EDITOR
	[DllImport("UnityCvPlugin")]
	private static extern int TestFunction_Internal();

	[DllImport("UnityCvPlugin")]
	private static extern void _SaveBlackAndWhite(IntPtr bytes, int rows, int cols, int type);


	#endif


	public static int Test()
	{
		#if !UNITY_EDITOR
		return TestFunction_Internal();
		#elif UNITY_EDITOR
		return TestFunction_Internal();
		#else
		return -1;
		#endif
	}

	public static void SaveBlackAndWhite(string imagePath)
	{

		byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
		Texture2D tmpTexture = new Texture2D(1, 1);
		tmpTexture.LoadImage(imageBytes);
		Color32[] pixelData = tmpTexture.GetPixels32();

		GCHandle pixelHandle = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
		IntPtr pixelPtr = pixelHandle.AddrOfPinnedObject();

		_SaveBlackAndWhite(pixelPtr, 334, 500, 1);
	}

}