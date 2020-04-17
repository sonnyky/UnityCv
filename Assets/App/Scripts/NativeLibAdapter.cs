﻿using System.Runtime.InteropServices;
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
	private static extern int _TestFunction_Internal();

	[DllImport("UnityCvPlugin")]
	private static extern void _SaveBlackAndWhite(IntPtr bytes, int rows, int cols, int type);

    [DllImport("UnityCvPlugin")]
    private static extern void _TransformImage(IntPtr bytes, IntPtr bytesOut, int rows, int cols, float angle, float scale, int transX, int transY);

    [DllImport("UnityCvPlugin")]
    private static extern void _DetectOuterHull(IntPtr bytes, int rows, int cols, int type);

    [DllImport("UnityCvPlugin")]
	private static extern float _TestGetImageShapeSimilarity();

	[DllImport("UnityCvPlugin")]
	private static extern float _CompareStructureSimilarity(IntPtr bytesRef, IntPtr toCompare, int rows, int cols, int type);

	[DllImport("UnityCvPlugin")]
	private static extern float _CompareSimilarityWithFeatures(IntPtr bytesRef, IntPtr toCompare, int rows, int cols, int type);

	#endif


	public static int Test()
	{
		#if !UNITY_EDITOR
		return _TestFunction_Internal();
		#elif UNITY_EDITOR
		return _TestFunction_Internal();
		#else
		return -1;
		#endif
	}

    public static void DetectOuterHull(string imagePath)
    {
        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
        Texture2D tmpTexture = new Texture2D(1, 1);
        tmpTexture.LoadImage(imageBytes);
        Color32[] pixelData = tmpTexture.GetPixels32();

        GCHandle pixelHandle = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
        IntPtr pixelPtr = pixelHandle.AddrOfPinnedObject();

        _DetectOuterHull(pixelPtr, 1241, 1755, 1);
    }

	public static void SaveBlackAndWhite(string imagePath)
	{

		byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
		Texture2D tmpTexture = new Texture2D(1, 1);
		tmpTexture.LoadImage(imageBytes);
		Color32[] pixelData = tmpTexture.GetPixels32();

		GCHandle pixelHandle = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
		IntPtr pixelPtr = pixelHandle.AddrOfPinnedObject();

		_SaveBlackAndWhite(pixelPtr, 1080, 1920, 1);
	}

	public static float TestFloat ()
	{
		float oneData = _TestGetImageShapeSimilarity ();
		return oneData;
	}

	public static float GetImageStructureSimilarity(string imgPath, string imgCompare, int type){
		byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
		Texture2D tmpTexture = new Texture2D(1, 1);
		tmpTexture.LoadImage(imageBytes);

		Texture2D resizedRef = Resize (tmpTexture, 3515, 2490);

		Color32[] pixelData = resizedRef.GetPixels32();

		GCHandle pixelHandle = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
		IntPtr pixelPtr = pixelHandle.AddrOfPinnedObject();

		byte[] imageCompareBytes = System.IO.File.ReadAllBytes(imgCompare);
		Texture2D cmpTexture = new Texture2D(1, 1);
		cmpTexture.LoadImage(imageCompareBytes);

		Texture2D resizedCompareRef = Resize (cmpTexture, 3515, 2490);

		Color32[] pixelCompareData = resizedCompareRef.GetPixels32();

		GCHandle pixelCompareHandle = GCHandle.Alloc(pixelCompareData, GCHandleType.Pinned);
		IntPtr pixelComparePtr = pixelCompareHandle.AddrOfPinnedObject();

		float result = _CompareStructureSimilarity (pixelPtr, pixelComparePtr, 2490, 3515, type);

		return result;
	}

	public static float GetImageSiftSimilarity(string imgPath, string imgCompare){
		byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
		Texture2D tmpTexture = new Texture2D(1, 1);
		tmpTexture.LoadImage(imageBytes);

		Texture2D resizedRef = Resize (tmpTexture, 3515, 2490);

		Color32[] pixelData = resizedRef.GetPixels32();

		GCHandle pixelHandle = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
		IntPtr pixelPtr = pixelHandle.AddrOfPinnedObject();

		byte[] imageCompareBytes = System.IO.File.ReadAllBytes(imgCompare);
		Texture2D cmpTexture = new Texture2D(1, 1);
		cmpTexture.LoadImage(imageCompareBytes);

		Texture2D resizedCompareRef = Resize (cmpTexture, 3515, 2490);

		Color32[] pixelCompareData = resizedCompareRef.GetPixels32();

		GCHandle pixelCompareHandle = GCHandle.Alloc(pixelCompareData, GCHandleType.Pinned);
		IntPtr pixelComparePtr = pixelCompareHandle.AddrOfPinnedObject();

		float result = _CompareSimilarityWithFeatures (pixelPtr, pixelComparePtr, 2490, 3515, 1);

		return result;
	}

	public static Texture2D Resize(Texture2D source, int newWidth, int newHeight)
	{
		source.filterMode = FilterMode.Point;
		RenderTexture rt = RenderTexture.GetTemporary(newWidth, newHeight);
		rt.filterMode = FilterMode.Point;
		RenderTexture.active = rt;
		Graphics.Blit(source, rt);
		Texture2D nTex = new Texture2D(newWidth, newHeight);
		nTex.ReadPixels(new Rect(0, 0, newWidth, newWidth), 0, 0);
		nTex.Apply();
		RenderTexture.active = null;
		return nTex;
	}

    public static void TransformImage(string inputImagePath)
    {
        byte[] imageBytes = System.IO.File.ReadAllBytes(inputImagePath);
        Texture2D tmpTexture = new Texture2D(1, 1);
        tmpTexture.LoadImage(imageBytes);
        Color32[] pixelData = tmpTexture.GetPixels32();

        GCHandle pixelHandle = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
        IntPtr inputPtr = pixelHandle.AddrOfPinnedObject();

        GCHandle outputHandle;
        IntPtr outputImage;
        Texture2D tex;
        Color32[] pixel32;

        tex = new Texture2D(tmpTexture.width, tmpTexture.height, TextureFormat.RGBA32, false);
        pixel32 = tex.GetPixels32();
        //Pin pixel32 array
        outputHandle = GCHandle.Alloc(pixel32, GCHandleType.Pinned);
        //Get the pinned address
        outputImage = outputHandle.AddrOfPinnedObject();

        _TransformImage(inputPtr, outputImage, tmpTexture.height, tmpTexture.width, 90f, 0.5f, 0, 0);
        tex.SetPixels32(pixel32);
        tex.Apply();

        byte[] output = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes("output_transformed.png", output);

        pixelHandle.Free();
        outputHandle.Free();

    }

}