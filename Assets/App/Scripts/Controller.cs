using UnityEngine;
using UnityEngine.UI;
using System.IO;

using System.Runtime.InteropServices;
using System;

/*
        Controller provides examples for passing pointers for byte arrays back and forth
        between managed C# and native C++. When making your own classes, be sure to specify
        the Texture2D texture format before attempting to load raw bytes into it.
    */
public class Controller : MonoBehaviour
{

	public string m_FolderPath = "";
	public string m_ImagePath = "Image";
	public string m_ImageToCompare = "ToCompare";

    public string m_TestSiftMarker = "SiftTest";
    public string m_TestSiftCompare = "SiftCompare";

	void Start(){
        
        // Save a black and white image
        NativeLibAdapter.SaveBlackAndWhite(m_ImagePath);

        // Detect edges and outer hull
        NativeLibAdapter.DetectOuterHull(m_ImagePath);

        // Compare image similarity by comparing shapes
        float similarity = NativeLibAdapter.GetImageStructureSimilarity(m_ImagePath, m_ImageToCompare);
        Debug.Log("Similarity from structure : " + similarity);

        // Compare image similarity by comparing feature points
        NativeLibAdapter.GetImageSiftSimilarity(m_TestSiftMarker, m_TestSiftCompare);

        // Crop and rotate image
        NativeLibAdapter.TransformImage(m_ImagePath, 90f, 0.5f, 100, 100);

    }

}