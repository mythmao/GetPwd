using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordPolicy.Algorithm
{
    /// <summary>
    /// ʹ�ü��ַ���Base64���ܽ���
    /// </summary>
    public class Base64Algorithm : IAlgorithm
    {
        /// <summary>
        /// �����ڴ��е��ַ�����
        /// </summary>
        /// <param name="data">����������</param>
        /// <param name="key">��Կ</param>
        /// <param name="iv">��ʼ������</param>
        /// <returns></returns>
        public string EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            try
            {
                if (data == null || data == string.Empty)
                    return "";

                // ���ַ�������ת��Ϊ�ֽ�����
                byte[] toEncrypt = UTF8Encoding.UTF8.GetBytes(data);

                string tt = Convert.ToBase64String(toEncrypt);

                return tt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("������ܳ����ԭ��: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// �����ڴ��е�����
        /// </summary>
        /// <param name="data">����������</param>
        /// <param name="key">��Կ</param>
        /// <param name="iv">��ʼ������</param>
        /// <returns></returns>
        public string DecryptTextFromMemory(string data, byte[] key, byte[] iv)
        {
            try
            {
                if (data == null || data ==string.Empty)
                    return "";

                //�����ܴ�תΪ�ֽ�����
                byte[] toEncrypt = Convert.FromBase64String(data);

                string strDecrypt = UTF8Encoding.UTF8.GetString(toEncrypt);

                return strDecrypt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("������ܴ����ԭ��: {0}", e.Message);
                return null;
            }

        }

        /// <summary>
        /// �����ڴ��е��ַ����ݣ�ʹ��Ĭ��key��iv
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
        /// �����ڴ��е��ַ������ݣ�ʹ��Ĭ��key��iv
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