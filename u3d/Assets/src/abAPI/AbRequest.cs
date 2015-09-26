using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//	AbRequest.cs
//	Author: Lu Zexi
//	2014-10-18


namespace GameResource
{
	//Assetbundle request
	public class AbRequest : MonoBehaviour
	{
		public delegate void FINISH_CALLBACK(Dictionary<string , AssetBundle> res);	//finish callback
		public delegate void ERROR_CALLBACK(string path , string error);	//error callback
		
		private FINISH_CALLBACK m_delFinishCallback = null;	//finish callback
		private ERROR_CALLBACK m_delErrorCallback = null;	//error callbcak

		private const int MAX_LOADER_NUM = 20;	//max loader num
		private int m_iLoadIndex = 0;	//index of the load list
		private int m_iCompleteNum = 0;	//loading complete num
		private List<string> m_lstPath = new List<string>();	//the path of the path
		private List<AssetBundleLoader> m_lstLoader = new List<AssetBundleLoader>();	//list loader
		private Dictionary<string , AssetBundle> m_mapRes = new Dictionary<string, AssetBundle>();	//the resource map

		public float Progress
		{
			get
			{
				float sum = 0;
				for(int i = 0; i<this.m_lstLoader.Count ; i++)
				{
					AssetBundleLoader abl = this.m_lstLoader[i];
					sum += abl.Progess;
				}
				return sum / this.m_lstLoader.Count;
			}
		}

		//create request
		public static AbRequest Create(FINISH_CALLBACK finish_callback = null , ERROR_CALLBACK error_callback = null)
		{
			GameObject go = new GameObject("AbRequest");
			AbRequest req = go.AddComponent<AbRequest>();
			req.m_delFinishCallback = finish_callback;
			req.m_delErrorCallback = error_callback;
			req.m_iLoadIndex = 0;
			req.m_iCompleteNum = 0;
			req.m_lstPath.Clear();
			req.m_lstLoader.Clear();
			req.m_mapRes.Clear();
			return req;
		}

		//request
		public void Request(string path)
		{
			if(!this.m_mapRes.ContainsKey(path))
			{
				this.m_lstPath.Add(path);
				this.m_mapRes.Add(path,null);
			}
		}

		//disport
		public void Disport()
		{
			this.m_lstLoader.Clear();
			foreach( KeyValuePair<string,AssetBundle> item in this.m_mapRes )
			{
				item.Value.Unload(false);
			}
			this.m_mapRes.Clear();
		}

		//get asset bundle
		public AssetBundle GetAssetBundle(string name)
		{
			if(this.m_mapRes.ContainsKey(name))
				return this.m_mapRes[name];
			return null;
		}

		//error callback
		protected void ErrorCallback(string path , string error)
		{
			if(this.m_delErrorCallback != null)
			{
				this.m_delErrorCallback(path,error);
			}
		}

		//finish callback
		protected void FinishCallback(string path , AssetBundle obj)
		{
			this.m_mapRes[path] = obj;
			this.m_iCompleteNum++;
			if(this.m_iCompleteNum == this.m_lstPath.Count)
			{
				if(this.m_delFinishCallback == null ) Debug.LogError("The finish callback is null.");
				else this.m_delFinishCallback(this.m_mapRes);
				GameObject.DestroyImmediate(this.gameObject);
			}
		}

		//update
		void Update()
		{
			if(this.m_iCompleteNum == this.m_iLoadIndex)
			{
				if(this.m_iLoadIndex < this.m_lstPath.Count)
				{
					for(int i=0 ; i<MAX_LOADER_NUM && i <this.m_lstPath.Count ; i++)
					{
						AssetBundleLoader loader = AssetBundleLoader.LoadWww(this.m_lstPath[this.m_iLoadIndex+i],FinishCallback,ErrorCallback);
						this.m_lstLoader.Add(loader);
					}
					this.m_iLoadIndex += MAX_LOADER_NUM;
					if(this.m_iLoadIndex > this.m_lstPath.Count)
						this.m_iLoadIndex = this.m_lstPath.Count;
				}
			}
		}
	}
}