// -*- coding: utf-8; tab-width: 4 -*-
using UnityEngine;
using System;
using System.Collections;

namespace GameResource
{
	//zip manager
	public class ZipManager : SingletonMono<ZipManager>
	{
		protected override void Init()
		{
	    	StartCoroutine("ZipUpdate");
	  	}
		
		// Update is called once per frame
		IEnumerator ZipUpdate () {
			while(true) {
				ZipProxy.checkoutZipProxy();
				yield return new WaitForSeconds(0.33f);
			}
		}
		
		public ZipProxy uncompless(string zipFile, string extralPath, System.Action<object> endCallback, System.Action<Exception> errorCallback=null) {
			return ZipProxy.uncompless(zipFile, extralPath, endCallback, errorCallback);
		}
	}
}