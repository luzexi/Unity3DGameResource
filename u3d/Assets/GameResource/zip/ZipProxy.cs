// -*- coding: utf-8; tab-width: 4 -*-
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;



namespace GameResource
{
	//zip proxy
	public class ZipProxy
	{
		private string m_zipPath;
		private string m_extralPath;
		private bool m_isDone;
		private bool m_isError;
		public int m_totalCount;
		public int m_decompressCount;
		private System.Action<object> m_endCallback;
		private System.Action<Exception> m_errorCallback;
		private Exception m_exception;
		
		private static List<ZipProxy> s_execProxy = new List<ZipProxy>();
		
		public bool IsDone()
		{
			return m_isDone;
		}

		public bool IsError()
		{
			return m_isError;
		}
		
		public static ZipProxy uncompless(string zipFile, string extralPath,
			System.Action<object> endCallback, System.Action<Exception> errorCallback)
		{
			ZipProxy proxy = new ZipProxy();
			proxy.m_zipPath = zipFile;
			proxy.m_extralPath = extralPath;
			proxy.m_isDone = false;
			proxy.m_isError = false;
			proxy.m_endCallback = endCallback;
			proxy.m_errorCallback = errorCallback;
			proxy.m_exception = null;
			proxy.m_totalCount = 0;
			proxy.m_decompressCount = 0;
			ThreadPool.QueueUserWorkItem(new WaitCallback(
				delegate(object t)
				{
					Exception exception = null;
					ZipProxy arg = (ZipProxy)t;
					byte[] buffer = new byte[1048576];

					//the count of files in zip
					int totalCount = 0;
					using(FileStream fs = new FileStream(arg.m_zipPath, FileMode.Open, FileAccess.Read))
					{
						using(ZipInputStream zis = new ZipInputStream(fs))
						{
							while(zis.GetNextEntry() != null)
							{
								totalCount++;
							}
						}
					}

					//begin to unzip
					arg.m_totalCount = totalCount;
					arg.m_decompressCount = 0;
					using(FileStream fs = new FileStream(arg.m_zipPath, FileMode.Open, FileAccess.Read))
					{
						using(ZipInputStream zis = new ZipInputStream(fs))
						{
							ZipEntry ze;
							while((ze = zis.GetNextEntry()) != null)
							{
								try
								{
									if (!ze.IsDirectory)	// create file and write content
									{
										string fileName = Path.GetFileName(ze.Name);
										string destDir = Path.Combine(arg.m_extralPath, Path.GetDirectoryName(ze.Name));
										Directory.CreateDirectory(destDir);
										string destPath = Path.Combine(destDir, fileName);
										
										using(FileStream writer = new FileStream(destPath, FileMode.Create, FileAccess.Write))
										{
											int len;
											while ((len = zis.Read(buffer, 0, buffer.Length)) > 0)
											{
												writer.Write(buffer, 0, len);
											}
											writer.Close();
										}
									}
									else	// create folder
									{
										string dirPath = Path.Combine(arg.m_extralPath, Path.GetDirectoryName(ze.Name));
										Directory.CreateDirectory(dirPath);
									}
								}
								catch (Exception e)
								{
									exception = e;
									break;
								}

								//recode decompless count
								arg.m_decompressCount++;
							}
						}
					}
				
					//complete unzip delete the zip file
					// File.Delete(arg.m_zipPath);

					lock (((ICollection)s_execProxy).SyncRoot)
					{
						if(exception != null)
						{
							arg.m_isError = true;
							arg.m_exception = exception;
						}
						arg.m_isDone = true;
					}
				}
			), proxy);
			
			lock (((ICollection)s_execProxy).SyncRoot)
			{
				s_execProxy.Add(proxy);
			}
			return proxy;
		}
		
		public static void checkoutZipProxy()
		{
			lock (((ICollection)s_execProxy).SyncRoot)
			{
				List<ZipProxy> remove_list = new List<ZipProxy>();
				foreach(ZipProxy proxy in s_execProxy)
				{
					if(proxy.m_isDone)
					{
						if(proxy.m_isError)
						{
							if(proxy.m_errorCallback != null)
							{
								proxy.m_errorCallback(proxy.m_exception);
							}
						}
						else
						{
							if(proxy.m_endCallback != null)
							{
								proxy.m_endCallback(null);
							}
						}
						remove_list.Add(proxy);
					}
				}
				
				foreach(ZipProxy removeObj in remove_list)
				{
					s_execProxy.Remove(removeObj);
				}
				remove_list = null;
			}
		}
	}

}
