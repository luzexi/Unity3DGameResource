using UnityEngine;
using System.Collections;

using REQUEST_FINISH_CALLBACK = System.Action<string , object , object[]>;
using REQUEST_ERROR_CALLBACK = System.Action<string>;


//	Request.cs
//	Author: Lu Zexi
//	2014-07-07


namespace Game.Resource
{
	/// <summary>
	/// 资源管理类
	/// </summary>
	public partial class ResourceMgr : MonoBehaviour
	{

//===================================== texture ===================================

		/// <summary>
		/// Requests the texture.
		/// </summary>
		/// <returns>The texture.</returns>
		/// <param name="path">Path.</param>
		public static ResourceRequireOwner RequestTexture( string path)
		{
			return RequestResouce(
				path , 0 , 0 , false , 0 ,false, RESOURCE_TYPE.WEB_TEXTURE ,
				ENCRYPT_TYPE.NORMAL , null,null ,null);
		}
		
		/// <summary>
		/// Requests the texture.
		/// </summary>
		/// <returns>The texture.</returns>
		/// <param name="path">Path.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestTexture( string path , long utime )
		{
			return RequestResouce(
				path , 0 , 0 , true , utime ,false, RESOURCE_TYPE.WEB_TEXTURE ,
				ENCRYPT_TYPE.NORMAL , null,null ,null);
		}
		
		/// <summary>
		/// Requests the texture.
		/// </summary>
		/// <returns>The texture.</returns>
		/// <param name="path">Path.</param>
		/// <param name="finish_callback">Finish_callback.</param>
		/// <param name="error_callback">Error_callback.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestTexture(
			string path , REQUEST_FINISH_CALLBACK finish_callback,
			REQUEST_ERROR_CALLBACK error_callback, params object[] arg
			)
		{
			return RequestResouce(
				path,0,0,false , 0 , true , RESOURCE_TYPE.WEB_TEXTURE,
				ENCRYPT_TYPE.NORMAL , finish_callback ,error_callback, arg
				);
		}
		
		/// <summary>
		/// Requests the texture.
		/// </summary>
		/// <returns>The texture.</returns>
		/// <param name="path">Path.</param>
		/// <param name="CALLBACK">CALLBAC.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestTexture
			(
				string path , long utime , REQUEST_FINISH_CALLBACK CALLBACK ,
				REQUEST_ERROR_CALLBACK error_callback,  params object[] arg
				)
		{
			return RequestResouce(
				path,0,0,true , utime , true , RESOURCE_TYPE.WEB_TEXTURE,
				ENCRYPT_TYPE.NORMAL , CALLBACK ,error_callback, arg
				);
		}

//===================================== assetbundle ===================================

		/// <summary>
		/// Requests the asset bundle.
		/// </summary>
		/// <returns>The asset bundle.</returns>
		/// <param name="path">Path.</param>
		public static ResourceRequireOwner RequestAssetBundle( string path)
		{
			return RequestResouce(
				path , 0 , 0 , false , 0 , false , RESOURCE_TYPE.WEB_ASSETBUNLDE ,
				ENCRYPT_TYPE.NORMAL , null,null , null);
		}

		/// <summary>
		/// Requests the asset bundle.
		/// </summary>
		/// <returns>The asset bundle.</returns>
		/// <param name="path">Path.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestAssetBundle( string path , long utime )
		{
			return RequestResouce(
				path , 0 , 0 , true , utime , false , RESOURCE_TYPE.WEB_ASSETBUNLDE ,
				ENCRYPT_TYPE.NORMAL , null,null , null);
		}

		/// <summary>
		/// Requests the asset bundle.
		/// </summary>
		/// <returns>The asset bundle.</returns>
		/// <param name="path">Path.</param>
		/// <param name="finish_callback">Finish_callback.</param>
		/// <param name="error_callback">Error_callback.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestAssetBundle
			(
				string path , REQUEST_FINISH_CALLBACK finish_callback ,
				REQUEST_ERROR_CALLBACK error_callback, params object[] arg
			)
		{
			return RequestResouce(
				path , 0 , 0 , true ,0 , true , RESOURCE_TYPE.WEB_ASSETBUNLDE ,
				ENCRYPT_TYPE.NORMAL , finish_callback ,error_callback, arg);
		}

		/// <summary>
		/// Requests the asset bundle.
		/// </summary>
		/// <returns>The asset bundle.</returns>
		/// <param name="path">Path.</param>
		/// <param name="CALLBACK">CALLBAC.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestAssetBundle
			(
				string path ,long utime , REQUEST_FINISH_CALLBACK CALLBACK ,
				REQUEST_ERROR_CALLBACK error_callback, params object[] arg
				)
		{
			return RequestResouce(
				path , 0 , 0 , true ,utime , true , RESOURCE_TYPE.WEB_ASSETBUNLDE ,
				ENCRYPT_TYPE.NORMAL , CALLBACK ,error_callback, arg);
		}

//===================================== text str ===================================


		/// <summary>
		/// Requests the text string.
		/// </summary>
		/// <returns>The text string.</returns>
		/// <param name="path">Path.</param>
		public static ResourceRequireOwner RequestTextStr( string path)
		{
			return RequestResouce(
				path , 0 , 0 , false , 0 , false , RESOURCE_TYPE.WEB_TEXT_STR ,
				ENCRYPT_TYPE.NORMAL , null , null , null
				);
		}
		
		/// <summary>
		/// Requests the text string.
		/// </summary>
		/// <returns>The text string.</returns>
		/// <param name="path">Path.</param>
		/// <param name="utime">Utime.</param>
		public static ResourceRequireOwner RequestTextStr( string path , long utime )
		{
			return RequestResouce(
				path , 0 , 0 , true , utime , false , RESOURCE_TYPE.WEB_TEXT_STR ,
				ENCRYPT_TYPE.NORMAL , null , null , null
				);
		}
		
		/// <summary>
		/// Requests the text string.
		/// </summary>
		/// <returns>The text string.</returns>
		/// <param name="path">Path.</param>
		/// <param name="finish_callback">Finish_callback.</param>
		/// <param name="error_callback">Error_callback.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestTextStr
			(
				string path , REQUEST_FINISH_CALLBACK finish_callback ,
				REQUEST_ERROR_CALLBACK error_callback, params object[] arg
				)
		{
			return RequestResouce(
				path , 0 , 0 , true ,0 , true , RESOURCE_TYPE.WEB_TEXT_STR ,
				ENCRYPT_TYPE.NORMAL , finish_callback ,error_callback, arg
				);
		}
		
		/// <summary>
		/// Requests the text string.
		/// </summary>
		/// <returns>The text string.</returns>
		/// <param name="path">Path.</param>
		/// <param name="utime">Utime.</param>
		/// <param name="CALLBACK">CALLBAC.</param>
		/// <param name="error_callback">Error_callback.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestTextStr
			(
				string path ,long utime , REQUEST_FINISH_CALLBACK CALLBACK ,
				REQUEST_ERROR_CALLBACK error_callback, params object[] arg
				)
		{
			return RequestResouce(
				path , 0 , 0 , true ,utime , true , RESOURCE_TYPE.WEB_TEXT_STR ,
				ENCRYPT_TYPE.NORMAL , CALLBACK ,error_callback, arg
				);
		}

//===================================== text byte ===================================

		/// <summary>
		/// Requests the text bytes.
		/// </summary>
		/// <returns>The text bytes.</returns>
		/// <param name="path">Path.</param>
		public static ResourceRequireOwner RequestTextBytes( string path)
		{
			return RequestResouce(
				path , 0 , 0 , false , 0 , false , RESOURCE_TYPE.WEB_TEXT_BYTES ,
				ENCRYPT_TYPE.NORMAL , null , null , null
				);
		}
		
		/// <summary>
		/// Requests the text bytes.
		/// </summary>
		/// <returns>The text bytes.</returns>
		/// <param name="path">Path.</param>
		/// <param name="utime">Utime.</param>
		public static ResourceRequireOwner RequestTextBytes( string path , long utime )
		{
			return RequestResouce(
				path , 0 , 0 , true , utime , false , RESOURCE_TYPE.WEB_TEXT_BYTES ,
				ENCRYPT_TYPE.NORMAL , null , null , null
				);
		}
		
		/// <summary>
		/// Requests the text bytes.
		/// </summary>
		/// <returns>The text bytes.</returns>
		/// <param name="path">Path.</param>
		/// <param name="finish_callback">Finish_callback.</param>
		/// <param name="error_callback">Error_callback.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestTextBytes
			(
				string path , REQUEST_FINISH_CALLBACK finish_callback ,
				REQUEST_ERROR_CALLBACK error_callback, params object[] arg
				)
		{
			return RequestResouce(
				path , 0 , 0 , true ,0 , true , RESOURCE_TYPE.WEB_TEXT_BYTES ,
				ENCRYPT_TYPE.NORMAL , finish_callback ,error_callback, arg
				);
		}
		
		/// <summary>
		/// Requests the text bytes.
		/// </summary>
		/// <returns>The text bytes.</returns>
		/// <param name="path">Path.</param>
		/// <param name="utime">Utime.</param>
		/// <param name="CALLBACK">CALLBAC.</param>
		/// <param name="error_callback">Error_callback.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestTextBytes
			(
				string path ,long utime , REQUEST_FINISH_CALLBACK CALLBACK ,
				REQUEST_ERROR_CALLBACK error_callback, params object[] arg
				)
		{
			return RequestResouce(
				path , 0 , 0 , true ,utime , true , RESOURCE_TYPE.WEB_TEXT_BYTES ,
				ENCRYPT_TYPE.NORMAL , CALLBACK ,error_callback, arg
				);
		}
		
//===================================== audio clip ===================================

		/// <summary>
		/// Requests the audio clip.
		/// </summary>
		/// <returns>The audio clip.</returns>
		/// <param name="path">Path.</param>
		public static ResourceRequireOwner RequestAudioClip( string path)
		{
			return RequestResouce(
				path , 0 , 0 , false , 0 , false , RESOURCE_TYPE.WEB_AUDIOCLIP ,
				ENCRYPT_TYPE.NORMAL , null , null , null
				);
		}
		
		/// <summary>
		/// Requests the audio clip.
		/// </summary>
		/// <returns>The audio clip.</returns>
		/// <param name="path">Path.</param>
		/// <param name="utime">Utime.</param>
		public static ResourceRequireOwner RequestAudioClip( string path , long utime )
		{
			return RequestResouce(
				path , 0 , 0 , true , utime , false , RESOURCE_TYPE.WEB_AUDIOCLIP ,
				ENCRYPT_TYPE.NORMAL , null , null , null
				);
		}
		
		/// <summary>
		/// Requests the audio clip.
		/// </summary>
		/// <returns>The audio clip.</returns>
		/// <param name="path">Path.</param>
		/// <param name="finish_callback">Finish_callback.</param>
		/// <param name="error_callback">Error_callback.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestAudioClip
			(
				string path , REQUEST_FINISH_CALLBACK finish_callback ,
				REQUEST_ERROR_CALLBACK error_callback, params object[] arg
				)
		{
			return RequestResouce(
				path , 0 , 0 , true ,0 , true , RESOURCE_TYPE.WEB_AUDIOCLIP ,
				ENCRYPT_TYPE.NORMAL , finish_callback ,error_callback, arg
				);
		}
		
		/// <summary>
		/// Requests the audio clip.
		/// </summary>
		/// <returns>The audio clip.</returns>
		/// <param name="path">Path.</param>
		/// <param name="utime">Utime.</param>
		/// <param name="CALLBACK">CALLBAC.</param>
		/// <param name="error_callback">Error_callback.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestAudioClip
			(
				string path ,long utime , REQUEST_FINISH_CALLBACK CALLBACK ,
				REQUEST_ERROR_CALLBACK error_callback, params object[] arg
				)
		{
			return RequestResouce(
				path , 0 , 0 , true ,utime , true , RESOURCE_TYPE.WEB_AUDIOCLIP ,
				ENCRYPT_TYPE.NORMAL , CALLBACK ,error_callback, arg
				);
		}

//===================================== movie ===================================

		/// <summary>
		/// Requests the movie.
		/// </summary>
		/// <returns>The movie.</returns>
		/// <param name="path">Path.</param>
		public static ResourceRequireOwner RequestMovie( string path)
		{
			return RequestResouce(
				path , 0 , 0 , false , 0 , false , RESOURCE_TYPE.WEB_MOVIETEXTURE ,
				ENCRYPT_TYPE.NORMAL , null , null , null
				);
		}
		
		/// <summary>
		/// Requests the movie.
		/// </summary>
		/// <returns>The movie.</returns>
		/// <param name="path">Path.</param>
		/// <param name="utime">Utime.</param>
		public static ResourceRequireOwner RequestMovie( string path , long utime )
		{
			return RequestResouce(
				path , 0 , 0 , true , utime , false , RESOURCE_TYPE.WEB_MOVIETEXTURE ,
				ENCRYPT_TYPE.NORMAL , null , null , null
				);
		}
		
		/// <summary>
		/// Requests the movie.
		/// </summary>
		/// <returns>The movie.</returns>
		/// <param name="path">Path.</param>
		/// <param name="finish_callback">Finish_callback.</param>
		/// <param name="error_callback">Error_callback.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestMovie
			(
				string path , REQUEST_FINISH_CALLBACK finish_callback ,
				REQUEST_ERROR_CALLBACK error_callback, params object[] arg
				)
		{
			return RequestResouce(
				path , 0 , 0 , true ,0 , true , RESOURCE_TYPE.WEB_MOVIETEXTURE ,
				ENCRYPT_TYPE.NORMAL , finish_callback ,error_callback, arg
				);
		}
		
		/// <summary>
		/// Requests the movie.
		/// </summary>
		/// <returns>The movie.</returns>
		/// <param name="path">Path.</param>
		/// <param name="utime">Utime.</param>
		/// <param name="CALLBACK">CALLBAC.</param>
		/// <param name="error_callback">Error_callback.</param>
		/// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestMovie
			(
				string path ,long utime , REQUEST_FINISH_CALLBACK CALLBACK ,
				REQUEST_ERROR_CALLBACK error_callback, params object[] arg
				)
		{
			return RequestResouce(
				path , 0 , 0 , true ,utime , true , RESOURCE_TYPE.WEB_MOVIETEXTURE ,
				ENCRYPT_TYPE.NORMAL , CALLBACK ,error_callback, arg
				);
		}
	}
}
