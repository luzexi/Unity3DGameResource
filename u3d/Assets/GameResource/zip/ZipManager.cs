// -*- coding: utf-8; tab-width: 4 -*-
using UnityEngine;
using System;
using System.Collections;

namespace GameResource
{
	//zip manager
	public class ZipManager : SingletonMono<ZipManager>
	{
		private ZipProxy m_ZipProxy = null;

		public float Progress
		{
			get
			{
				if(this.m_ZipProxy != null)
				{
					return this.m_ZipProxy.m_decompressCount*1f/this.m_ZipProxy.m_totalCount;
				}
				return 0;
			}
		}

		protected override void Init()
		{
	    	StartCoroutine("ZipUpdate");
	  	}
		
		// Update is called once per frame
		IEnumerator ZipUpdate ()
		{
			while(true)
			{
				ZipProxy.checkoutZipProxy();
				yield return new WaitForSeconds(0.33f);
			}
		}
		
		public ZipProxy uncompless(string zipFile, string extralPath,
			System.Action<object> endCallback, System.Action<Exception> errorCallback=null)
		{
			this.m_ZipProxy = ZipProxy.uncompless(zipFile, extralPath, endCallback, errorCallback);
			return this.m_ZipProxy;
		}
	}
}