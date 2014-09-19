

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//	RequestCollector.cs
//	Author: Lu Zexi
//	2014-09-19



namespace Game.Resource
{

	//Resources request collector
	public class RequestCollection : MonoBehaviour
	{
		private List<ResourceRequireOwner> m_lstOwner = new List<ResourceRequireOwner>();
		
		//temp var
		private float checkTime;

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
					this.CompleteCallback(resMap);
					this.m_lstOwner.Clear();
				}
			}
		}

		//complete callback
		protected virtual void CompleteCallback( Dictionary<string , object> resMap)
		{
			//
		}

		//error callback
		protected virtual void ErrorCallback(string error)
		{
			//
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
				ENCRYPT_TYPE.NORMAL , null,ErrorCallback ,null);
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
				ENCRYPT_TYPE.NORMAL , null,ErrorCallback ,null);
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
				ENCRYPT_TYPE.NORMAL , null,ErrorCallback , null);
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
				ENCRYPT_TYPE.NORMAL , null ,ErrorCallback , null);
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
				ENCRYPT_TYPE.NORMAL , null , ErrorCallback , null
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
				ENCRYPT_TYPE.NORMAL , null , ErrorCallback , null
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
				ENCRYPT_TYPE.NORMAL , null , ErrorCallback , null
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
				ENCRYPT_TYPE.NORMAL , null , ErrorCallback , null
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
				ENCRYPT_TYPE.NORMAL , null , ErrorCallback , null
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
				ENCRYPT_TYPE.NORMAL , null , ErrorCallback , null
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
				ENCRYPT_TYPE.NORMAL , null , ErrorCallback , null
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
				ENCRYPT_TYPE.NORMAL , null , ErrorCallback , null
				);
			this.m_lstOwner.Add(owner);
			return owner;
		}
		
	}
	
}
