using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using GameResource;


public class example_test8 : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		string path = ""+Application.dataPath + "/example/bundle/abzip.zip";
		string dest_path = ""+Application.dataPath + "/example/ab_zip/";
		ZipManager.I.uncompless(path,dest_path,finish_callback,error_callback);
	}

	private void finish_callback(object obj)
	{
		Debug.Log("Zip Finished");

		AssetBundle ab = null;

		string path = ""+Application.dataPath + "/example/ab_zip/item_uncompress.unity3d";
		if(FileLoader.IsExist(path))
		{
			ab = AssetBundleLoader.CreateFromFile(path);
			GameObject.Instantiate(ab.mainAsset);
		}

		path = ""+Application.dataPath + "/example/ab_zip/Terrain_uncompress.unity3d";
		if(FileLoader.IsExist(path))
		{
			ab = AssetBundleLoader.CreateFromFile(path);
			GameObject.Instantiate(ab.mainAsset);
		}
	}

	private void error_callback(Exception ex)
	{
		Debug.LogError("Error " + ex.StackTrace);
	}

	void OnGUI()
	{
		GUI.Label(new Rect(0,0,100,40),"Progress " + ZipManager.I.Progress);
	}


}
