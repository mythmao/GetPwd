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
    /// ϵͳ������
    /// </summary>
    public class PasswordConfiguration
    {
        #region ��̬˽�г�Ա

        private static PasswordConfiguration _Instance;
        private static object _lockPad;
        private string _PasswordPolicyFileName;


        #endregion

        #region ��̬���캯��

        /// <summary>
        /// ��̬���캯��
        /// </summary>
        public PasswordConfiguration()
        {
            Init();
        }

        #endregion

        #region ��̬��������

        /// <summary>
        /// ȡ���������ʵ��
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

        #region ��̬��������

        /// <summary>
        /// ȡ������������ܲ��Ե��ļ����û�У�
        /// ��Ĭ�ϵ��ļ�����
        /// </summary>
        public string PasswordPolicyFileName
        {
            get
            {
                return _PasswordPolicyFileName;
            }
        }

        #endregion

        #region ��̬˽�з���

        private void Init()
        {
            #region ��ʼ��

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