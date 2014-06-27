

using System.Collections;
using UnityEngine;

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
        private uint m_iCRC; //CRC码
        private bool m_bComplete;   //是否加载完成
        public bool Complete
        {
            get { return this.m_bComplete; }
        }

        private WWW m_cWww;     //WWW
        public float Progess
        {
            get
            {
                if (this.m_cWww != null)
                    return this.m_cWww.progress;
                return 1f;
            }
        }
        public delegate void Func(string str, object asset);
        private Func m_cCallBack;   //回调方法
        private DecryptBytesFunc m_delDecryptFunc;  //解密接口
        private ENCRYPT_TYPE m_eEncryptType;    //加密类型
        private RESOURCE_TYPE m_eResType;   //资源类型
        private int m_iVersion; //版本

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
        public static LoadPackage StartWWW(string path, uint crc, int version, Func callback, RESOURCE_TYPE res_type, ENCRYPT_TYPE encrypt_type, DecryptBytesFunc decryptFunc)
        {
            GameObject obj = new GameObject("WWWLoad");
            LoadPackage loader = obj.AddComponent<LoadPackage>();
            loader.Init(path, crc, version, callback, res_type, encrypt_type, decryptFunc);
            loader.StartCoroutine("Load");
            return loader;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="id"></param>
        public void Init(string path, uint crc, int version, Func callback, RESOURCE_TYPE res_type, ENCRYPT_TYPE encrypt_type, DecryptBytesFunc decryptFunc)
        {
            this.m_strPath = path;
            this.m_iCRC = crc;
            this.m_bComplete = false;
            this.m_cWww = null;
            this.m_cCallBack = callback;
            this.m_delDecryptFunc = decryptFunc;
            this.m_eEncryptType = encrypt_type;
            this.m_eResType = res_type;
            this.m_iVersion = version;
        }

        ///// <summary>
        ///// 开始加载
        ///// </summary>
        //public void BeginLoad()
        //{
        //    StartCoroutine(Load());
        //}

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
        public IEnumerator Load()
        {
            StartLoad:

            string path = "";
            path += this.m_strPath;
            //Debug.Log("version " + this.m_iVersion);
            //Debug.Log("path " + this.m_strPath);
            if (this.m_iVersion >= 0)
                this.m_cWww = WWW.LoadFromCacheOrDownload(path, this.m_iVersion, this.m_iCRC);
            else
            {
                this.m_cWww = new WWW(path);
            }

            yield return this.m_cWww;

            //Debug.Log("www " + this.m_cWww.isDone  + " path " + this.m_strPath);

            if (this.m_cWww.error != null && this.m_iVersion >= 0 && !this.m_cWww.error.Contains("404") )
            {
                Debug.Log(m_cWww.error.ToString());
                goto StartLoad;
            }
            else
            {
                this.m_bComplete = true;

                if (this.m_eResType == RESOURCE_TYPE.WEB_TEXT_STR)
                {
                    if (this.m_cCallBack != null)
                    {
                        if (this.m_cWww.error == null)
                            this.m_cCallBack(this.m_strPath, this.m_cWww.text);
                        else
                            this.m_cCallBack(this.m_strPath, null);
                    }
                }
                else
                {
                    if (this.m_cCallBack != null )
                    {
                        if (this.m_cWww.error == null)
                            this.m_cCallBack(this.m_strPath, this.m_cWww.assetBundle);
                        else
                            this.m_cCallBack(this.m_strPath, null);
                    }

                    //string fileName = GetFileName(path);
                    //TextAsset ta = (TextAsset)this.m_cWww.assetBundle.Load(fileName);
                    //if (ta != null)
                    //{
                    //    byte[] datas = ta.bytes;

                    //    //解密
                    //    if( this.m_eEncryptType == ENCRYPT_TYPE.ENCRYPT)
                    //        datas = this.m_delDecryptFunc(datas);

                    //    AssetBundleCreateRequest abcr = AssetBundle.CreateFromMemory(datas);

                    //    yield return abcr;

                    //    if (this.m_cCallBack != null && abcr.isDone)
                    //    {
                    //        this.m_cCallBack(this.m_strPath, abcr.assetBundle);
                    //    }
                    //}
                }
            }
        }

        /// <summary>
        /// 从路径中获取文件名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string GetFileName(string path)
        {
            string[] strs = path.Split('.');
            string file = strs[strs.Length - 2];
            int i;
            for (i = file.Length - 1; i >= 0; i--)
            {
                if (file[i] == '/' || file[i] == '\\')
                {
                    break;
                }
            }
            if (i >= 0)
            {
                return file.Substring(i + 1, file.Length - (i + 1));
            }
            return null;
        }

    }


}