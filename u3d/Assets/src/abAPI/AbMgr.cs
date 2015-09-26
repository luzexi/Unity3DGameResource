
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//	AbMgr.cs
//	Author: Lu Zexi
//	2014-10-18



namespace GameResource
{
	//assetbundle manager
	public class AbMgr
	{
		public delegate void FINISH_CALLBACK();
		public delegate void ERROR_CALLBACK(string path , string error);

		private static AbMgr sInstance;
		public static AbMgr I
		{
			get
			{
				if(sInstance == null)
				{
					sInstance = new AbMgr();
				}
				return sInstance;
			}
		}

		private FINISH_CALLBACK m_FinishCallback = null;	//finish callback
		private ERROR_CALLBACK m_ErrorCallback = null;		//error callback
		private Dictionary<string,AssetBundle> m_mapRes = new Dictionary<string,AssetBundle>(); //resources
		private AbRequest m_AbRequest = null;

		//progress
		public float Progress
		{
			get
			{
				if(this.m_AbRequest != null)
				{
					return this.m_AbRequest.Progress;
				}
				return 1;
			}
		}

		//request
		public void Request(List<string> paths ,FINISH_CALLBACK finish_callback = null , ERROR_CALLBACK error_callback = null)
		{
			this.m_FinishCallback = finish_callback;
			this.m_ErrorCallback = error_callback;

			this.m_AbRequest = AbRequest.Create(RequestFinishCallback);
			for(int i = 0 ; i < paths.Count ; i++)
			{
				string path = paths[i];
				if(!this.m_mapRes.ContainsKey(path))
				{
					this.m_mapRes.Add(path,null);
					this.m_AbRequest.Request(path);
				}
			}
		}

		//request finish callback
		private void RequestFinishCallback(Dictionary<string,AssetBundle> res)
		{
			foreach( KeyValuePair<string,AssetBundle> item in res )
			{
				this.m_mapRes[item.Key] = item.Value;
			}
			if(this.m_FinishCallback != null)
			{
				this.m_FinishCallback();
			}
		}
	}
}