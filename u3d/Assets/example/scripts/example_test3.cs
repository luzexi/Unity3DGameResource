using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameResource;


public class example_test3 : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		AssetBundle ab = null;

		string path = ""+Application.dataPath + "/example/bundle/item_uncompress.unity3d";
		if(FileLoader.IsExist(path))
		{
			ab = AssetBundleLoader.CreateFromFile(path);
			GameObject.Instantiate(ab.mainAsset);
		}

		path = ""+Application.dataPath + "/example/bundle/Button_uncompress.unity3d";
		if(FileLoader.IsExist(path))
		{
			ab = AssetBundleLoader.CreateFromFile(path);
			GameObject.Instantiate(ab.mainAsset);
		}

		path = ""+Application.dataPath + "/example/bundle/Terrain_uncompress.unity3d";
		if(FileLoader.IsExist(path))
		{
			ab = AssetBundleLoader.CreateFromFile(path);
			GameObject.Instantiate(ab.mainAsset);
		}
	}
}
