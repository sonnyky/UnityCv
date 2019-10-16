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

	void Awake()
	{
		Debug.LogWarning("Test Value in C++ is " + NativeLibAdapter.Test());

		Debug.Log ("Test float : " + NativeLibAdapter.TestFloat());
			//NativeLibAdapter.SaveBlackAndWhite (m_ImagePath);

	}

	void Start(){

		float timeNow = Time.realtimeSinceStartup;
		/*
		string[] filePaths= Directory.GetFiles(m_FolderPath,"*.jpg");

		for (int i = 0; i < filePaths.Length; i++) {
			float similarity = NativeLibAdapter.GetImageStructureSimilarity (m_ImagePath, filePaths[i]);
			string result = " is different";
			if(similarity < 0.0001f) result = " is same";
			Debug.Log ("The image : " + filePaths[i] + " and similarity to ref : " + similarity + result);
		}
*/

		float similarity = NativeLibAdapter.GetImageStructureSimilarity (m_ImagePath, m_ImageToCompare, 2);
		string result = " is different";
		if(similarity < 0.0001f) result = " is same";
		Debug.Log ("similarity : " + similarity + result);

		/*
		float similaritySift = NativeLibAdapter.GetImageSiftSimilarity (m_ImagePath, m_ImageToCompare);
		string resultSift = " is different";
		if(similaritySift < 0.0001f) resultSift = " is same";
		Debug.Log ("similarity with SIFT : " + similaritySift + resultSift);
*/
		float timeAfterMethod = Time.realtimeSinceStartup;

		Debug.Log ("Elapsed time : " + (timeAfterMethod - timeNow));
	}

	void Update()
	{
	}
}