
using System;
using System.Collections;
using UnityEngine;

using DOWN_FINISH_CALLBACK = System.Action<string , object>;
using DOWN_ERROR_CALLBACK = System.Action<string , object>;

//  LoadPackage.cs
//  Lu Zexi
//  2012-7-5

namespace Game.Resource
{

    /// <summary>
    /// 加载需求包
    /// </summary>
    public class LoadPackage : MonoBehaviour
    {
        private string m_strPath;   //加载路径
		private long m_lUTime;	//Unix Time
        private bool m_bComplete;   //是否加载完成
        public bool Complete
        {
            get { return this.m_bComplete; }
        }

        private WWW m_cWww;     //WWW
		private float m_fProgess;	//the progess of the www.
        public float Progess
        {
            get
            {
				return this.m_fProgess;
            }
        }
		private DOWN_FINISH_CALLBACK m_cCallBack;   //回调方法
		private DOWN_ERROR_CALLBACK m_cErrorCallBack;	//error callback.
        private DecryptBytesFunc m_delDecryptFunc;  //解密接口
        private ENCRYPT_TYPE m_eEncryptType;    //加密类型
        private RESOURCE_TYPE m_eResType;   //资源类型
		private bool m_bAutoSave;	//auto save function.

        /// <summary>
        /// 开始WW加载
        /// </summary>
        /// <param name="path"></param>
        /// <param name="crc"></param>
        /// <param name="version"></param>
        /// <param name="callback"></param>
        /// <param name="res_type"></param>
        /// <param name="encrypt_type"></param>
        /// <param name="decryptFunc"></param> 
		public static LoadPackage StartWWW(
			string path ,
			bool autosave = false , long utime = 0 , DOWN_FINISH_CALLBACK callback = null,
			DOWN_ERROR_CALLBACK error_call = null,
			RESOURCE_TYPE res_type = RESOURCE_TYPE.WEB_ASSETBUNLDE,
			ENCRYPT_TYPE encrypt_type = ENCRYPT_TYPE.NORMAL,
			DecryptBytesFunc decryptFunc = null
			)
        {
            GameObject obj = new GameObject("WWWLoad");
            LoadPackage loader = obj.AddComponent<LoadPackage>();
            loader.Init(
				path , autosave ,utime , callback,error_call,
				res_type, encrypt_type, decryptFunc);
            loader.StartCoroutine("Load");
            return loader;
        }

		/// <summary>
		/// Restart this instance.
		/// </summary>
		public void Restart()
		{
			this.m_bComplete = true;
			this.m_cWww = null;
			this.m_fProgess = 0;
			StartCoroutine("Load");
		}

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="id"></param>
		private void Init(
			string path ,
			bool autosave = false, long utime = 0,
			DOWN_FINISH_CALLBACK callback = null,
			DOWN_ERROR_CALLBACK error_call = null,
			RESOURCE_TYPE res_type = RESOURCE_TYPE.WEB_ASSETBUNLDE,
			ENCRYPT_TYPE encrypt_type = ENCRYPT_TYPE.NORMAL,
			DecryptBytesFunc decryptFunc = null
			)
        {
            this.m_strPath = path;
			this.m_lUTime = utime;
            this.m_bComplete = false;
            this.m_cWww = null;
            this.m_cCallBack = callback;
			this.m_cErrorCallBack = error_call;
            this.m_delDecryptFunc = decryptFunc;
            this.m_eEncryptType = encrypt_type;
            this.m_eResType = res_type;
			this.m_fProgess = 0;
			this.m_bAutoSave = autosave;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Destory()
        {
            this.m_cWww = null;
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        private IEnumerator Load()
        {
            string path = "";
            path += this.m_strPath;
			this.m_cWww = new WWW(path);

			for( ;!this.m_cWww.isDone; )
			{
				this.m_fProgess = this.m_cWww.progress;
				yield return new WaitForSeconds(0);
			}

            if (this.m_cWww.error != null)
            {
                Debug.Log(m_cWww.error);
				if(this.m_cErrorCallBack != null )
				{
					this.m_cErrorCallBack(this.m_cWww.error,this);
				}
            }
            else
            {
                this.m_bComplete = true;
				this.m_fProgess = 1;

				if(this.m_cCallBack != null )
				{
					switch(this.m_eResType)
					{
					case RESOURCE_TYPE.WEB_ASSETBUNLDE:
						this.m_cCallBack(this.m_strPath , this.m_cWww.assetBundle);
						break;
					case RESOURCE_TYPE.WEB_TEXTURE:
						this.m_cCallBack(this.m_strPath , this.m_cWww.texture);
						break;
					case RESOURCE_TYPE.WEB_TEXT_BYTES:
						this.m_cCallBack(this.m_strPath , this.m_cWww.bytes);
						break;
					case RESOURCE_TYPE.WEB_TEXT_STR:
						this.m_cCallBack(this.m_strPath , this.m_cWww.text);
						break;
					}
				}
				if(this.m_bAutoSave)
				{
					Uri tmpUri = new Uri(this.m_strPath);

					string dataPath = Application.persistentDataPath + tmpUri.AbsolutePath;
					if(!tmpUri.IsFile)
					{
						CFile.WriteAllBytes(dataPath , this.m_cWww.bytes , this.m_lUTime);
					}

				}
            }

			this.m_cWww.Dispose();
			this.m_cWww = null;
        }

    }


}