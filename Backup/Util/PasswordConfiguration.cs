using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Xml;
using System.IO;

namespace PasswordPolicy.Util
{
    /// <summary>
    /// 系统配置类
    /// </summary>
    public class PasswordConfiguration
    {
        #region 静态私有成员

        private static PasswordConfiguration _Instance;
        private static object _lockPad;
        private string _PasswordPolicyFileName;


        #endregion

        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        public PasswordConfiguration()
        {
            Init();
        }

        #endregion

        #region 静态公共方法

        /// <summary>
        /// 取得配置类的实例
        /// </summary>
        /// <returns></returns>
        public static PasswordConfiguration Instance()
        {
            if (_Instance == null)
            {
                _lockPad = new object();
                lock (_lockPad)
                {
                    if (_Instance == null)
                    {
                        _Instance = new PasswordConfiguration();
                    }
                }
            }

            return _Instance;
        }

        #endregion

        #region 静态公共属性

        /// <summary>
        /// 取得整体密码加密策略的文件如果没有，
        /// 个默认的文件存在
        /// </summary>
        public string PasswordPolicyFileName
        {
            get
            {
                return _PasswordPolicyFileName;
            }
        }

        #endregion

        #region 静态私有方法

        private void Init()
        {
            #region 初始化

            object obj = ConfigurationManager.GetSection(PasswordPolicyConstWord.PasswordPolicy);

            if (obj == null)
            {
                _PasswordPolicyFileName = string.Empty;
            }
            else
            {
                XmlElement element = (XmlElement)obj;
                string xpath = PasswordPolicyConstWord.PasswordPolicyFileName;
                XmlNode node = element.SelectSingleNode(xpath);
                if (node == null)
                {
                    _PasswordPolicyFileName = string.Empty;
                }
                else
                {
                    if (node.Attributes[PasswordPolicyConstWord.ValueStr] == null)
                    {
                        _PasswordPolicyFileName = string.Empty;
                    }
                    else
                    {
                        _PasswordPolicyFileName = node.Attributes[PasswordPolicyConstWord.ValueStr].Value;
                    }
                }
            }

            #endregion
        }

        #endregion
    }
}