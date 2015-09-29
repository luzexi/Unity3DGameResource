using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameResource;


public class example_test2 : MonoBehaviour
{
	List<string> paths = new List<string>();

	// Use this for initialization
	void Start ()
	{
		AbMgr.I.Clear();
		string path = "file:///"+Application.dataPath + "/example/bundle/item.unity3d";
		this.paths.Add(path);

		path = "file:///"+Application.dataPath + "/example/bundle/Terrain.unity3d";
		this.paths.Add(path);

		path = "file:///"+Application.dataPath + "/example/bundle/Button.unity3d";
		this.paths.Add(path);

		AbMgr.I.Request(this.paths , finish_callback , error_callback);
	}

	private void finish_callback()
	{
		Debug.Log("Finished");
		for(int i = 0 ; i<this.paths.Count ; i++)
		{
			GameObject.Instantiate(AbMgr.I.GetAssetBundle(paths[i]).mainAsset);
		}
	}

	private void error_callback(string path , string error)
	{
		Debug.LogError(" path :" + path + "\nerror :" + error);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//
	}

	void OnGUI()
	{
		{
			GUI.Label(new Rect(0,0,100,40),"Progress " + AbMgr.I.Progress);
		}
	}
}
