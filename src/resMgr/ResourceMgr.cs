
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  ResourceManager.cs
//  Lu Zexi
//  2012-7-5

namespace Game.Resource
{
	using REQUEST_FINISH_CALLBACK = System.Action<string , object , object[]>;
	using REQUEST_ERROR_CALLBACK = System.Action<string , object>;

    public delegate byte[] DecryptBytesFunc(byte[] datas);  //解密接口

    /// <summary>
    /// 资源类型
    /// </summary>
    public enum RESOURCE_TYPE
    { 
		NONE,
		PREFAB,
        WEB_TEXTURE,    //网络贴图资源
        WEB_ASSETBUNLDE,     //网络AssetBundle物体资源
        WEB_TEXT_STR,   //网络文本文件资源
        WEB_TEXT_BYTES, //网络2进制文件资源
		WEB_AUDIOCLIP,	//AudioClip
		WEB_MOVIETEXTURE,	//MovieTexture
    }

    /// <summary>
    /// 加密类型
    /// </summary>
    public enum ENCRYPT_TYPE
    { 
        NORMAL,     //无加密
        ENCRYPT,    //有加密
    }

    /// <summary>
    /// 资源需求者
    /// </summary>
    public class ResourceRequireOwner
    {
        public string m_cResName;               //资源名
        //public string m_strResValue;    //资源值
        public RESOURCE_TYPE m_eResType;        //资源类型
		public REQUEST_FINISH_CALLBACK m_delCallBack;  //回调方法
		public REQUEST_ERROR_CALLBACK m_delErrorCall;	//error callback
        public object[] m_vecArg;   //参数
        public bool m_bComplete;    //是否完成
        public object m_cAsset;    //资源变量
    }

	/// <summary>
	/// the load type of the resouces , is normal or is editor.
	/// </summary>
	public enum LOAD_TYPE
	{
		NORMAL = 1,
		EDITOR,
	}

    /// <summary>
    /// 资源管理类
    /// </summary>
    public partial class ResourceMgr : MonoBehaviour
    {
        private const int LOAD_MAX_NUM = 3;		//Max load num
        private const string RESOURCE_POST = ".res";    //资源名后缀

		private LOAD_TYPE m_eLoadType = LOAD_TYPE.NORMAL; //异步加载方式

		//The www resource of the var
		private Dictionary<string, ResourceRequireData> m_mapRes = new Dictionary<string, ResourceRequireData>();   //资源集合
		private List<ResourceRequireData> m_lstRequires = new List<ResourceRequireData>();  //需求数据
		private DecryptBytesFunc m_delDecryptFunc;  //解密接口

		//The Share of resources is can't be unload
		private List<string> m_lstShareResources = new List<string> ();	//The share resources.

        //异步加载
		private Dictionary<string, object> m_mapAsyncLoader = new Dictionary<string, object>();    //异步映射

		private static ResourceMgr s_cInstance;

		public static ResourceMgr sInstance
		{
			get
			{
				if(s_cInstance == null)
				{
					s_cInstance = (new GameObject("ResourcesManager")).AddComponent<ResourceMgr>();
				}
				return s_cInstance;
			}
		}

		/// <summary>
		/// Awake this instance.
		/// </summary>
        void Awake()
        {
            m_delDecryptFunc = CEncrypt.DecryptBytes;
        }

		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		void OnDestroy()
		{
			if(s_cInstance == this )
			{
				s_cInstance = null;
			}
		}

		/// <summary>
		/// update the logic
		/// </summary>
		void Update()
		{
			int sum = 0;
			foreach (ResourceRequireData item in sInstance.m_lstRequires)
			{
				if (item.Start && !item.Complete)
					sum++;
			}
			
			if (sum < LOAD_MAX_NUM)
			{
				sum = 0;
				foreach (ResourceRequireData item in sInstance.m_lstRequires)
				{
					if (!item.Start && !item.Complete)
					{
						item.Initialize();
						sum++;
						if (sum >= LOAD_MAX_NUM)
							break;
					}
				}
			}
		}

		/// <summary>
		/// Switchs the web resources load
		/// </summary>
		public static void SwitchNormal()
		{
			sInstance.m_eLoadType = LOAD_TYPE.NORMAL;
		}

		/// <summary>
		/// Switchs the local resources load
		/// </summary>
		public static void SwitchEditor()
		{
			sInstance.m_eLoadType = LOAD_TYPE.EDITOR;
		}

        /// <summary>
        /// 清除异步加载
        /// </summary>
        public static void ClearAsyncLoad()
        {
			sInstance.m_mapAsyncLoader.Clear();
        }

        /// <summary>
        /// 获取异步进程
        /// </summary>
        /// <returns></returns>
        public static float GetAsyncProcess()
        {
            float rate = 0;
			if (sInstance.m_eLoadType == LOAD_TYPE.NORMAL)
            {
				foreach (KeyValuePair<string, object> item in sInstance.m_mapAsyncLoader)
                {
                    rate += ((AsyncLoader)item.Value).Progress();
                }
            }
            else
            {
				rate = sInstance.m_mapAsyncLoader.Count;
            }

			if (sInstance.m_mapAsyncLoader.Count <= 0)
                return 1f;

			rate /= sInstance.m_mapAsyncLoader.Count;

            return rate;
        }

        /// <summary>
        /// 获取异步加载的资源
        /// </summary>
        /// <param name="resName"></param>
        /// <returns></returns>
        public static UnityEngine.Object GetAsyncObject(string resName)
        {
			if ( sInstance.m_eLoadType == LOAD_TYPE.NORMAL)
            {
				if ( sInstance.m_mapAsyncLoader.ContainsKey(resName))
                {
					return ((AsyncLoader)sInstance.m_mapAsyncLoader[resName]).GetAsset();
                }
                else
                {
                    Debug.LogError("Error: the async object is not exist." + resName);
                }
            }
            else
            {
				if (sInstance.m_mapAsyncLoader.ContainsKey(resName))
                {
					return (UnityEngine.Object)sInstance.m_mapAsyncLoader[resName];
                }
                else
                {
                    Debug.LogError("Error: the async object is not exist." + resName);
                }
            }

            return null;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="path"></param>
        public static void LoadAsync(string resName)
        {
            LoadAsync(resName, resName);
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="resName"></param>
        /// <returns></returns>
        public static void LoadAsync(string path , string resName )
        {
			if (  sInstance.m_eLoadType == LOAD_TYPE.NORMAL )
            {
				if ( sInstance.m_mapRes.ContainsKey(path) )
                {
					if (!sInstance.m_mapAsyncLoader.ContainsKey(resName))
                    {
						AsyncLoader loader = AsyncLoader.StartLoad(((AssetBundle)sInstance.m_mapRes[path].GetAssetObject()), resName);
						sInstance.m_mapAsyncLoader.Add(resName, loader);
                    }
                    else
                    {
                        Debug.Log("The resources is already in the list. " + resName);
                    }
                }
                else
                {
                    Debug.LogError("The resources is not exist. " + path );
                }
            }
            else
            {
				if (!sInstance.m_mapAsyncLoader.ContainsKey(resName))
                {
					UnityEngine.Object obj = ResourceMgr.LoadEditor(resName);
					sInstance.m_mapAsyncLoader.Add(resName, obj);
                }
                else
                {
                    Debug.Log("The resources is already in the list. " + resName);
                }
            }
        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <param name="name"></param>
        public static void UnloadResource(string name)
        {
			if (sInstance.m_mapRes.ContainsKey(name))
            {
                if (!CanUnload(name))
                    return;
				sInstance.m_mapRes[name].Destory( false );
				sInstance.m_lstRequires.Remove(sInstance.m_mapRes[name]);
				sInstance.m_mapRes.Remove(name);
                Resources.UnloadUnusedAssets();
            }
			else
			{
				Debug.Log("the resouce named " + name + " is not exist.");
			}
        }

        /// <summary>
        /// 删除资源资源需求
        /// </summary>
        /// <param name="name"></param>
        /// <param name="owner"></param>
        public static void UnloadResource(string name, ResourceRequireOwner owner)
        {
			if (sInstance.m_mapRes.ContainsKey(name))
            {
				sInstance.m_mapRes[name].RemoveRequireOwner(owner);
                //倘若资源需求为空
				if (sInstance.m_mapRes[name].IsOwnerEmpty())
                {
                    //如果可以卸载 或者 加载未完成则卸载资源
					if (CanUnload(name) || !sInstance.m_mapRes[name].Complete)
                    {
						sInstance.m_mapRes[name].Destory(false);
						sInstance.m_lstRequires.Remove(sInstance.m_mapRes[name]);
						sInstance.m_mapRes.Remove(name);
                        Resources.UnloadUnusedAssets();
                    }
                }
            }
            else
            {
                Debug.LogError("Has not resource " + name);
            }
        }

        /// <summary>
        /// 是否可以卸载
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static bool CanUnload(string name)
        {
			foreach (string item in sInstance.m_lstShareResources)
			{
				if (name == item || name.StartsWith(item))
					return false;
			}
            return true;
        }

        /// <summary>
        /// 卸载所有不使用资源
        /// </summary>
        public static void UnloadUnusedResources()
        {
            List<string> lst = new List<string>();
			foreach (KeyValuePair<string, ResourceRequireData> item in sInstance.m_mapRes)
            {
                if (!CanUnload(item.Key))
                    continue;

                lst.Add(item.Key);
            }
            foreach (string key in lst)
            {
				sInstance.m_mapRes[key].Destory( false );
				sInstance.m_mapRes.Remove(key);
            }
			sInstance.m_lstRequires.Clear();
			sInstance.m_mapAsyncLoader.Clear();
            Resources.UnloadUnusedAssets();
            //GC.Collect();
        }



        /// <summary>
        /// Requests the resouce.
        /// </summary>
        /// <returns>The resouce.</returns>
        /// <param name="path">Path.</param>
        /// <param name="crc">Crc.</param>
        /// <param name="version">Version.</param>
        /// <param name="autoSave">If set to <c>true</c> auto save.</param>
        /// <param name="utime">Utime.</param>
        /// <param name="autoClear">If set to <c>true</c> auto clear.</param>
        /// <param name="resType">Res type.</param>
        /// <param name="encrypt_type">Encrypt_type.</param>
        /// <param name="func">Func.</param>
        /// <param name="error_call">Error_call.</param>
        /// <param name="arg">Argument.</param>
		public static ResourceRequireOwner RequestResouce(
			string path , bool autoSave , long utime , bool autoClear , RESOURCE_TYPE resType,
			ENCRYPT_TYPE encrypt_type, REQUEST_FINISH_CALLBACK func ,
			REQUEST_ERROR_CALLBACK error_call,object[] arg
			)
        {
			if (sInstance.m_delDecryptFunc == null && encrypt_type == ENCRYPT_TYPE.ENCRYPT )
            {
                // Error
                // 没有资源解密接口
                return null;
            }

			string resName = (new Uri(path)).AbsolutePath;

            ResourceRequireOwner owner = new ResourceRequireOwner();
            owner.m_cResName = resName;
//            owner.m_strResValue = resValue;
            owner.m_delCallBack = func;
			owner.m_delErrorCall = error_call;
            owner.m_eResType = resType;
            owner.m_vecArg = arg;
			if ( sInstance.m_eLoadType != LOAD_TYPE.NORMAL )
			{ 
				UnityEngine.Object obj = LoadEditor(resName);
				if (!sInstance.m_mapRes.ContainsKey(resName))
				{
					object resData = null;
					switch( resType )
					{
					case RESOURCE_TYPE.WEB_ASSETBUNLDE:
					case RESOURCE_TYPE.WEB_TEXTURE:
						resData = obj;
						break;
					case RESOURCE_TYPE.WEB_TEXT_BYTES:
						resData = (obj as TextAsset).bytes;
						break;
					case RESOURCE_TYPE.WEB_TEXT_STR:
						resData = (obj as TextAsset).text;
						break;
					}
					ResourceRequireData data = new ResourceRequireData(resData);
					sInstance.m_mapRes.Add(resName, data);
				}
				sInstance.m_mapRes[resName].AddRequireOwner(owner);
				sInstance.m_mapRes[resName].Used();
				if(sInstance.m_mapRes[resName].Complete)
				{
					sInstance.m_mapRes[resName].CompleteCallBack();
				}
				return owner;
			}

			if (sInstance.m_mapRes.ContainsKey(resName))
            {
                //增加需求者
				sInstance.m_mapRes[resName].AddRequireOwner(owner);
				sInstance.m_mapRes[resName].Used();
				if (sInstance.m_mapRes[resName].Complete)
                {
					sInstance.m_mapRes[resName].CompleteCallBack();
                }
            }
            else
            {
				ResourceRequireData rrd = new ResourceRequireData(
					path , autoSave ,utime ,autoClear, resType,
					encrypt_type, sInstance.m_delDecryptFunc);
				sInstance.m_lstRequires.Add(rrd);
				sInstance.m_mapRes.Add(resName, rrd);
                rrd.AddRequireOwner(owner);
            }

            return owner;
        }

        /// <summary>
        /// 删除所有资源
        /// </summary>
        public static void Destory()
        {
			foreach (ResourceRequireData item in sInstance.m_mapRes.Values)
            {
                item.Destory( true );
            }
			sInstance.m_mapRes.Clear();
			sInstance.m_lstRequires.Clear();
			sInstance.m_mapAsyncLoader.Clear();
			GameObject.DestroyImmediate(sInstance.gameObject);
			s_cInstance = null;
        }

        /// <summary>
        /// 清楚进度
        /// </summary>
        public static void ClearProgress()
        {
			sInstance.m_lstRequires.Clear();
        }

        /// <summary>
        /// 获取进度
        /// </summary>
        /// <returns></returns>
        public static float GetProgress()
        {
            float progess = 0;
			foreach (ResourceRequireData item in sInstance.m_lstRequires)
            {
                progess += item.GetProgress();
            }

			if ( sInstance.m_lstRequires.Count > 0)
            {
				return progess / sInstance.m_lstRequires.Count;
            }

            return 1f;
        }

        /// <summary>
        /// 完成加载
        /// </summary>
        /// <returns></returns>
        public static bool IsComplete()
        {
            bool finish = true;
			foreach (ResourceRequireData item in sInstance.m_lstRequires)
            {
                if (!item.Complete)
                    finish = false;
            }

            return finish;
        }

        //判断是否完成加载某个资源
        public static bool IsComplete(string resName)
        {
            return  sInstance.m_mapRes.ContainsKey(resName) && sInstance.m_mapRes[resName].Complete;           
        }

    }



}