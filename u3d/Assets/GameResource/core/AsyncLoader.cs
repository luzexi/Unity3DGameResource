using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//  AsyncLoader.cs
//  Lu Zexi
//  2013-12-16



namespace GameResource
{
    // Async Load The AssetBundle
    public class AsyncLoader : MonoBehaviour
    {
        public delegate void FINISH_CALLBACK(string name, UnityEngine.Object obj);

        private static GameObject sGoInstance;  //singleton gameobject instance

        private float m_fProgress;  //progress
        private FINISH_CALLBACK m_delFinishCallback;    //finish callback
        private AssetBundleRequest m_cRequest;  //The load request

        public float Progress
        {
            get
            {
                return this.m_fProgress;
            }
        }

        //Start async load the assetbundle
        public static AsyncLoader StartLoad(AssetBundle asset, string resName , FINISH_CALLBACK finish_callback)
        {
            if(sGoInstance == null)
            {
                sGoInstance = new GameObject("AsyncLoader");
            }
            AsyncLoader loader = sGoInstance.AddComponent<AsyncLoader>();
            loader.StartCoroutine(loader.GoLoader(asset, resName,finish_callback));
            return loader;
        }
        
        //Begin to load
        private IEnumerator GoLoader(AssetBundle asset, string resName , FINISH_CALLBACK finish_callback)
        {
            this.m_delFinishCallback = finish_callback;
            this.m_fProgress = 0;
            this.m_cRequest = asset.LoadAsync(resName, typeof(UnityEngine.Object));

            for (; !this.m_cRequest.isDone; )
            {
                this.m_fProgress = this.m_cRequest.progress;
                yield return new WaitForEndOfFrame();
            }
            this.m_fProgress = 1;

            if(this.m_delFinishCallback != null)
            {
                this.m_delFinishCallback(resName , this.m_cRequest.asset);
            }

            GameObject.Destroy(this.gameObject);
        }
    }

}
