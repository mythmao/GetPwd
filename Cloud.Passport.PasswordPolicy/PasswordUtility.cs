using System;
using System.Collections.Generic;
using System.Text;
using PasswordPolicy.Util;
using PasswordPolicy.Algorithm;
using System.Text.RegularExpressions;

namespace PasswordPolicy
{
    /// <summary>
    /// ���빤����
    /// </summary>
    public class PasswordUtility
    {
        #region ˽�г�Ա

        private static object _lockPad;
        private static PasswordUtility _Instance;
        private string _KeyVersion;
        IDictionary<string, string> _PolicyDic = new Dictionary<string, string>();
        IDictionary<string, PasswordPolicyEntity> _PassworPolicyDic = new Dictionary<string, PasswordPolicyEntity>();

        #endregion

        #region ������̬����

        /// <summary>
        /// ��ȡӦ������ʵ��
        /// </summary>
        /// <returns></returns>
        public static PasswordUtility GetInstance(string fromSystem, string keyVersion)
        {
            if (_Instance == null)
            {
                _lockPad = new object();
                lock (_lockPad)
                {
                    if (_Instance == null)
                    {
                        _Instance = new PasswordUtility(fromSystem, keyVersion);
                    }
                }
            }

            return _Instance;
        }


        /// <summary>
        /// ��ȡӦ������ʵ��
        /// </summary>
        /// <returns></returns>
        public static PasswordUtility GetInstance()
        {
            if (_Instance == null)
            {
                _lockPad = new object();
                lock (_lockPad)
                {
                    if (_Instance == null)
                    {
                        _Instance = new PasswordUtility(string .Empty, string .Empty);
                    }
                }
            }

            return _Instance;
        }

        #endregion

        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="fromSystem">ϵͳ��ʶ</param>
        /// <param name="keyVersion">Key�汾</param>
        public PasswordUtility(string fromSystem, string keyVersion)
        {
            Init(fromSystem, keyVersion);
        }

        #endregion

        #region ��������

        /// <summary>
        /// DES���ܰ汾��
        /// </summary>
        public string KeyVersion
        {
            get { return _KeyVersion; }
        }
        
        /// <summary>
        /// �����������ʵ���ֵ�
        /// </summary>
        public IDictionary<string,PasswordPolicyEntity> PassworPolicyDic
        {
            get { return _PassworPolicyDic; }
        }

        #endregion

        #region ��������

        /// <summary>
        /// �������ǿ���Ƿ����ָ����������ԣ�
        /// fromSystem����������ʱ����������
        /// ���߼��ϲ����ж�
        /// ���Ը��ݲ�ͬ��ϵͳ���ò�ͬǿ�Ȳ���
        /// ����Ĳ��Ի��ƿ���ͨ�������ļ���ȡ
        /// ��������1��2��3����Խ��Խ�ߣ�����0�ǲ��������ϣ�
        /// 1.	�����ַ�������6��12���ַ����޸İ汾���20λ��
        /// 2.	����ʹ�õ��ַ���Χ��Ӣ����ĸ�����ֺͿɴ�ӡ����
        /// 3.	��ȫ���𣺲���ȫ����ͨ����ȫ
        /// a)	����ȫ��ֻʹ����һ���ַ�����ֻ��Ӣ����ĸ����ֻ�����֣���ֻ�з���
        /// b)	��ͨ��ֻʹ���������ַ�(�����һ�ַ�ʽ����ʹ���������ַ����ǳ��Ȳ���8λ��)
        /// c)	��ȫ��ʹ���������ַ�����ǿ�˼�����ֻ�г��ȹ�8Ϊλ�������ַ���Ϊǿ��
        /// d)  ��һЩ�ų������ֵ䣨������ȫ�ظ��ġ���ĸ�����ظ�3�ĵȷ���0��Ϊ������ģ�
        /// </summary>
        /// <param name="password">����</param>
        /// <returns></returns>
        public int CheckPasswordStrongPolicy(string password)
        {
            //������ʽ�ֵ��жϵĲ���
            ///�����ж�
            string exprre = _PolicyDic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + "1"];

            if (Regex.IsMatch(@password.Trim(), @"[^a-zA-Z0-9!@$_]"))
            {
                return 0;
            }
            if (!PasswordPolicyUtility.IsFormatValidate(@password, exprre))
            {
                return 0;
            }

            //�ų����ʽ
            for (int j = 2; j < 5; j++)
            {
                string ex = _PolicyDic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + j];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return 0;
                }
            }

            //ȫ����ͬ���ַ���Ϊ����Ϊ0
            if (PasswordPolicyUtility.IsAllSameCharInStr(password))
            {
                return 0;
            }
            
            ///ǿ�ȱ��ʽ
            for (int i = 0; i < 3; i++)
            {
                string ex = _PolicyDic[PasswordPolicyConstWord.PasswordRuleRegularExpression + (i + 1)];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return i + 1;
                }
            }

            ///��ӵ�һ��ǿ�ȼ�飨ʹ���������ַ����ǳ��Ȳ���8λ�ģ�
            string ex2 = @"(^(?!(?:[^a-zA-Z]+$|\D|(^[A-Za-z0-9]+$))).{6,7}$)";

            if (PasswordPolicyUtility.IsFormatValidate(@password, ex2))
                return 2;

            return 0;
        }


        /// <summary>
        /// �������ǿ���Ƿ����ָ����������ԣ�
        /// fromSystem����������ʱ����������
        /// ���߼��ϲ����ж�
        /// ���Ը��ݲ�ͬ��ϵͳ���ò�ͬǿ�Ȳ���
        /// ����Ĳ��Ի��ƿ���ͨ�������ļ���ȡ
        /// ��������1��2��3����Խ��Խ�ߣ�����0�ǲ��������ϣ�
        /// 1.	�����ַ�������6��12���ַ����޸İ汾���20λ��
        /// 2.	����ʹ�õ��ַ���Χ��Ӣ����ĸ�����ֺͿɴ�ӡ����
        /// 3.	��ȫ���𣺲���ȫ����ͨ����ȫ
        /// a)	����ȫ��ֻʹ����һ���ַ�����ֻ��Ӣ����ĸ����ֻ�����֣���ֻ�з���
        /// b)	��ͨ��ֻʹ���������ַ�(�����һ�ַ�ʽ����ʹ���������ַ����ǳ��Ȳ���8λ��)
        /// c)	��ȫ��ʹ���������ַ�����ǿ�˼�����ֻ�г��ȹ�8Ϊλ�������ַ���Ϊǿ��
        /// d)  ��һЩ�ų������ֵ䣨������ȫ�ظ��ġ���ĸ�����ظ�3�ĵȷ���0��Ϊ������ģ�
        /// </summary>
        /// <param name="passportname">�û���</param>
        /// <param name="password">����</param>
        /// <param name="fromsystem">ϵͳ��ʾ</param>
        /// <returns></returns>
        public int CheckPasswordStrongPolicy(string passportname,string password,string fromsystem)
        {
            //������ʽ�ֵ��жϵĲ���
            PasswordPolicyEntity ppe = GetPasswordPolicyEntityByFromsystem(fromsystem);

            //�û�������
            if(fromsystem == "my")
            {
                return CheckPasswordStrongPolicyByMy(passportname, password,string .Empty , ppe);
            }

            IDictionary<string, string> expdic = ppe.ExPasswordRuleRegularExpressionDic;
            IDictionary<string, string> pdic = ppe.PasswordRuleRegularExpressionDic;

            ///�����ж�
            string exprre = expdic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + "1"];
            if (!PasswordPolicyUtility.IsFormatValidate(@password, exprre))
            {
                return 0;
            }

            //�ų����ʽ
            for (int j = 2; j < 5; j++)
            {
                string ex = expdic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + j];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return 0;
                }
            }

            //ȫ����ͬ���ַ���Ϊ����Ϊ0
            if (PasswordPolicyUtility.IsAllSameCharInStr(password))
            {
                return 0;
            }

            ///ǿ�ȱ��ʽ
            for (int i = 0; i < 3; i++)
            {
                string ex = pdic[PasswordPolicyConstWord.PasswordRuleRegularExpression + (i + 1)];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return i + 1;
                }
            }

            ///��ӵ�һ��ǿ�ȼ�飨ʹ���������ַ����ǳ��Ȳ���8λ�ģ�
            string ex2 = @"(^(?!(?:[^a-zA-Z]+$|\D|(^[A-Za-z0-9]+$))).{6,7}$)";

            if (PasswordPolicyUtility.IsFormatValidate(@password, ex2))
                return 2;

            return 0;
        }


        /// <summary>
        /// �������ǿ���Ƿ����ָ����������ԣ�
        /// fromSystem����������ʱ����������
        /// ���߼��ϲ����ж�
        /// ���Ը��ݲ�ͬ��ϵͳ���ò�ͬǿ�Ȳ���
        /// ����Ĳ��Ի��ƿ���ͨ�������ļ���ȡ
        /// ��������1��2��3����Խ��Խ�ߣ�����0�ǲ��������ϣ�
        /// 1.	�����ַ�������6��12���ַ����޸İ汾���20λ��
        /// 2.	����ʹ�õ��ַ���Χ��Ӣ����ĸ�����ֺͿɴ�ӡ����
        /// 3.	��ȫ���𣺲���ȫ����ͨ����ȫ
        /// a)	����ȫ��ֻʹ����һ���ַ�����ֻ��Ӣ����ĸ����ֻ�����֣���ֻ�з���
        /// b)	��ͨ��ֻʹ���������ַ�(�����һ�ַ�ʽ����ʹ���������ַ����ǳ��Ȳ���8λ��)
        /// c)	��ȫ��ʹ���������ַ�����ǿ�˼�����ֻ�г��ȹ�8Ϊλ�������ַ���Ϊǿ��
        /// d)  ��һЩ�ų������ֵ䣨������ȫ�ظ��ġ���ĸ�����ظ�3�ĵȷ���0��Ϊ������ģ�
        /// </summary>
        /// <param name="passportname">�û���</param>
        /// <param name="password">����</param>
        /// <param name="email">�ʼ���ַ</param>
        /// <param name="fromsystem">ϵͳ��ʾ</param>
        /// <returns></returns>
        public int CheckPasswordStrongPolicy(string passportname, string password,string email, string fromsystem)
        {
            //������ʽ�ֵ��жϵĲ���
            PasswordPolicyEntity ppe = GetPasswordPolicyEntityByFromsystem(fromsystem);

            if (fromsystem == "my")
            {
                return CheckPasswordStrongPolicyByMy(passportname, password, email, ppe);
            }

            IDictionary<string, string> expdic = ppe.ExPasswordRuleRegularExpressionDic;
            IDictionary<string, string> pdic = ppe.PasswordRuleRegularExpressionDic;

            ///�����ж�
            string exprre = expdic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + "1"];
            if (!PasswordPolicyUtility.IsFormatValidate(@password, exprre))
            {
                return 0;
            }

            //�ų����ʽ
            for (int j = 2; j < 5; j++)
            {
                string ex = expdic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + j];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return 0;
                }
            }

            //ȫ����ͬ���ַ���Ϊ����Ϊ0
            if (PasswordPolicyUtility.IsAllSameCharInStr(password))
            {
                return 0;
            }

            ///ǿ�ȱ��ʽ
            for (int i = 0; i < 3; i++)
            {
                string ex = pdic[PasswordPolicyConstWord.PasswordRuleRegularExpression + (i + 1)];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return i + 1;
                }
            }

            ///��ӵ�һ��ǿ�ȼ�飨ʹ���������ַ����ǳ��Ȳ���8λ�ģ�
            string ex2 = @"(^(?!(?:[^a-zA-Z]+$|\D|(^[A-Za-z0-9]+$))).{6,7}$)";

            if (PasswordPolicyUtility.IsFormatValidate(@password, ex2))
                return 2;

            return 0;
        }


        #region ����������ָ���ϵͳ


        /// <summary>
        /// �������ǿ���Ƿ����ָ����������ԣ�
        /// fromSystem����������ʱ����������
        /// ���߼��ϲ����ж�
        /// ���Ը��ݲ�ͬ��ϵͳ���ò�ͬǿ�Ȳ���
        /// ����Ĳ��Ի��ƿ���ͨ�������ļ���ȡ
        /// ��������1��2��3����Խ��Խ�ߣ�����0�ǲ��������ϣ�
        /// 1.	�����ַ�������6��12���ַ����޸İ汾���20λ��
        /// 2.	����ʹ�õ��ַ���Χ��Ӣ����ĸ�����ֺͿɴ�ӡ����
        /// 3.	��ȫ���𣺲���ȫ����ͨ����ȫ
        /// a)	����ȫ��ֻʹ����һ���ַ�����ֻ��Ӣ����ĸ����ֻ�����֣���ֻ�з���
        /// b)	��ͨ��ֻʹ���������ַ�(�����һ�ַ�ʽ����ʹ���������ַ����ǳ��Ȳ���8λ��)
        /// c)	��ȫ��ʹ���������ַ�����ǿ�˼�����ֻ�г��ȹ�8Ϊλ�������ַ���Ϊǿ��
        /// d)  ��һЩ�ų������ֵ䣨������ȫ�ظ��ġ���ĸ�����ظ�3�ĵȷ���0��Ϊ������ģ�
        /// </summary>
        /// <param name="passportname">�û���</param>
        /// <param name="password">����</param>
        /// <param name="email">�û��ʼ�</param>
        /// <param name="fromsystem">ϵͳ��ʾ</param>
        /// <returns></returns>
        private int CheckPasswordStrongPolicyByMy(string passportname, string password,string email, PasswordPolicyEntity ppe)
        {
            //������ʽ�ֵ��жϵĲ���

            if (ppe.PasswordIsSamePassportName > 0 && !string.IsNullOrEmpty(passportname))
            {
                string oldName = GetOldUserName(passportname, "my");

                if (password.IndexOf(oldName) >=0)
                    return 0;
            }

            if (ppe.PasswordIsSamePassportName == 2 && !string.IsNullOrEmpty(email))
            {
                if (password.IndexOf(email) >=0)
                    return 0;
            }

            IDictionary<string, string> expdic = ppe.ExPasswordRuleRegularExpressionDic;
            IDictionary<string, string> pdic = ppe.PasswordRuleRegularExpressionDic;

            ///�����ж�
            string exprre = expdic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + "1"];
            if (!PasswordPolicyUtility.IsFormatValidate(@password, exprre))
            {
                return 0;
            }

            //�ղ��������۵��Ǹ����������ȷ��һ�£�
            //1��	��ĸ������ Ok
            //2��	��ĸ�������ַ� ok
            //3��	��ĸ�������ַ������� ok
            //4��	���ֺ������ַ� no
            //5��	ֻ����ĸ no
            //6��	ֻ������no
            //7��	ֻ�������ַ�no
            //8��	����Ϊ6-20 ok
            //9��	����������ַ����ܱ���    ��@��&-��<  ok
            //10������������ʹ��һ����ĸ��һ�����ֻ���һ������ ok

            //�ų����ʽ
            for (int j = 2; j < 5; j++)
            {
                string ex = expdic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + j];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return 0;
                }
            }

            //ȫ����ͬ���ַ���Ϊ����Ϊ0
            if (PasswordPolicyUtility.IsAllSameCharInStr(password))
            {
                return 0;
            }

            //ֻ����ĸ�������ֻ��������ַ�
            string ex1 = pdic[PasswordPolicyConstWord.PasswordRuleRegularExpression + 1];

            if (PasswordPolicyUtility.IsFormatValidate(@password, ex1))
            {
                return 0;
            }

            //���ֺ������ַ����
            ex1 = @"^[\W+\d]+$";
            if (PasswordPolicyUtility.IsFormatValidate(@password, ex1))
            {
                return 0;
            }

            //�ж������ַ��Ƿ����
            if (PasswordPolicyUtility.IsSpecialCharInStr(@password))
            {
                return 0;
            }

            ///ǿ�ȱ��ʽ
            for (int i = 0; i < 2; i++)
            {
                string ex = pdic[PasswordPolicyConstWord.PasswordRuleRegularExpression + (i + 2)];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return i + 1;
                }
            }

            ///��ӵ�һ��ǿ�ȼ�飨ʹ���������ַ����ǳ��Ȳ���8λ�ģ�
            string ex2 = @"(^(?!(?:[^a-zA-Z]+$|\D|(^[A-Za-z0-9]+$))).{6,7}$)";

            if (PasswordPolicyUtility.IsFormatValidate(@password, ex2))
                return 2;

            return 0;
        }

        #endregion

        /// <summary>
        /// ����ϵͳ��ʾ��ȡ������Զ���
        /// </summary>
        /// <param name="fromsystem">ϵͳ��ʾ</param>
        /// <returns></returns>
        public PasswordPolicyEntity GetPasswordPolicyEntityByFromsystem(string fromsystem)
        {
            if (!PassworPolicyDic.ContainsKey(fromsystem))
                fromsystem = "xueda.com";
            PasswordPolicyEntity ppe = PassworPolicyDic[fromsystem];

            return ppe;
        }


        /// <summary>
        /// Hash���룬�û�����Ϊ���Ӳ���
        /// </summary>
        /// <param name="username">�û���</param>
        /// <param name="password">����</param>
        /// <returns></returns>
        public string HashPassword(string username, string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;
            
            string unp = PasswordPolicyUtility.GetPasswordPolicyStr(username.ToLower(), @password);

            return PasswordPolicyUtility.GetEncryptPassword(unp);
        }

        /// <summary>
        /// �����û����룬�û�����Ϊ���Ӳ���
        /// �����͵��ĸ���������Ҫ��Key������Ҫ����AppSettings��������Key�ļ���·��
        /// </summary>
        /// <param name="username">�û���</param>
        /// <param name="password">����</param>
        /// <returns></returns>
        public string EncryptPasswordByDES(string username, string password)
        {
            string keyV = _PolicyDic[PasswordPolicyConstWord.PasswordDesKeyVerSion];
            if (string.IsNullOrEmpty(keyV))
                keyV = _KeyVersion;

            return EncryptPasswordByDES(username.ToLower(), @password, keyV);
        }

        /// <summary>
        /// �����û����룬�û�����Ϊ���Ӳ���
        /// �����͵��ĸ���������Ҫ��Key������Ҫ����AppSettings��������Key�ļ���·��
        /// </summary>
        /// <param name="encryptPasswordText">Ҫ���ܵ�����</param>
        /// <param name="userName">�û���</param>
        /// <returns></returns>
        public string UnEncryptPasswordByDES(string encryptPasswordText, string userName)
        {
            string keyV = _PolicyDic[PasswordPolicyConstWord.PasswordDesKeyVerSion];
            if (string.IsNullOrEmpty(keyV))
                keyV = _KeyVersion;

            return UnEncryptPasswordByDES(encryptPasswordText, userName.ToLower(), keyV);
        }



        /// <summary>
        /// �����û����룬�û�����Ϊ���Ӳ���
        /// �����͵��ĸ���������Ҫ��Key������Ҫ����AppSettings��������Key�ļ���·��
        /// ���ð汾Ҳ���Դ��ļ���ȡ���������ý��϶�ȡ
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="password">����</param>
        /// <param name="keyVersion">����Key�İ汾��</param>
        /// <returns></returns>
        public string EncryptPasswordByDES(string userName, string password, string keyVersion)
        {
            //string fileName = _PolicyDic["PasswordDesKeyFile"];

            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            //���ļ�����ȡ�ļ��е�Key��IV
            //string[] keys = SysStringAccess.GetPasswordKeyFileKeyIv(@fileName, keyVersion);
            string keyStr = _PolicyDic[PasswordPolicyConstWord.PasswordDesKey];
            string ivStr = _PolicyDic[PasswordPolicyConstWord.PasswordDesKeyIv];

            if (!string.IsNullOrEmpty(keyStr) && !string.IsNullOrEmpty(ivStr))
            {
                if (UTF8Encoding.UTF8.GetBytes(keyStr).Length == 8 && UTF8Encoding.UTF8.GetBytes(ivStr).Length == 8)
                {
                    key = UTF8Encoding.UTF8.GetBytes(keyStr);
                    iv = UTF8Encoding.UTF8.GetBytes(ivStr);
                }
            }

            //�����û���Ϊ��ʱ���ж�
            string unp = string.Empty;

            if (string.IsNullOrEmpty(userName))
                unp = @password;
            else
                unp = PasswordPolicyUtility.GetPasswordPolicyAndStr(userName.ToLower(), @password);
            //string unp = PasswordPolicyUtility.GetPasswordPolicyStr(username, password);

            //����ʵ��ȡ��
            IAlgorithm algorithm = AlgorithmManager.GetAlgorithm(AlgorithmType.DES);

            //���ؽ����ַ�
            string strEecrypt = (algorithm as DESAlgorithm).EncryptTextToMemory(unp, key, iv);

            return strEecrypt;
        }

        /// <summary>
        /// �����û����룬�û�����Ϊ���Ӳ���
        /// �����͵��ĸ���������Ҫ��Key������Ҫ����AppSettings��������Key�ļ���·��
        /// ���ð汾Ҳ���Դ��ļ���ȡ���������ý��϶�ȡ
        /// </summary>
        /// <param name="encryptPasswordText">Ҫ���ܵ�����</param>
        /// <param name="userName">�û���</param>
        /// <param name="keyVersion">����Key�İ汾��</param>
        /// <returns></returns>
        public string UnEncryptPasswordByDES(string encryptPasswordText, string userName, string keyVersion)
        {
            //string fileName = _PolicyDic["PasswordDesKeyFile"];

            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            //���ļ�����ȡ�ļ��е�Key��IV
            //string[] keys = SysStringAccess.GetPasswordKeyFileKeyIv(@fileName, keyVersion);
            string keyStr = _PolicyDic[PasswordPolicyConstWord.PasswordDesKey];
            string ivStr = _PolicyDic[PasswordPolicyConstWord.PasswordDesKeyIv];

            if (!string.IsNullOrEmpty(keyStr) && !string.IsNullOrEmpty(ivStr))
            {
                if (UTF8Encoding.UTF8.GetBytes(keyStr).Length == 8 && UTF8Encoding.UTF8.GetBytes(ivStr).Length == 8)
                {
                    key = UTF8Encoding.UTF8.GetBytes(keyStr);
                    iv = UTF8Encoding.UTF8.GetBytes(ivStr);
                }
            }

            //����ʵ��ȡ��
            IAlgorithm algorithm = AlgorithmManager.GetAlgorithm(AlgorithmType.DES);

            //���ؽ����ַ�
            string strDecrypt = (algorithm as DESAlgorithm).DecryptTextFromMemory(encryptPasswordText, key, iv);

            //�����û���Ϊ��ʱ���ж�
            string unp = string.Empty;

            if (string.IsNullOrEmpty(userName))
                unp = strDecrypt;
            else
                unp = PasswordPolicyUtility.GetUnPasswordPolicyAndStr(userName.ToLower(), strDecrypt); 
            //string unp = PasswordPolicyUtility.GetUnPasswordPolicyStr(userName, strDecrypt);

            return unp;
        }

        /// <summary>
        /// �����û����룬�û�����Ϊ���Ӳ���
        /// �����͵��ĸ���������Ҫ��Key������Ҫ����AppSettings��������Key�ļ���·��
        /// ���ð汾Ҳ���Դ��ļ���ȡ���������ý��϶�ȡ
        /// </summary>
        /// <param name="encryptPasswordText">Ҫ���ܵ�����</param>
        /// <param name="userName">�û���</param>
        /// <returns></returns>
        public string UnOldEncryptPasswordByDES(string encryptPasswordText, string userName)
        {
            //string fileName = _PolicyDic["PasswordDesKeyFile"];

            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            ////���ļ�����ȡ�ļ��е�Key��IV
            ////string[] keys = SysStringAccess.GetPasswordKeyFileKeyIv(@fileName, keyVersion);
            //string keyStr = _PolicyDic[PasswordPolicyConstWord.PasswordDesKey];
            //string ivStr = _PolicyDic[PasswordPolicyConstWord.PasswordDesKeyIv];

            //if (!string.IsNullOrEmpty(keyStr) && !string.IsNullOrEmpty(ivStr))
            //{
            //    if (UTF8Encoding.UTF8.GetBytes(keyStr).Length == 8 && UTF8Encoding.UTF8.GetBytes(ivStr).Length == 8)
            //    {
            //        key = UTF8Encoding.UTF8.GetBytes(keyStr);
            //        iv = UTF8Encoding.UTF8.GetBytes(ivStr);
            //    }
            //}

            //����ʵ��ȡ��
            IAlgorithm algorithm = AlgorithmManager.GetAlgorithm(AlgorithmType.DES);

            //���ؽ����ַ�
            string strDecrypt = (algorithm as DESAlgorithm).DecryptTextFromMemory(encryptPasswordText, key, iv);

            //�����û���Ϊ��ʱ���ж�
            string unp = string.Empty;

            if (string.IsNullOrEmpty(userName))
                unp = strDecrypt;
            else
                unp = PasswordPolicyUtility.GetUnPasswordPolicyAndStr(userName.ToLower(), strDecrypt);
            //string unp = PasswordPolicyUtility.GetUnPasswordPolicyStr(userName, strDecrypt);

            return unp;
        }

        /// <summary>
        /// ȡ����ϵͳ�û���ȥ����׺��
        /// </summary>
        /// <param name="inputName">�����û���</param>
        /// <param name="fromsystem">ϵͳ��ʾ</param>
        /// <returns></returns>
        public string GetOldUserName(string inputName,string fromsystem)
        {
            string domainName = string.Empty;
            
            if (fromsystem == "my")
            {
                domainName = "@my.xueda.com";
            }
            else if (fromsystem == "ehr")
            {
                domainName = "@ehr.xueda.com";
            }
            else
            {
                domainName = "@admin@xueda.com";
            }

            int i = inputName.ToLower().LastIndexOf(domainName);

            if (i < 0)
                return inputName;

            if (i == 0)
                return string.Empty;

            string tmpU = inputName.Substring(0, i);

            return tmpU;
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="fromSystem">��ʶ</param>
        /// <param name="keyVersion">����汾</param>
        private void Init(string fromSystem, string keyVersion)
        {
            #region ��ʼ��

            string fileName = PasswordConfiguration.Instance().PasswordPolicyFileName;

            if (string.IsNullOrEmpty(fileName))
                fileName = "";

            //ȡ��Ĭ��ϵͳ����
            if (string.IsNullOrEmpty(fromSystem))
            {
                fromSystem = PasswordPolicyConstWord.DefaultStr;
            }

            //ȡ��Ĭ�ϰ汾��
            if (string.IsNullOrEmpty(keyVersion))
            {
                _KeyVersion = PasswordPolicyConstWord.ZeroStr;
            }

            _PolicyDic = PasswordPolicyUtility.GetPasswordPolicyXml(fileName, fromSystem);

            if (!string.IsNullOrEmpty(_PolicyDic[PasswordPolicyConstWord.PasswordDesKeyVerSion]))
                _KeyVersion = _PolicyDic[PasswordPolicyConstWord.PasswordDesKeyVerSion];

            _PassworPolicyDic = PasswordPolicyUtility.GetPasswordPolicyXmlAll(fileName);

            #endregion
        }

        #endregion
    }
}