using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordPolicy.Algorithm
{
    /// <summary>
    /// 使用简单字符串Base64加密解密
    /// </summary>
    public class Base64Algorithm : IAlgorithm
    {
        /// <summary>
        /// 加密内存中的字符数据
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns></returns>
        public string EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            try
            {
                if (data == null || data == string.Empty)
                    return "";

                // 将字符串变量转变为字节数组
                byte[] toEncrypt = UTF8Encoding.UTF8.GetBytes(data);

                string tt = Convert.ToBase64String(toEncrypt);

                return tt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("引起加密出错的原因: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 解密内存中的数据
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns></returns>
        public string DecryptTextFromMemory(string data, byte[] key, byte[] iv)
        {
            try
            {
                if (data == null || data ==string.Empty)
                    return "";

                //将加密串转为字节数组
                byte[] toEncrypt = Convert.FromBase64String(data);

                string strDecrypt = UTF8Encoding.UTF8.GetString(toEncrypt);

                return strDecrypt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("引起解密错误的原因: {0}", e.Message);
                return null;
            }

        }

        /// <summary>
        /// 加密内存中的字符数据，使用默认key及iv
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string EncryptTextToMemory(string data)
        {
            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
            return EncryptTextToMemory(data, key, iv);
        }

        /// <summary>
        /// 解密内存中的字符串数据，使用默认key及iv
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string DecryptTextFromMemory(string data)
        {
            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
            return DecryptTextFromMemory(data,key,iv);
        }
    }
}