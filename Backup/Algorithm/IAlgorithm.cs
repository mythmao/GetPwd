using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordPolicy.Algorithm
{
    /// <summary>
    /// �ӽ����㷨�ӿ�
    /// </summary>
    public interface IAlgorithm
    {
        ///// <summary>
        ///// �����ڴ��е��ַ�����
        ///// </summary>
        ///// <param name="data">����������</param>
        ///// <param name="key">��Կ</param>
        ///// <param name="iv">��ʼ������</param>
        ///// <returns></returns>
        //string EncryptTextToMemory(string data, byte[] key, byte[] iv);
        ///// <summary>
        ///// �����ڴ��е�����
        ///// </summary>
        ///// <param name="data">����������</param>
        ///// <param name="key">��Կ</param>
        ///// <param name="iv">��ʼ������</param>
        ///// <returns></returns>
        //string DecryptTextFromMemory(string data, byte[] key, byte[] iv);
        /// <summary>
        /// �����ڴ��е��ַ����ݣ�ʹ��desĬ��key��iv
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string EncryptTextToMemory(string data);
        /// <summary>
        /// �����ڴ��е��ַ������ݣ�ʹ��desĬ��key��iv
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string DecryptTextFromMemory(string data);
        ///// <summary>
        ///// �����ڴ��еĶ��������飬ʹ��Ĭ��key��iv
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //string EncryptBytesToMemory(byte[] data);

        ///// <summary>
        ///// �����ڴ��еĶ��������飬ʹ��Ĭ��key��iv
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //byte[] DecryptBytesFromMemory(string data);

        ///// <summary>
        ///// �����ڴ��еĶ���������
        ///// </summary>
        ///// <param name="data">����������</param>
        ///// <param name="key">��Կ</param>
        ///// <param name="iv">��ʼ������</param>
        ///// <returns></returns>
        //string EncryptBytesToMemory(byte[] data, byte[] key, byte[] iv);

        ///// <summary>
        ///// �����ڴ��еĶ���������
        ///// </summary>
        ///// <param name="data">����������</param>
        ///// <param name="key">��Կ</param>
        ///// <param name="iv">��ʼ������</param>
        ///// <returns></returns>
        //byte[] DecryptBytesFromMemory(string data, byte[] key, byte[] iv);
    }
}