using UnityEngine;
using System.Collections;

//	MgrLoad.cs
//	Author: Lu Zexi
//	2014-09-14


namespace Game.Resource
{
	/// <summary>
	/// 资源管理类
	/// </summary>
	public partial class ResourceMgr : MonoBehaviour
	{
		/// <summary>
		/// Load editor resources
		/// </summary>
		/// <returns>The loacl.</returns>
		private static UnityEngine.Object LoadEditor( string path )
		{
			UnityEngine.Object obj = Resources.LoadAssetAtPath( Application.dataPath + path , typeof(UnityEngine.Object));
			if(obj != null )
			{
				return obj;
			}
			
			return null;
		}
		
		/// <summary>
		/// Loads the resources in the unity.
		/// </summary>
		/// <returns>The resources.</returns>
		public static UnityEngine.Object LoadResource( string filePath)
		{
			return UnityEngine.Resources.Load(filePath);
		}
		
		/// <summary>
		/// 读取资源
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static object LoadAsset(string path)
		{
			if (sInstance.m_mapRes.ContainsKey(path))
			{
				sInstance.m_mapRes[path].Used();
				if (sInstance.m_mapRes[path].GetAssetObject() is AssetBundle)
				{
					return (sInstance.m_mapRes[path].GetAssetObject() as AssetBundle).mainAsset;
				}
				return sInstance.m_mapRes[path].GetAssetObject();
			}
			else
			{
				Debug.LogError("Resource is null. path " + path);
			}
			return null;
		}
		
		/// <summary>
		/// load the resource in assetbundle.
		/// </summary>
		/// <returns>The asset.</returns>
		/// <param name="path">Path.</param>
		/// <param name="name">Name.</param>
		public static UnityEngine.Object LoadAsset( string path , string name )
		{
			if (sInstance.m_mapRes.ContainsKey(path))
			{
				sInstance.m_mapRes[path].Used();
				if (sInstance.m_mapRes[path].GetAssetObject() is AssetBundle)
				{
					return (sInstance.m_mapRes[path].GetAssetObject() as AssetBundle).Load(name);
				}
				Debug.LogError("Resource is not assetbundle. path " + path);
			}
			else
			{
				Debug.LogError("Resource is null. path " + path);
			}
			return null;
		}
	}
}

