using UnityEngine;
using System;
using System.IO;
using System.Collections;


namespace Game.Resource
{
	/// <summary>
	/// flie tools.
	/// </summary>
	public class CFile
	{
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
		public static void WriteAllBytes( string path , byte[] data )
		{
			try
			{
				string dir = Path.GetDirectoryName(path);
				if (!Directory.Exists(dir)) {
					CreateDirectory(dir);
				}
				File.WriteAllBytes(path , data);
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
	}

}