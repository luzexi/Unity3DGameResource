using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//	SingletonMono.cs
//	Author: Lu Zexi
//	2014-10-19


namespace GameResource
{
	//singleton mono
	public abstract class SingletonMono<T> : MonoBehaviour
		where T : MonoBehaviour
	{
		private static T s_cInstance;
		public static T I
		{
			get
			{
				if(s_cInstance==null)
				{
					GameObject go = new GameObject(typeof(T).Name);
					s_cInstance = go.AddComponent<T>();
				}
				return s_cInstance;
			}
		}
		
		void Awake()
		{
			Init();
		}
		
		void OnDestroy()
		{
			onDestroy();
		}
		
		//destory
		protected virtual void onDestroy()
		{
			s_cInstance = null;
		}
		
		//init
		protected virtual void Init(){}
	}
}