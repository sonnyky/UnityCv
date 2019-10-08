using UnityEngine;
using UnityEngine.UI;

using System.Runtime.InteropServices;
using System;

/*
        Controller provides examples for passing pointers for byte arrays back and forth
        between managed C# and native C++. When making your own classes, be sure to specify
        the Texture2D texture format before attempting to load raw bytes into it.
    */
public class Controller : MonoBehaviour
{

	public string m_ImagePath = "Image";

	void Awake()
	{
		Debug.LogWarning("Test Value in C++ is " + NativeLibAdapter.Test());

			Debug.Log ("Saving image...");
			NativeLibAdapter.SaveBlackAndWhite (m_ImagePath);
		
	}

	void Update()
	{
	}
}