using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GameResource;


public class example_test1 : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		AbRequest req = AbRequest.Create(finish_callback , error_callback);
		string path = "file:///"+Application.dataPath + "/item.unity3d";
		req.Request(path);
	}

	private void finish_callback(Dictionary<string , AssetBundle> res)
	{
		string path = "file:///"+Application.dataPath + "/item.unity3d";
		GameObject.Instantiate(res[path].mainAsset);
	}

	private void error_callback(string path , string error)
	{
		Debug.LogError(" path :" + path + "\nerror :" + error);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
