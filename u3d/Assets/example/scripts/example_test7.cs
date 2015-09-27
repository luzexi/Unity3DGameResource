using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameResource;


public class example_test7 : MonoBehaviour
{
	FileLoader floader = null;

	// Use this for initialization
	void Start ()
	{
		AssetBundle ab = null;
		string path = ""+Application.dataPath + "/item_uncompress.unity3d";

		this.floader = FileLoader.AsyncReadFile(path , finish_callback , error_callback);
	}

	//finish callback
	private void finish_callback(string name , byte[] res)
	{
		AssetBundle ab = AssetBundleLoader.CreateFromMemoryImmediate(res);
		GameObject.Instantiate(ab.mainAsset);
	}

	private void error_callback(string path , string error)
	{
		Debug.LogError("path " + path + " error " + error);
	}
}
