using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameResource;


public class example_test5 : MonoBehaviour
{
	AssetBundleLoader loader = null;
	List<string> paths = new List<string>();

	// Use this for initialization
	void Start ()
	{
		AssetBundle ab = null;

		string path = ""+Application.dataPath + "/example/bundle/item.unity3d";
		if(FileLoader.IsExist(path))
		{
			byte[] data = FileLoader.ReadAllBytes(path);
			this.loader = AssetBundleLoader.LoadBinary(data,finish_callback,error_callback);
		}
	}

	//finish callback
	private void finish_callback(string path , AssetBundle res)
	{
		GameObject.Instantiate(res.mainAsset);
		res.Unload(false);
	}

	//error callback
	private void error_callback(string path , string error)
	{
		Debug.LogError(" path :" + path + "\nerror :" + error);
	}
}
