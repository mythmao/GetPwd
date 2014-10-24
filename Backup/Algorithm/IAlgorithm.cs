using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordPolicy.Algorithm
{
    /// <summary>
    /// 加解密算法接口
    /// </summary>
    public interface IAlgorithm
    {
        ///// <summary>
        ///// 加密内存中的字符数据
        ///// </summary>
        ///// <param name="data">待加密数据</param>
        ///// <param name="key">密钥</param>
        ///// <param name="iv">初始化向量</param>
        ///// <returns></returns>
        //string EncryptTextToMemory(string data, byte[] key, byte[] iv);
        ///// <summary>
        ///// 解密内存中的数据
        ///// </summary>
        ///// <param name="data">待解密数据</param>
        ///// <param name="key">密钥</param>
        ///// <param name="iv">初始化向量</param>
        ///// <returns></returns>
        //string DecryptTextFromMemory(string data, byte[] key, byte[] iv);
        /// <summary>
        /// 加密内存中的字符数据，使用des默认key及iv
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string EncryptTextToMemory(string data);
        /// <summary>
        /// 解密内存中的字符串数据，使用des默认key及iv
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string DecryptTextFromMemory(string data);
        ///// <summary>
        ///// 加密内存中的二进制数组，使用默认key及iv
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //string EncryptBytesToMemory(byte[] data);

        ///// <summary>
        ///// 解密内存中的二进制数组，使用默认key及iv
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //byte[] DecryptBytesFromMemory(string data);

        ///// <summary>
        ///// 加密内存中的二进制数组
        ///// </summary>
        ///// <param name="data">待加密数据</param>
        ///// <param name="key">密钥</param>
        ///// <param name="iv">初始化向量</param>
        ///// <returns></returns>
        //string EncryptBytesToMemory(byte[] data, byte[] key, byte[] iv);

        ///// <summary>
        ///// 解密内存中的二进制数组
        ///// </summary>
        ///// <param name="data">待解密数据</param>
        ///// <param name="key">密钥</param>
        ///// <param name="iv">初始化向量</param>
        ///// <returns></returns>
        //byte[] DecryptBytesFromMemory(string data, byte[] key, byte[] iv);
    }
}