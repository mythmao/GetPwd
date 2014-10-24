using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordPolicy.Algorithm
{
    /// <summary>
    /// ʹ��DES�Գ��㷨�ӽ���
    /// </summary>
    public class DESAlgorithm : IAlgorithm
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

                // �����ڴ���
                MemoryStream mStream = new MemoryStream();

                // ����DES����
                DES dESalg = DES.Create();

                // Ϊ�ڴ��������������ͼ�����Կ����ʼ������
                CryptoStream cStream = new CryptoStream(mStream, dESalg.CreateEncryptor(key, iv), CryptoStreamMode.Write);

                // ���ַ�������ת��Ϊ�ֽ�����
                byte[] toEncrypt = UTF8Encoding.UTF8.GetBytes(data);
                //byte[] toEncrypt = unicodeEncoding.GetBytes(data);

                // ���ֽ�����д��������У�ִ�м���
                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                cStream.FlushFinalBlock();

                // �����ܺ������תΪ�ֽ�����
                byte[] ret = mStream.ToArray();

                // �ر����е���
                cStream.Close();
                mStream.Close();

                // ���ؼ��ܺ�����

                string tt = Convert.ToBase64String(ret);
                return tt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("������ܳ����ԭ��: {0}", e.Message);
                throw e;
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
                // ͨ���ֽ����鴴���ڴ���
                MemoryStream msDecrypt = new MemoryStream(toEncrypt);

                // ����DES����
                DES dESalg = DES.Create();

                // �����ӽ�����

                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    dESalg.CreateDecryptor(key, iv),
                    CryptoStreamMode.Read);

                StreamReader sr = new StreamReader(csDecrypt);
                return sr.ReadToEnd();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("������ܴ����ԭ��: {0}", e.Message);
                throw e;
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

            // ����DES����
            DES dESalg = DES.Create();
            dESalg.Key = key;
            dESalg.IV = iv;
            return EncryptTextToMemory(data, dESalg.Key, dESalg.IV);
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

            // ����DES����
            DES dESalg = DES.Create();
            dESalg.Key = key;
            dESalg.IV = iv;
            return DecryptTextFromMemory(data, dESalg.Key, dESalg.IV);
        }

        /// <summary>
        /// �����ڴ��еĶ��������飬ʹ��Ĭ��key��iv
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string EncryptBytesToMemory(byte[] data)
        {
            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            // ����DES����
            DES dESalg = DES.Create();
            dESalg.Key = key;
            dESalg.IV = iv;
            return EncryptBytesToMemory(data, dESalg.Key, dESalg.IV);
        }

        /// <summary>
        /// �����ڴ��еĶ��������飬ʹ��Ĭ��key��iv
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] DecryptBytesFromMemory(string data)
        {
            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            // ����DES����
            DES dESalg = DES.Create();
            dESalg.Key = key;
            dESalg.IV = iv;
            return DecryptBytesFromMemory(data, dESalg.Key, dESalg.IV);
        }

        /// <summary>
        /// �����ڴ��еĶ���������
        /// </summary>
        /// <param name="data">����������</param>
        /// <param name="key">��Կ</param>
        /// <param name="iv">��ʼ������</param>
        /// <returns></returns>
        public string EncryptBytesToMemory(byte[] data, byte[] key, byte[] iv)
        {
            try
            {
                if (data == null)
                    return "";

                // �����ڴ���
                MemoryStream mStream = new MemoryStream();

                //����DES����
                DES dESalg = DES.Create();

                // Ϊ�ڴ��������������ͼ�����Կ����ʼ������
                CryptoStream cStream = new CryptoStream(mStream, dESalg.CreateEncryptor(key, iv), CryptoStreamMode.Write);

                // ���ַ�������ת��Ϊ�ֽ�����
                byte[] toEncrypt = data;
                //byte[] toEncrypt = unicodeEncoding.GetBytes(data);

                // ���ֽ�����д��������У�ִ�м���
                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                cStream.FlushFinalBlock();

                // �����ܺ������תΪ�ֽ�����
                byte[] ret = mStream.ToArray();

                // �ر����е���
                cStream.Close();
                mStream.Close();

                // ���ؼ��ܺ�����

                string tt = Convert.ToBase64String(ret);
                return tt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("������ܳ����ԭ��: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// �����ڴ��еĶ���������
        /// </summary>
        /// <param name="data">����������</param>
        /// <param name="key">��Կ</param>
        /// <param name="iv">��ʼ������</param>
        /// <returns></returns>
        public byte[] DecryptBytesFromMemory(string data, byte[] key, byte[] iv)
        {
            try
            {
                if (data == null || data == string.Empty)
                    return null;
                //�����ܴ�תΪ�ֽ�����
                byte[] toEncrypt = Convert.FromBase64String(data);
                // ͨ���ֽ����鴴���ڴ���
                MemoryStream msDecrypt = new MemoryStream(toEncrypt);
                // ����DES����
                DES dESalg = DES.Create();

                // �����ӽ�����

                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    dESalg.CreateDecryptor(key, iv),
                    CryptoStreamMode.Read);

                //StreamReader sr = new StreamReader(csDecrypt);
                // �����ܺ������תΪ�ֽ�����
                byte[] rebytes = new byte[msDecrypt.Length];

                csDecrypt.Read(rebytes, 0, rebytes.Length);
                //
                //string resr = sr.ReadToEnd();

                //if (resr == null || resr == string.Empty)
                //    return null;

                //byte[] rebytes = Convert.FromBase64String(resr);

                return rebytes;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("������ܴ����ԭ��: {0}", e.Message);
                return null;
            }

        }



        /// <summary>
        /// �����ڴ��е��ַ����ݣ�ʹ��Ĭ��key��iv
        /// ��������Key��NULL�����ǿ��ַ�����ʹ��Ĭ��Key��
        /// �������ֵ�ľͰ���ֵ��Ĭ���ļ�ȥ��Key��
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keyVersion"></param>
        /// <returns></returns>
        public string EncryptTextToMemory(string data, string keyVersion)
        {
            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            // ����DES����
            DES dESalg = DES.Create();
            dESalg.Key = key;
            dESalg.IV = iv;
            return EncryptTextToMemory(data, dESalg.Key, dESalg.IV);
        }

        /// <summary>
        /// �����ڴ��е��ַ������ݣ�ʹ��Ĭ��key��iv
        /// ��������Key��NULL�����ǿ��ַ�����ʹ��Ĭ��Key��
        /// �������ֵ�ľͰ���ֵ��Ĭ���ļ�ȥ��Key��
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keyVersion"></param>
        /// <returns></returns>
        public string DecryptTextFromMemory(string data,string keyVersion)
        {
            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            // ����DES����
            DES dESalg = DES.Create();
            dESalg.Key = key;
            dESalg.IV = iv;
            return DecryptTextFromMemory(data, dESalg.Key, dESalg.IV);
        }
    }
}