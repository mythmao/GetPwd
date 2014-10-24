using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace PasswordPolicy.Util
{
    /// <summary>
    /// DEC 64λ����
    /// </summary>
    public class EncryptionUtility
    {
        //������Կ 8λ
        private static readonly string _SecretKey = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CookieMemIdSecretKey"]) ?
                                                        "myehrkey" : (ConfigurationManager.AppSettings["CookieMemIdSecretKey"].Trim().Length != 8 ?
                                                                        "myehrkey" : ConfigurationManager.AppSettings["CookieMemIdSecretKey"]);

        #region ����
        /// <summary>
        /// ���� Ĭ����Կ:myehrkey
        ///     ��ԿΪWeb.config�����õġ�[CookieMemIdSecretKey]
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

        #region ����
        /// <summary>
        /// ���� Ĭ����Կ:myehrkey
        ///     ��ԿΪWeb.config�����õġ�[CookieMemIdSecretKey]
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

        #region DEC ���ܹ��� DEC ���ܹ���
        /// <summary>
        /// DEC ���ܹ���  64λ����
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey">8�ַ�</param>
        /// <returns></returns>
        public static string EncryptString(string pToEncrypt, string sKey)
        {
            #region
            DESCryptoServiceProvider des = new DESCryptoServiceProvider(); //64 ���ַ����ŵ�byte������ 

            byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
            //byte[] inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);
            //Rijndael RijndaelAlg = Rijndael.Create();
            //RijndaelAlg.KeySize = 128;//128 192 256
            byte[] decKeys = GetDECKeys(sKey);
            des.Key = decKeys; //�������ܶ������Կ��ƫ���� 
            des.IV = decKeys; //ԭ��ʹ��Encoding.UTF8������GetBytes���� 
            MemoryStream ms = new MemoryStream(); //ʹ�����������������Ӣ���ı� 
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
        /// DEC ���ܹ���  64λ����
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey">8�ַ�</param>
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
            des.Key = decKeys; //�������ܶ������Կ��ƫ���� 
            des.IV = decKeys; //ԭ��ʹ��Encoding.UTF8������GetBytes���� 

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder(); //����StringBuild����CreateDecryptʹ�õ��������󣬱���ѽ��ܺ���ı���������� 

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
            #endregion
        }

        /// <summary>
        /// ��ȡdec�ļ�����Կ 8λ ��������0���䵽8λ ������ֻȡ8λ
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