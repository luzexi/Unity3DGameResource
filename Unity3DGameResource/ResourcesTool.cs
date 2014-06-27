
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



//  ResourcesTool.cs
//  Lu Zexi
//  2012-9-18


namespace Game.Resource
{
    /// <summary>
    /// 加解密
    /// </summary>
    internal class CEncrypt
    {
        public readonly static int[] GAMESERVER_TO_CLIENT_KEY = { -73, -2, -50, -15, -58, -9, -74, -53, -74, -44, -65, -51, -69, -89, -74, -53, -75, -60, -73, -30, -80, -4, -61, -36, -44, -65 };
        public readonly static int[] CLIENT_TO_GAMESERVER_KEY = { -73, -2, -50, -15, -58, -9, -74, -53, -74, -44, -65, -51, -69, -89, -74, -53, -75, -60, -73, -30, -80, -4, -61, -36, -44, -65 };

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="buff">要加密的数据流</param>
        /// <param name="offset">数据流偏移</param>
        /// <param name="Key">加密的KEY</param>
        /// <param name="keyoffset">KEY的偏移</param>
        /// <param name="len">总长度</param>
        public static void Encoding(ref byte[] buff, int offset, int[] Key, int keyoffset, int len)
        {
            try
            {
                int keylen = Key.Length;
                for (int i = 0; i < len; ++i)
                {
                    buff[i + offset] ^= (byte)(Key[(i + keyoffset) % keylen]);
                }
            }
            catch (Exception e)
            {
                // Error
            }


        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="buff"></param>
        public static byte[] EncryptBytes(byte[] buff)
        {
            Encoding(ref buff, 0, GAMESERVER_TO_CLIENT_KEY, 0, buff.Length);
            return buff;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="buff">要解密的数据流</param>
        /// <param name="offset">数据流偏移</param>
        /// <param name="Key">加密KEY</param>
        /// <param name="keyoffset">KEY的偏移</param>
        /// <param name="len">总长度</param>
        public static void Decoding(ref byte[] buff, int offset, int[] Key, int keyoffset, int len)
        {
            try
            {
                int keylen = Key.Length;
                for (int i = 0; i < len; ++i)
                {
                    buff[i + offset] ^= (byte)(Key[(i + keyoffset) % keylen]);
                }
            }
            catch (Exception e)
            {
                // Error
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="buff"></param>
        public static byte[] DecryptBytes(byte[] buff)
        {
            Decoding(ref buff, 0, CLIENT_TO_GAMESERVER_KEY, 0, buff.Length);
            return buff;
        }
    }

}
