using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordPolicy.Algorithm
{
    /// <summary>
    /// 加密算法控制类
    /// </summary>
    public class AlgorithmManager
    {
        /// <summary>
        /// 根据类型取得加密类型实例
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
    /// 加密类型
    /// </summary>
    public enum AlgorithmType
    {
        /// <summary>
        /// 对称加密
        /// </summary>
        DES,
        /// <summary>
        /// 三倍
        /// </summary>
        Tripl,
        /// <summary>
        /// 资源定义
        /// </summary>
        RC2,
        /// <summary>
        /// 资源定义
        /// </summary>
        RC4,
        /// <summary>
        /// Cast算法
        /// </summary>
        CAST,
        /// <summary>
        /// 字符串Base64算法
        /// </summary>
        Base64,
        /// <summary>
        /// 单向的MD5加密
        /// </summary>
        MD5
    }
}