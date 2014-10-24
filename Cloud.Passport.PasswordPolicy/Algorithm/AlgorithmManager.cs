using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordPolicy.Algorithm
{
    /// <summary>
    /// �����㷨������
    /// </summary>
    public class AlgorithmManager
    {
        /// <summary>
        /// ��������ȡ�ü�������ʵ��
        /// </summary>
        /// <param name="at"></param>
        /// <returns></returns>
        public static IAlgorithm GetAlgorithm(AlgorithmType at)
        {
            IAlgorithm algorithm;
            switch (at)
            {
                case AlgorithmType.DES:
                    algorithm = new DESAlgorithm();
                    break;
                case AlgorithmType.Tripl:
                    algorithm = null;
                    break;
                case AlgorithmType.RC2:
                    algorithm = null;
                    break;
                case AlgorithmType.RC4:
                    algorithm = null;
                    break;
                case AlgorithmType.CAST:
                    algorithm = null;
                    break;
                case AlgorithmType.Base64:
                    algorithm = new Base64Algorithm();
                    break;
                case AlgorithmType.MD5:
                    algorithm = new MD5Algorithm();
                    break;
                default:
                    algorithm = new Base64Algorithm();
                    break;
            }
            return algorithm;
        }
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum AlgorithmType
    {
        /// <summary>
        /// �ԳƼ���
        /// </summary>
        DES,
        /// <summary>
        /// ����
        /// </summary>
        Tripl,
        /// <summary>
        /// ��Դ����
        /// </summary>
        RC2,
        /// <summary>
        /// ��Դ����
        /// </summary>
        RC4,
        /// <summary>
        /// Cast�㷨
        /// </summary>
        CAST,
        /// <summary>
        /// �ַ���Base64�㷨
        /// </summary>
        Base64,
        /// <summary>
        /// �����MD5����
        /// </summary>
        MD5
    }
}