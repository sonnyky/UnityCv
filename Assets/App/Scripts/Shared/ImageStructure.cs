using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;
using System;

public class ImageStructure : MonoBehaviour {

	[DllImport("UnityCvPlugin")]
	private static extern float _CompareStructureSimilarityByPath(IntPtr bytesRef, IntPtr toCompare, int rows, int cols, int type);

	public static float GetImageStructureSimilarityByTexture(Texture2D texA, Texture2D texB, int type){
		

		Color32[] pixelData = texA.GetPixels32();

		GCHandle pixelHandle = GCHandle.Alloc(pixelData, GCHandleType.Pinned);
		IntPtr pixelPtr = pixelHandle.AddrOfPinnedObject();



		Color32[] pixelCompareData = texB.GetPixels32();

		GCHandle pixelCompareHandle = GCHandle.Alloc(pixelCompareData, GCHandleType.Pinned);
		IntPtr pixelComparePtr = pixelCompareHandle.AddrOfPinnedObject();

		float result = _CompareStructureSimilarity (pixelPtr, pixelComparePtr, texA.height, texA.width, type);

		return result;
	}


	public static float GetImageStructureSimilarityByPath(string imgPath, string imgCompare, int type){
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

}
