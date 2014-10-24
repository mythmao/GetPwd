using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordPolicy.Algorithm
{
    /// <summary>
    /// ʹ��MD5�㷨�ӽ���
    /// </summary>
    public class MD5Algorithm : IAlgorithm
    {
        /// <summary>
        /// �����ڴ��е��ַ�����
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
                Console.WriteLine("������ܳ����ԭ��: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// �����ڴ��е��ַ�������
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
                Console.WriteLine("������ܴ����ԭ��: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// �õ��򵥵�һ�����Ҵ�
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        private string GetAddPass(string pass)
        {
            #region �õ��򵥵�һ�����Ҵ�

            string str = string.Empty;

            str = string.Format("Chin{0}ahr",pass);

            return str;

            #endregion
        }
    }
}