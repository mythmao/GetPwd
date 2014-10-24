using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace PasswordPolicy.Util
{
    /// <summary>
    /// DEC 64位加密
    /// </summary>
    public class EncryptionUtility
    {
        //加密密钥 8位
        private static readonly string _SecretKey = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CookieMemIdSecretKey"]) ?
                                                        "myehrkey" : (ConfigurationManager.AppSettings["CookieMemIdSecretKey"].Trim().Length != 8 ?
                                                                        "myehrkey" : ConfigurationManager.AppSettings["CookieMemIdSecretKey"]);

        #region 加密
        /// <summary>
        /// 加密 默认密钥:myehrkey
        ///     密钥为Web.config中配置的。[CookieMemIdSecretKey]
        /// </summary>
        /// <param name="memID"></param>
        /// <returns></returns>
        public static string EncryptMemID(string memID)
        {
            if (string.IsNullOrEmpty(memID))
            {
                return "";
            }
            return EncryptString(memID, _SecretKey);
        }
        #endregion

        #region 解密
        /// <summary>
        /// 解密 默认密钥:myehrkey
        ///     密钥为Web.config中配置的。[CookieMemIdSecretKey]
        /// </summary>
        /// <param name="memID"></param>
        /// <returns></returns>
        public static string DecryptMemID(string memID)
        {
            if (string.IsNullOrEmpty(memID))
            {
                return "";
            }
            return DecryptString(memID, _SecretKey);
        }
        #endregion

        #region DEC 加密过程 DEC 解密过程
        /// <summary>
        /// DEC 加密过程  64位加密
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey">8字符</param>
        /// <returns></returns>
        public static string EncryptString(string pToEncrypt, string sKey)
        {
            #region
            DESCryptoServiceProvider des = new DESCryptoServiceProvider(); //64 把字符串放到byte数组中 

            byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
            //byte[] inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);
            //Rijndael RijndaelAlg = Rijndael.Create();
            //RijndaelAlg.KeySize = 128;//128 192 256
            byte[] decKeys = GetDECKeys(sKey);
            des.Key = decKeys; //建立加密对象的密钥和偏移量 
            des.IV = decKeys; //原文使用Encoding.UTF8方法的GetBytes方法 
            MemoryStream ms = new MemoryStream(); //使得输入密码必须输入英文文本 
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
            #endregion
        }

        /// <summary>
        /// DEC 解密过程  64位加密
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey">8字符</param>
        /// <returns></returns>
        public static string DecryptString(string pToDecrypt, string sKey)
        {
            #region
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            byte[] decKeys = GetDECKeys(sKey);
            des.Key = decKeys; //建立加密对象的密钥和偏移量 
            des.IV = decKeys; //原文使用Encoding.UTF8方法的GetBytes方法 

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder(); //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象 

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
            #endregion
        }

        /// <summary>
        /// 获取dec的加密密钥 8位 不够的用0补充到8位 超过的只取8位
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static byte[] GetDECKeys(string key)
        {
            #region
            byte[] decKeyArray = new byte[8];

            byte[] temp = Encoding.UTF8.GetBytes(key);

            int i;
            if (temp.Length < 8)
            {
                Array.Copy(temp, 0, decKeyArray, 0, temp.Length);
                for (i = temp.Length; i < 8; i++)
                {
                    decKeyArray[i] = 0;
                }
            }
            else
            {
                Array.Copy(temp, 0, decKeyArray, 0, 8);
            }

            return decKeyArray;
            #endregion
        }

        #endregion
    }
}