using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//	ResAPI.cs
//	Author: Lu Zexi
//	2014-10-18


namespace Game.Resource
{
	/// <summary>
	/// Res API.
	/// </summary>
	public class ResAPI : MonoBehaviour
	{
		public delegate void DOWN_FINISH_CALLBACK(Dictionary<string , object> res);
		public DOWN_FINISH_CALLBACK m_delFinishCallback=null;	//finish callback
		private LoadPackage m_cLoadPackage=null;	//loader
		private int m_iLoadIndex = 0;	//index of the load list
		private List<string> m_lstPath = new List<string>();	//the path of the path
		private List<RESOURCE_TYPE> m_lstResType = new List<RESOURCE_TYPE>();	//the type of the resource
		private List<long> m_lstFTime = new List<long>();	//the file of the time
		private Dictionary<string , object> m_mapRes = new Dictionary<string, object>();	//the resource map

		//
		public static ResAPI Create()
		{
			GameObject go = new GameObject("RequestAPI");
			ResAPI api = go.AddComponent<ResAPI>();
			return api;
		}

		//
		public void Request(string path , RESOURCE_TYPE resType = RESOURCE_TYPE.WEB_ASSETBUNLDE , long utime = 0 )
		{
			if( !this.m_mapRes.ContainsKey(path) )
			{
				this.m_lstPath.Add(path);
				this.m_lstResType.Add(resType);
				this.m_lstFTime.Add(utime);
				this.m_mapRes.Add(path,null);
			}
		}

		//
		protected virtual void error_callback(string error_str , object obj)
		{
			//
		}

		//
		private void finish_callback(string path , object obj)
		{
			this.m_mapRes[path] = obj;
		}

		//
		void Update()
		{
			if(this.m_iLoadIndex < this.m_lstPath.Count )
			{
				if( this.m_cLoadPackage == null )
				{
					this.m_cLoadPackage = LoadPackage.StartWWW(this.m_lstPath[this.m_iLoadIndex] ,
					                                           true , this.m_lstFTime[this.m_iLoadIndex],
					                                           finish_callback , error_callback , 
					                                           this.m_lstResType[this.m_iLoadIndex]);
				}
				if( this.m_cLoadPackage.Complete )
				{
					this.m_cLoadPackage = null;
					this.m_iLoadIndex++;
				}
			}
			else
			{
				if(this.m_delFinishCallback == null ) Debug.LogError("The finish callback is null.");
				else this.m_delFinishCallback(this.m_mapRes);
				GameObject.DestroyImmediate(this.gameObject);
			}
		}
	}
}