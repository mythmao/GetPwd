using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordPolicy.Algorithm
{
    /// <summary>
    /// 使用DES对称算法加解密
    /// </summary>
    public class DESAlgorithm : IAlgorithm
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

                // 创建内存流
                MemoryStream mStream = new MemoryStream();

                // 创建DES对象
                DES dESalg = DES.Create();

                // 为内存流创建加密流和加密密钥及初始化向量
                CryptoStream cStream = new CryptoStream(mStream, dESalg.CreateEncryptor(key, iv), CryptoStreamMode.Write);

                // 将字符串变量转变为字节数组
                byte[] toEncrypt = UTF8Encoding.UTF8.GetBytes(data);
                //byte[] toEncrypt = unicodeEncoding.GetBytes(data);

                // 将字节数组写入加密流中，执行加密
                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                cStream.FlushFinalBlock();

                // 将加密后的数据转为字节数组
                byte[] ret = mStream.ToArray();

                // 关闭所有的流
                cStream.Close();
                mStream.Close();

                // 返回加密后数据

                string tt = Convert.ToBase64String(ret);
                return tt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("引起加密出错的原因: {0}", e.Message);
                throw e;
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
                // 通过字节数组创建内存流
                MemoryStream msDecrypt = new MemoryStream(toEncrypt);

                // 创建DES对象
                DES dESalg = DES.Create();

                // 创建加解密流

                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    dESalg.CreateDecryptor(key, iv),
                    CryptoStreamMode.Read);

                StreamReader sr = new StreamReader(csDecrypt);
                return sr.ReadToEnd();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("引起解密错误的原因: {0}", e.Message);
                throw e;
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

            // 创建DES对象
            DES dESalg = DES.Create();
            dESalg.Key = key;
            dESalg.IV = iv;
            return EncryptTextToMemory(data, dESalg.Key, dESalg.IV);
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

            // 创建DES对象
            DES dESalg = DES.Create();
            dESalg.Key = key;
            dESalg.IV = iv;
            return DecryptTextFromMemory(data, dESalg.Key, dESalg.IV);
        }

        /// <summary>
        /// 加密内存中的二进制数组，使用默认key及iv
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string EncryptBytesToMemory(byte[] data)
        {
            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            // 创建DES对象
            DES dESalg = DES.Create();
            dESalg.Key = key;
            dESalg.IV = iv;
            return EncryptBytesToMemory(data, dESalg.Key, dESalg.IV);
        }

        /// <summary>
        /// 解密内存中的二进制数组，使用默认key及iv
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] DecryptBytesFromMemory(string data)
        {
            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            // 创建DES对象
            DES dESalg = DES.Create();
            dESalg.Key = key;
            dESalg.IV = iv;
            return DecryptBytesFromMemory(data, dESalg.Key, dESalg.IV);
        }

        /// <summary>
        /// 加密内存中的二进制数组
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns></returns>
        public string EncryptBytesToMemory(byte[] data, byte[] key, byte[] iv)
        {
            try
            {
                if (data == null)
                    return "";

                // 创建内存流
                MemoryStream mStream = new MemoryStream();

                //创建DES对象
                DES dESalg = DES.Create();

                // 为内存流创建加密流和加密密钥及初始化向量
                CryptoStream cStream = new CryptoStream(mStream, dESalg.CreateEncryptor(key, iv), CryptoStreamMode.Write);

                // 将字符串变量转变为字节数组
                byte[] toEncrypt = data;
                //byte[] toEncrypt = unicodeEncoding.GetBytes(data);

                // 将字节数组写入加密流中，执行加密
                cStream.Write(toEncrypt, 0, toEncrypt.Length);
                cStream.FlushFinalBlock();

                // 将加密后的数据转为字节数组
                byte[] ret = mStream.ToArray();

                // 关闭所有的流
                cStream.Close();
                mStream.Close();

                // 返回加密后数据

                string tt = Convert.ToBase64String(ret);
                return tt;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("引起加密出错的原因: {0}", e.Message);
                return null;
            }
        }

        /// <summary>
        /// 解密内存中的二进制数组
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns></returns>
        public byte[] DecryptBytesFromMemory(string data, byte[] key, byte[] iv)
        {
            try
            {
                if (data == null || data == string.Empty)
                    return null;
                //将加密串转为字节数组
                byte[] toEncrypt = Convert.FromBase64String(data);
                // 通过字节数组创建内存流
                MemoryStream msDecrypt = new MemoryStream(toEncrypt);
                // 创建DES对象
                DES dESalg = DES.Create();

                // 创建加解密流

                CryptoStream csDecrypt = new CryptoStream(msDecrypt,
                    dESalg.CreateDecryptor(key, iv),
                    CryptoStreamMode.Read);

                //StreamReader sr = new StreamReader(csDecrypt);
                // 将加密后的数据转为字节数组
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
                Console.WriteLine("引起解密错误的原因: {0}", e.Message);
                return null;
            }

        }



        /// <summary>
        /// 加密内存中的字符数据，使用默认key及iv
        /// 如果传入的Key是NULL或者是空字符串，使用默认Key。
        /// 如果是有值的就按照值到默认文件去拿Key。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keyVersion"></param>
        /// <returns></returns>
        public string EncryptTextToMemory(string data, string keyVersion)
        {
            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            // 创建DES对象
            DES dESalg = DES.Create();
            dESalg.Key = key;
            dESalg.IV = iv;
            return EncryptTextToMemory(data, dESalg.Key, dESalg.IV);
        }

        /// <summary>
        /// 解密内存中的字符串数据，使用默认key及iv
        /// 如果传入的Key是NULL或者是空字符串，使用默认Key。
        /// 如果是有值的就按照值到默认文件去拿Key。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keyVersion"></param>
        /// <returns></returns>
        public string DecryptTextFromMemory(string data,string keyVersion)
        {
            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            // 创建DES对象
            DES dESalg = DES.Create();
            dESalg.Key = key;
            dESalg.IV = iv;
            return DecryptTextFromMemory(data, dESalg.Key, dESalg.IV);
        }
    }
}