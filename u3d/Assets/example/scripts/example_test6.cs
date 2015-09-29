using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameResource;


public class example_test6 : MonoBehaviour
{
	AsyncLoader loader = null;
	List<string> paths = new List<string>();

	// Use this for initialization
	void Start ()
	{
		AssetBundle ab = null;
		string path = ""+Application.dataPath + "/example/bundle/combine_uncompress.unity3d";

		if(FileLoader.IsExist(path))
		{
			ab = AssetBundleLoader.CreateFromFile(path);
			this.loader = AsyncLoader.StartLoad(ab,"item",finish_callback);
		}
	}

	//finish callback
	private void finish_callback(string name , UnityEngine.Object res)
	{
		Debug.Log("Finished example test6");
		GameObject.Instantiate(res);
	}

	void OnGUI()
	{
		if(this.loader != null)
		{
			GUI.Label(new Rect(0,0,100,40),"Progress " + this.loader.Progress);
		}
	}
}
