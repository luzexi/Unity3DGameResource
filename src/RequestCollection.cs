

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//	RequestCollector.cs
//	Author: Lu Zexi
//	2014-09-19



namespace Game.Resource
{
	using REQUEST_ERROR_CALLBACK = System.Action<string>;

	//Resources request collector
	public class RequestCollection : MonoBehaviour
	{
		public delegate void REQUEST_COMPLETE_CALLBACK(Dictionary<string,object> resMap);

		private List<ResourceRequireOwner> m_lstOwner = new List<ResourceRequireOwner>();

		public REQUEST_COMPLETE_CALLBACK m_delCompleteCallback = null;
		public REQUEST_ERROR_CALLBACK m_delErrorCallback = null;

		//temp var
		private float checkTime;


		/// <summary>
		/// Create this instance.
		/// </summary>
		public static RequestCollection Create()
		{
			GameObject go = new GameObject("RequestCollection");
			RequestCollection col = go.AddComponent<RequestCollection>();
			return col;
		}

		void Update()
		{
			if(Time.time - checkTime > 0.3f)
			{
				checkTime = Time.time;
				bool finish = true;
				foreach( ResourceRequireOwner item in this.m_lstOwner )
				{
					if( !item.m_bComplete )
					{
						finish = false;
					}
				}

				if(finish)
				{
					Dictionary<string , object> resMap = new Dictionary<string , object>();
					foreach (ResourceRequireOwner item in this.m_lstOwner)
					{
						resMap[item.m_cResName] = item.m_cAsset;
					}
					if(this.m_delCompleteCallback != null)
						this.m_delCompleteCallback(resMap);
					this.m_lstOwner.Clear();
					GameObject.Destroy(this.gameObject);
				}
			}
		}

//===================================== Resource ===================================

		public ResourceRequireOwner RequestPrefab( string path )
		{
			ResourceRequireOwner owner = new ResourceRequireOwner();
			owner.m_bComplete = true;
			owner.m_cResName = path;
			owner.m_eResType = RESOURCE_TYPE.PREFAB;
			owner.m_cAsset = Resources.Load(path);
			this.m_lstOwner.Add(owner);
			return owner;
		}


//===================================== texture ===================================

		/// <summary>
		/// Requests the texture.
		/// </summary>
		/// <returns>The texture.</returns>
		/// <param name="path">Path.</param>
		public ResourceRequireOwner RequestTexture( string path)
		{
			ResourceRequireOwner owner = ResourceMgr.RequestResouce(
				path , 0 , 0 , false , 0 ,true, RESOURCE_TYPE.WEB_TEXTURE ,
				ENCRYPT_TYPE.NORMAL , null,this.m_delErrorCallback ,null);
			this.m_lstOwner.Add(owner);
			return owner;
		}
		
		/// <summary>
		/// Requests the texture.
		/// </summary>
		/// <returns>The texture.</returns>
		/// <param name="path">Path.</param>
		/// <param name="arg">Argument.</param>
		public ResourceRequireOwner RequestTexture( string path , long utime )
		{
			ResourceRequireOwner owner = ResourceMgr.RequestResouce(
				path , 0 , 0 , true , utime ,true, RESOURCE_TYPE.WEB_TEXTURE ,
				ENCRYPT_TYPE.NORMAL , null,this.m_delErrorCallback ,null);
			this.m_lstOwner.Add(owner);
			return owner;
		}
//===================================== assetbundle ===================================

		/// <summary>
		/// Requests the asset bundle.
		/// </summary>
		/// <returns>The asset bundle.</returns>
		/// <param name="path">Path.</param>
		public ResourceRequireOwner RequestAssetBundle( string path)
		{
			ResourceRequireOwner owner = ResourceMgr.RequestResouce(
				path , 0 , 0 , false , 0 , true , RESOURCE_TYPE.WEB_ASSETBUNLDE ,
				ENCRYPT_TYPE.NORMAL , null,this.m_delErrorCallback , null);
			this.m_lstOwner.Add(owner);
			return owner;
		}

		/// <summary>
		/// Requests the asset bundle.
		/// </summary>
		/// <returns>The asset bundle.</returns>
		/// <param name="path">Path.</param>
		/// <param name="arg">Argument.</param>
		public ResourceRequireOwner RequestAssetBundle( string path , long utime )
		{
			ResourceRequireOwner owner = ResourceMgr.RequestResouce(
				path , 0 , 0 , true , utime , true , RESOURCE_TYPE.WEB_ASSETBUNLDE ,
				ENCRYPT_TYPE.NORMAL , null ,this.m_delErrorCallback , null);
			this.m_lstOwner.Add(owner);
			return owner;
		}

//===================================== text str ===================================


		/// <summary>
		/// Requests the text string.
		/// </summary>
		/// <returns>The text string.</returns>
		/// <param name="path">Path.</param>
		public ResourceRequireOwner RequestTextStr( string path)
		{
			ResourceRequireOwner owner =  ResourceMgr.RequestResouce(
				path , 0 , 0 , false , 0 , true , RESOURCE_TYPE.WEB_TEXT_STR ,
				ENCRYPT_TYPE.NORMAL , null , this.m_delErrorCallback , null
				);
			this.m_lstOwner.Add(owner);
			return owner;
		}
		
		/// <summary>
		/// Requests the text string.
		/// </summary>
		/// <returns>The text string.</returns>
		/// <param name="path">Path.</param>
		/// <param name="utime">Utime.</param>
		public ResourceRequireOwner RequestTextStr( string path , long utime )
		{
			ResourceRequireOwner owner = ResourceMgr.RequestResouce(
				path , 0 , 0 , true , utime , true , RESOURCE_TYPE.WEB_TEXT_STR ,
				ENCRYPT_TYPE.NORMAL , null , this.m_delErrorCallback , null
				);
			this.m_lstOwner.Add(owner);
			return owner;
		}

//===================================== text byte ===================================

		/// <summary>
		/// Requests the text bytes.
		/// </summary>
		/// <returns>The text bytes.</returns>
		/// <param name="path">Path.</param>
		public ResourceRequireOwner RequestTextBytes( string path)
		{
			ResourceRequireOwner owner = ResourceMgr.RequestResouce(
				path , 0 , 0 , false , 0 , true , RESOURCE_TYPE.WEB_TEXT_BYTES ,
				ENCRYPT_TYPE.NORMAL , null , this.m_delErrorCallback , null
				);
			this.m_lstOwner.Add(owner);
			return owner;
		}
		
		/// <summary>
		/// Requests the text bytes.
		/// </summary>
		/// <returns>The text bytes.</returns>
		/// <param name="path">Path.</param>
		/// <param name="utime">Utime.</param>
		public ResourceRequireOwner RequestTextBytes( string path , long utime )
		{
			ResourceRequireOwner owner = ResourceMgr.RequestResouce(
				path , 0 , 0 , true , utime , true , RESOURCE_TYPE.WEB_TEXT_BYTES ,
				ENCRYPT_TYPE.NORMAL , null , this.m_delErrorCallback , null
				);
			this.m_lstOwner.Add(owner);
			return owner;
		}
		
//===================================== audio clip ===================================

		/// <summary>
		/// Requests the audio clip.
		/// </summary>
		/// <returns>The audio clip.</returns>
		/// <param name="path">Path.</param>
		public ResourceRequireOwner RequestAudioClip( string path)
		{
			ResourceRequireOwner owner = ResourceMgr.RequestResouce(
				path , 0 , 0 , false , 0 , true , RESOURCE_TYPE.WEB_AUDIOCLIP ,
				ENCRYPT_TYPE.NORMAL , null , this.m_delErrorCallback , null
				);
			this.m_lstOwner.Add(owner);
			return owner;
		}
		
		/// <summary>
		/// Requests the audio clip.
		/// </summary>
		/// <returns>The audio clip.</returns>
		/// <param name="path">Path.</param>
		/// <param name="utime">Utime.</param>
		public ResourceRequireOwner RequestAudioClip( string path , long utime )
		{
			ResourceRequireOwner owner = ResourceMgr.RequestResouce(
				path , 0 , 0 , true , utime , true , RESOURCE_TYPE.WEB_AUDIOCLIP ,
				ENCRYPT_TYPE.NORMAL , null , this.m_delErrorCallback , null
				);
			this.m_lstOwner.Add(owner);
			return owner;
		}

//===================================== movie ===================================

		/// <summary>
		/// Requests the movie.
		/// </summary>
		/// <returns>The movie.</returns>
		/// <param name="path">Path.</param>
		public ResourceRequireOwner RequestMovie( string path)
		{
			ResourceRequireOwner owner = ResourceMgr.RequestResouce(
				path , 0 , 0 , false , 0 , true , RESOURCE_TYPE.WEB_MOVIETEXTURE ,
				ENCRYPT_TYPE.NORMAL , null , this.m_delErrorCallback , null
				);
			this.m_lstOwner.Add(owner);
			return owner;
		}
		
		/// <summary>
		/// Requests the movie.
		/// </summary>
		/// <returns>The movie.</returns>
		/// <param name="path">Path.</param>
		/// <param name="utime">Utime.</param>
		public ResourceRequireOwner RequestMovie( string path , long utime )
		{
			ResourceRequireOwner owner = ResourceMgr.RequestResouce(
				path , 0 , 0 , true , utime , true , RESOURCE_TYPE.WEB_MOVIETEXTURE ,
				ENCRYPT_TYPE.NORMAL , null , this.m_delErrorCallback , null
				);
			this.m_lstOwner.Add(owner);
			return owner;
		}
		
	}
	
}
