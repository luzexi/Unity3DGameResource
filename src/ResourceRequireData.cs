using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;


//	ResourceRequireData.cs
//	Author: Lu Zexi
//	2014-06-06



namespace Game.Resource
{
	
	/// <summary>
	/// 资源需求数据
	/// </summary>
	public class ResourceRequireData
	{
		private string m_strFilePath;       //资源地址
		private Uri m_cPathUri;	//the path of uri
		private uint m_iCRC; //CRC码
		private int m_iVersion; //资源版本
		private long m_lUTime;	//Unix Time
		private bool m_bAutoSave;	//auto save of the resources.
		private bool m_bAutoClear;	//auto clear of the resources.
		private float m_fLastUseTime;   //最近使用时间
		private RESOURCE_TYPE m_eResType;   //资源类型
		private ENCRYPT_TYPE m_eEncryType = ENCRYPT_TYPE.NORMAL;   //加密类型
		private List<ResourceRequireOwner> m_lstOwners = new List<ResourceRequireOwner>();    //需求者
		
		private object m_cAsset = null;       //资源包
		private LoadPackage m_cLoader = null;      //加载器
		
		private DecryptBytesFunc m_funDecryptFunc;  //加密解密接口
		private bool m_bComplete = false;   //是否完成
		private bool m_bStart = false;  //是否开始
		public bool Start
		{
			get { return this.m_bStart; }
		}
		
		public ResourceRequireData( object asset )
		{
			this.m_cAsset = asset;
			this.m_bComplete = true;
			this.m_bStart = true;
			this.m_fLastUseTime = Time.fixedTime;
		}
		
		public ResourceRequireData(
			string path, uint crc, int version , bool autosave , long utime , bool autoClear ,
			RESOURCE_TYPE type, ENCRYPT_TYPE encrypt_type, DecryptBytesFunc decryptFunc
			)
		{
			this.m_strFilePath = path;
			this.m_cPathUri = new Uri(path);
			this.m_iCRC = crc;
			this.m_iVersion = version;
			this.m_lUTime = utime;
			this.m_bAutoSave = autosave;
			this.m_bAutoClear = autoClear;
			this.m_eResType = type;
			this.m_eEncryType = encrypt_type;
			this.m_funDecryptFunc = decryptFunc;
			this.m_fLastUseTime = Time.fixedTime;
		}
		
		/// <summary>
		/// 获取最近使用时间
		/// </summary>
		/// <returns></returns>
		public float GetLastUsedTime()
		{
			return this.m_fLastUseTime;
		}
		
		/// <summary>
		/// 被使用接口
		/// </summary>
		public void Used()
		{
			this.m_fLastUseTime = Time.fixedTime;
		}
		
		/// <summary>
		/// 初始化
		/// </summary>
		public void Initialize()
		{
			this.m_bStart = true;

			this.m_cLoader = LoadPackage.StartWWW(
				this.m_strFilePath, this.m_iCRC, this.m_iVersion , this.m_bAutoSave , this.m_lUTime , LoaderCallBack,
				ErrorCallBack , this.m_eResType, this.m_eEncryType, this.m_funDecryptFunc);
			this.m_cLoader.transform.parent = ResourceMgr.sInstance.transform;
		}
		
		/// <summary>
		/// 获取资源物体
		/// </summary>
		/// <returns></returns>
		public object GetAssetObject()
		{
			return this.m_cAsset;
		}
		
		/// <summary>
		/// 加载器回调
		/// </summary>
		/// <param name="path"></param>
		/// <param name="www"></param>
		private void LoaderCallBack(string path, object asset)
		{
			//资源
			this.m_cAsset = asset;
			//回调
			CompleteCallBack();
			
			this.m_bComplete = true;
			
			//删除加载器
			if (this.m_cLoader != null)
				GameObject.Destroy(this.m_cLoader.gameObject);
			this.m_cLoader = null;

			if(this.m_bAutoClear)
			{
				ResourceMgr.UnloadResource(this.m_cPathUri.AbsolutePath);
			}
		}

		/// <summary>
		/// error of the loader callback
		/// </summary>
		/// <param name="str">String.</param>
		private void ErrorCallBack(string str )
		{
			foreach (ResourceRequireOwner item in this.m_lstOwners)
			{
				if (item.m_delErrorCall != null)
				{
					item.m_delErrorCall(str);
				}
			}
		}

		/// <summary>
		/// 是否完成加载
		/// </summary>
		public bool Complete
		{
			get
			{
				return this.m_bComplete;
			}
		}
		
		/// <summary>
		/// 完成回调
		/// </summary>
		public void CompleteCallBack()
		{
			//为了没有死循环，先把所有请求者存为另外的列表
			//将源请求者清除
			List<ResourceRequireOwner> lst = new List<ResourceRequireOwner>(this.m_lstOwners);
			this.m_lstOwners.Clear();
			foreach (ResourceRequireOwner item in lst)
			{
				if (item.m_delCallBack != null)
				{
					item.m_delCallBack(item.m_cResName, this.m_cAsset, item.m_vecArg);
				}
			}
		}
		
		/// <summary>
		/// 增加资源需求者
		/// </summary>
		/// <param name="owner"></param>
		public bool AddRequireOwner(ResourceRequireOwner owner)
		{
			this.m_lstOwners.Add(owner);
			return true;
		}
		
		/// <summary>
		/// 删除需求者
		/// </summary>
		/// <param name="owner"></param>
		/// <returns></returns>
		public bool RemoveRequireOwner(ResourceRequireOwner owner)
		{
			return this.m_lstOwners.Remove(owner);
		}
		
		/// <summary>
		/// 是否拥有者为空
		/// </summary>
		/// <returns></returns>
		public bool IsOwnerEmpty()
		{
			return this.m_lstOwners.Count <= 0;
		}
		
		/// <summary>
		/// 销毁
		/// </summary>
		public void Destory( bool force )
		{
			if (this.m_cLoader != null)
			{
				this.m_cLoader.StopAllCoroutines();
				UnityEngine.Object.Destroy(this.m_cLoader.gameObject);
				UnityEngine.Object.Destroy(this.m_cLoader);
				this.m_cLoader = null;
			}
			if (this.m_cAsset != null && this.m_cAsset is AssetBundle)
			{
				((AssetBundle)this.m_cAsset).Unload(force);
				this.m_cAsset = null;
			}
		}
		
		/// <summary>
		/// 停止加载
		/// </summary>
		public void StopLoader()
		{
			if (this.m_cLoader != null)
			{
				this.m_cLoader.StopAllCoroutines();
				UnityEngine.Object.Destroy(this.m_cLoader.gameObject);
				UnityEngine.Object.Destroy(this.m_cLoader);
				this.m_cLoader = null;
			}
		}
		
		/// <summary>
		/// 获取加载进度
		/// </summary>
		/// <returns></returns>
		public float GetProgress()
		{
			if (this.m_cLoader != null)
			{
				return this.m_cLoader.Progess;
			}
			if (this.m_cAsset != null)
			{
				return 1f;
			}
			if (this.m_bComplete)
				return 1f;
			
			return 0f;
		}
	}
}