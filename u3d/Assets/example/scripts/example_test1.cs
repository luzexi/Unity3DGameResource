using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameResource;


public class example_test1 : MonoBehaviour
{
	AbRequest req = null;
	List<string> paths = new List<string>();

	// Use this for initialization
	void Start ()
	{
		this.req = AbRequest.Create(finish_callback , error_callback);

		string path = "file:///"+Application.dataPath + "/example/bundle/item.unity3d";
		this.paths.Add(path);
		req.Request(path);

		path = "file:///"+Application.dataPath + "/example/bundle/Terrain.unity3d";
		this.paths.Add(path);
		req.Request(path);

		path = "file:///"+Application.dataPath + "/example/bundle/Button.unity3d";
		this.paths.Add(path);
		req.Request(path);
	}

	private void finish_callback(Dictionary<string , AssetBundle> res)
	{
		for(int i = 0 ; i<this.paths.Count ; i++)
		{
			GameObject.Instantiate(res[paths[i]].mainAsset);
		}
		this.req.Disport();
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
		if(this.req != null)
		{
			GUI.Label(new Rect(0,0,100,40),"Progress " + this.req.Progress);
		}
	}
}
