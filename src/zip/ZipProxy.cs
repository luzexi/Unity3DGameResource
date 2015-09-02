// -*- coding: utf-8; tab-width: 4 -*-
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class ZipProxy
{
	private string m_zipPath;
	private string m_extralPath;
	private bool m_isDone;
	private bool m_isError;
	public int m_totalCount;
	public int m_decomplessCount;
	private System.Action<object> m_endCallback;
	private System.Action<Exception> m_errorCallback;
	private Exception m_exception;
	
	private static List<ZipProxy> s_execProxy = new List<ZipProxy>();
	
	public bool IsDone() {
		return m_isDone;
	}
	public bool IsError() {
		return m_isError;
	}
	
	public static ZipProxy uncompless(string zipFile, string extralPath, System.Action<object> endCallback, System.Action<Exception> errorCallback)
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
		proxy.m_decomplessCount = 0;
		ThreadPool.QueueUserWorkItem(new WaitCallback(
			delegate(object t) {
				Exception exception = null;
				ZipProxy arg = (ZipProxy)t;
				// Zip

				byte[] buffer = new byte[1048576];
				int totalCount = 0;
				using(FileStream fs = new FileStream(arg.m_zipPath, FileMode.Open, FileAccess.Read)) {
					using(ZipInputStream zis = new ZipInputStream(fs)) {
						while(zis.GetNextEntry() != null) {
							totalCount++;
						}
					}
				}

				arg.m_totalCount = totalCount;
				arg.m_decomplessCount = 0;
				using(FileStream fs = new FileStream(arg.m_zipPath, FileMode.Open, FileAccess.Read)) {
					using(ZipInputStream zis = new ZipInputStream(fs)) {
						ZipEntry ze;
						while((ze = zis.GetNextEntry()) != null) {
							try {
								if (!ze.IsDirectory) {
									//展開先のファイル名を決定
									string fileName = Path.GetFileName(ze.Name);
								    //展開先のフォルダを決定
									string destDir = Path.Combine(arg.m_extralPath, Path.GetDirectoryName(ze.Name));
									Directory.CreateDirectory(destDir);
									//展開先のファイルのフルパスを決定
									string destPath = Path.Combine(destDir, fileName);
									
									//書き込み先のファイルを開く
									using(FileStream writer = new FileStream(destPath, FileMode.Create, FileAccess.Write)) {
										//展開するファイルを読み込む
										int len;
										while ((len = zis.Read(buffer, 0, buffer.Length)) > 0)
										{
											//ファイルに書き込む
											writer.Write(buffer, 0, len);
										}
										//閉じる
										writer.Close();
									}
								}
								else {
									//フォルダのとき
									//作成するフォルダのフルパスを決定
									string dirPath = Path.Combine(arg.m_extralPath, Path.GetDirectoryName(ze.Name));
									//フォルダを作成
									Directory.CreateDirectory(dirPath);
								}
							}
							catch (Exception e) {
								exception = e;
								break;
							}

							arg.m_decomplessCount++;
						}
					}
				}
			
				File.Delete(arg.m_zipPath);
				lock (((ICollection)s_execProxy).SyncRoot) {
					if(exception != null) {
						arg.m_isError = true;
						arg.m_exception = exception;
					}
					arg.m_isDone = true;
				}
			}), proxy);
		
		lock (((ICollection)s_execProxy).SyncRoot) {
			s_execProxy.Add(proxy);
		}
		return proxy;
	}
	
	//
	public static void checkoutZipProxy()
	{
		lock (((ICollection)s_execProxy).SyncRoot) {
			List<ZipProxy> remove_list = new List<ZipProxy>();
			foreach(ZipProxy proxy in s_execProxy)
			{
	
				if(proxy.m_isDone) {
					if(proxy.m_isError) {
						if(proxy.m_errorCallback != null) {
							proxy.m_errorCallback(proxy.m_exception);
						}
					}
					else {
						if(proxy.m_endCallback != null) {
							proxy.m_endCallback(null);
						}
					}
					remove_list.Add(proxy);
				}
			}
			
			foreach(ZipProxy removeObj in remove_list) {
				s_execProxy.Remove(removeObj);
			}
			remove_list = null;
		}
	}
}
