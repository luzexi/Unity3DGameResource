using UnityEngine;
using System;
using System.IO;
using System.Collections;


namespace GameResource
{
	//file loader
	public class FileLoader
	{
		public delegate FINISH_CALLBACK(string path, byte[] obj);
        public delegate ERROR_CALLBACK(string path, string error);

        private byte[] m_Buffer { get; set; }	//buffer
        private int m_BufferSize { get; set; }	//data buffer size
        private FileStream m_InputStream;	//input stream
        private FINISH_CALLBACK m_FinishCallback;	//finish callback
        private ERROR_CALLBACK m_ErrorCallback;	//error callback
        private string m_Path;	//path

		/// <summary>
		/// Determines if is exist the specified Path.
		/// </summary>
		/// <returns><c>true</c> if is exist the specified Path; otherwise, <c>false</c>.</returns>
		/// <param name="Path">Path.</param>
		public static bool IsExist( string Path )
		{
			return File.Exists(Path);
		}

		/// <summary>
		/// Creates the directory.
		/// </summary>
		/// <param name="path">Path.</param>
		public static void CreateDirectory(string path)
		{
			if (!Directory.Exists(path) )
			{
				Directory.CreateDirectory(path);
			}
		}

		/// <summary>
		/// Reads all bytes.
		/// </summary>
		/// <returns>The all bytes.</returns>
		/// <param name="path">Path.</param>
		public static byte[] ReadAllBytes(string path)
		{
			try
			{
				//
				byte[] data = File.ReadAllBytes(path);
				return data;
			}
			catch( Exception e)
			{
				Debug.LogError(e.StackTrace);
			}
			return null;
		}

		/// <summary>
		/// Writes all bytes.
		/// </summary>
		/// <param name="Path">Path.</param>
		/// <param name="data">Data.</param>
		public static void WriteAllBytes( string path , byte[] data , long utime )
		{
			try
			{
				string dir = Path.GetDirectoryName(path);
				if (!Directory.Exists(dir))
				{
					CreateDirectory(dir);
				}
				if(File.Exists(path))
				{
					File.Delete(path);
				}
				File.WriteAllBytes(path , data);
				File.SetLastWriteTimeUtc(path , new DateTime(utime));
			}
			catch(Exception e)
			{
				Debug.LogError(e.StackTrace);
			}
		}

		/// <summary>
		/// Reads all text.
		/// </summary>
		/// <returns>The all text.</returns>
		/// <param name="path">Path.</param>
		public static string ReadAllText(string path)
		{
			try
			{
				string data = File.ReadAllText(path);
				return data;
			}
			catch(Exception e)
			{
				Debug.LogError(e.StackTrace);
			}
			return null;
		}

		/// <summary>
		/// Writes all text.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="data">Data.</param>
		public static void WriteAllText( string path , string data)
		{
			try
			{
				string dir = Path.GetDirectoryName(path);
				if (!Directory.Exists(dir)) {
					CreateDirectory(dir);
				}
				File.WriteAllText(path , data);
			}
			catch(Exception e)
			{
				Debug.LogError(e.StackTrace);
			}
		}

		//async read file
		public static CFile AsyncReadFile(string path , FINISH_CALLBACK finish_callback , ERROR_CALLBACK error_callback = null)
		{
			CFile cfile = new CFile();
			file.AsyncBeginReadFile(path , finish_callback , error_callback);
			return cfile;
		}

		//get async progress
		public float GetAsyncProgress()
		{
			return this.m_BufferSize*1f/this.m_Buffer.Length;
		}

		//async read file
        private void AsyncBeginReadFile(string path , FINISH_CALLBACK finish_callback , ERROR_CALLBACK error_callback )
        {
        	this.m_Path = path;
        	this.m_FinishCallback = finish_callback;
        	this.m_ErrorCallback = error_callback;

        	try
        	{
        		this.m_InputStream = new FileStream(path, FileMode.Open);
	            this.m_Buffer = new byte[this.m_InputStream.Length];
	            this.m_BufferSize = 0;
	            this.m_InputStream.BeginRead(this.m_Buffer, this.m_BufferSize, this.m_Buffer.Length, AsyncReadCallback, null);
        	}
        	catch(Exception e)
        	{
        		Debug.LogError("Error: " + e.StackTrace);
        		if(this.m_ErrorCallback != null)
        		{
        			this.m_ErrorCallback(this.m_Path , e.StackTrace);
        		}
        	}
        }
        
        //async read callback
        private void AsyncReadCallback(IAsyncResult ar)
        {
        	try
        	{
	            int bytesRead = this.m_InputStream.EndRead(ar);
	            if (bytesRead > 0)
	            {
	            	this.m_BufferSize += bytesRead;
	                Thread.Sleep(TimeSpan.FromMilliseconds(10));
	                this.m_InputStream.BeginRead(this.m_Buffer, this.m_BufferSize, this.m_Buffer.Length, AsyncReadCallback, null);
	            }
	            else
	            {
	            	if(this.m_FinishCallback != null)
	            	{
	            		this.m_FinishCallback(this.m_Path , this.m_Buffer);
	            	}
	                this.m_InputStream.Close();
	            }
        	}
        	catch(Exception e)
        	{
        		Debug.LogError("Error: " + e.StackTrace);
        		if(this.m_ErrorCallback != null)
        		{
        			this.m_ErrorCallback(this.m_Path , e.StackTrace);
        		}
        	}
        }
	}

}