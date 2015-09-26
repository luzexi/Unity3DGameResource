using System;
using System.Collections;
using UnityEngine;


//  AssetBundleLoader.cs
//  Lu Zexi
//  2012-7-5


namespace GameResource
{
    //assetbundle loader
    public class AssetBundleLoader : MonoBehaviour
    {
        private static GameObject sGoInstance = null;  //gameobject

        public delegate void FINISH_CALLBACK(string path, AssetBundle ob);
        public delegate void ERROR_CALLBACK(string path, string error);

        private string m_strPath;   //加载路径
        private bool m_bComplete;   //是否加载完成
        public bool Complete
        {
            get { return this.m_bComplete; }
        }

        private AssetBundle m_cAssetbundle;  //assetbundle data
        private WWW m_cWww;     //WWW
        private AssetBundleCreateRequest m_cABRequest;  //assetbundle create request
        private byte[] m_cData; //assetbundle data
        private float m_fProgess;   //the progess of the www.
        public float Progess
        {
            get
            {
                return this.m_fProgess;
            }
        }
        private FINISH_CALLBACK m_cFinishCallBack;   //finish callback
        private ERROR_CALLBACK m_cErrorCallBack;    //error callback.


        //create form file immediate
        public static AssetBundle CreateFromFile(string path , int offset = 0)
        {
            return AssetBundle.CreateFromFile(path , offset);
        }

        //create from memory immediate
        public static AssetBundle CreateFromMemoryImmediate(byte[] binary)
        {
            return AssetBundle.CreateFromMemoryImmediate(binary);
        }

        //load async by binary
        public static AssetBundleLoader LoadBinary(byte[] binary,FINISH_CALLBACK finish_callback = null ,ERROR_CALLBACK error_callback = null)
        {
            if(sGoInstance == null)
            {
                sGoInstance = new GameObject("AssetBundleLoader");
            }
            
            AssetBundleLoader loader = sGoInstance.AddComponent<AssetBundleLoader>();
            loader.Init(binary , finish_callback , error_callback);
            loader.StartCoroutine(loader.StartBinary());
            return loader;
        }

        //load async by www
        public static AssetBundleLoader LoadWww(
            string path ,
            FINISH_CALLBACK finish_callback = null,
            ERROR_CALLBACK error_callback = null
            )
        {
            if(sGoInstance == null)
            {
                sGoInstance = new GameObject("AssetBundleLoader");
            }
            
            AssetBundleLoader loader = sGoInstance.AddComponent<AssetBundleLoader>();
            loader.Init(path , finish_callback , error_callback);
            loader.StartCoroutine(loader.StartWWW());
            return loader;
        }

        //init binary
        private void Init(
            byte[] binary ,
            FINISH_CALLBACK finish_callback = null ,
            ERROR_CALLBACK error_callback = null
            )
        {
            this.m_cData = binary;
            Init(finish_callback,error_callback);
        }

        //init path
        private void Init(
            string path ,
            FINISH_CALLBACK finish_callback = null,
            ERROR_CALLBACK error_callback = null
            )
        {
            this.m_strPath = path;
            Init(finish_callback,error_callback);
        }

        //init
        private void Init(
            FINISH_CALLBACK finish_callback = null,
            ERROR_CALLBACK error_callback = null
            )
        {
            this.m_bComplete = false;
            this.m_cWww = null;
            this.m_cABRequest = null;
            this.m_cFinishCallBack = finish_callback;
            this.m_cErrorCallBack = error_callback;
            this.m_fProgess = 0;
        }

        //start binary load
        private IEnumerator StartBinary()
        {
            this.m_cABRequest = AssetBundle.CreateFromMemory(this.m_cData);
            for(;!this.m_cABRequest.isDone;)
            {
                this.m_fProgess = this.m_cABRequest.progress;
                yield return new WaitForEndOfFrame();
            }
            if(this.m_cFinishCallBack != null)
            {
                this.m_cFinishCallBack("",this.m_cABRequest.assetBundle);
            }
            GameObject.Destroy(this);
        }

        //start www load
        private IEnumerator StartWWW()
        {
            string path = "";
            path += this.m_strPath;
            this.m_cAssetbundle = null;
            this.m_cWww = new WWW(path);

            for( ;!this.m_cWww.isDone; )
            {
                this.m_fProgess = this.m_cWww.progress;
                yield return new WaitForEndOfFrame();
            }

            if (this.m_cWww.error != null)
            {
                Debug.LogError(m_cWww.error);
                if(this.m_cErrorCallBack != null )
                {
                    this.m_cErrorCallBack(this.m_strPath , this.m_cWww.error);
                }
            }
            else
            {
                this.m_bComplete = true;
                this.m_fProgess = 1;
                this.m_cAssetbundle = this.m_cWww.assetBundle;
                if(this.m_cFinishCallBack != null)
                {
                    this.m_cFinishCallBack(this.m_strPath , this.m_cWww.assetBundle);
                }
            }

            this.m_cWww.Dispose();
            this.m_cWww = null;
            GameObject.Destroy(this);
        }

    }


}