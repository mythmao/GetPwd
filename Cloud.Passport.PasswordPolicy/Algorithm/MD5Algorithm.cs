using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordPolicy.Algorithm
{
    /// <summary>
    /// 使用MD5算法加解密
    /// </summary>
    public class MD5Algorithm : IAlgorithm
    {
        /// <summary>
        /// 加密内存中的字符数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string EncryptTextToMemory(string data)
        {
            try
            {
                if (data == null || data == string.Empty)
                    return "";

                string str = GetAddPass(data);

                byte[] b = System.Text.Encoding.UTF8.GetBytes(str);
                b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
                string ret = "";
                for (int i = 0; i < b.Length; i++)
                {
                    ret += b[i].ToString("x").PadLeft(2, '0');
                }
                return ret;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("引起加密出错的原因: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 解密内存中的字符串数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string DecryptTextFromMemory(string data)
        {
            try
            {
                if (data == null || data == string.Empty)
                    return "";
                return "";
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("引起解密错误的原因: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 得到简单的一个打乱串
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        private string GetAddPass(string pass)
        {
            #region 得到简单的一个打乱串

            string str = string.Empty;

            str = string.Format("Chin{0}ahr",pass);

            return str;

            #endregion
        }
    }
}