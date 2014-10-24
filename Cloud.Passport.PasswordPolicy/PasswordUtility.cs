using System;
using System.Collections.Generic;
using System.Text;
using PasswordPolicy.Util;
using PasswordPolicy.Algorithm;
using System.Text.RegularExpressions;

namespace PasswordPolicy
{
    /// <summary>
    /// 密码工具类
    /// </summary>
    public class PasswordUtility
    {
        #region 私有成员

        private static object _lockPad;
        private static PasswordUtility _Instance;
        private string _KeyVersion;
        IDictionary<string, string> _PolicyDic = new Dictionary<string, string>();
        IDictionary<string, PasswordPolicyEntity> _PassworPolicyDic = new Dictionary<string, PasswordPolicyEntity>();

        #endregion

        #region 公共静态方法

        /// <summary>
        /// 获取应用宿主实例
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
        /// 获取应用宿主实例
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

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fromSystem">系统标识</param>
        /// <param name="keyVersion">Key版本</param>
        public PasswordUtility(string fromSystem, string keyVersion)
        {
            Init(fromSystem, keyVersion);
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// DES加密版本号
        /// </summary>
        public string KeyVersion
        {
            get { return _KeyVersion; }
        }
        
        /// <summary>
        /// 返回密码策略实体字典
        /// </summary>
        public IDictionary<string,PasswordPolicyEntity> PassworPolicyDic
        {
            get { return _PassworPolicyDic; }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 检查密码强度是否符合指定的密码策略，
        /// fromSystem参数可以暂时仅仅保留，
        /// 但逻辑上不做判断
        /// 可以根据不同的系统采用不同强度策略
        /// 具体的策略机制可以通过配置文件获取
        /// 返回数字1、2、3级别越来越高（返回0是不都不符合）
        /// 1.	密码字符个数：6～12个字符（修改版本最多20位）
        /// 2.	允许使用的字符范围：英文字母、数字和可打印符号
        /// 3.	安全级别：不安全－普通－安全
        /// a)	不安全：只使用了一类字符，即只有英文字母，或只有数字，或只有符号
        /// b)	普通：只使用了两类字符(添加了一种方式就是使用了三类字符但是长度不够8位的)
        /// c)	安全：使用了三类字符（增强了检查策略只有长度够8为位的三类字符才为强）
        /// d)  做一些排除规则字典（数字完全重复的、字母连续重复3的等返回0认为必须更改）
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public int CheckPasswordStrongPolicy(string password)
        {
            //例外表达式字典判断的操作
            ///长度判断
            string exprre = _PolicyDic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + "1"];

            if (Regex.IsMatch(@password.Trim(), @"[^a-zA-Z0-9!@$_]"))
            {
                return 0;
            }
            if (!PasswordPolicyUtility.IsFormatValidate(@password, exprre))
            {
                return 0;
            }

            //排除表达式
            for (int j = 2; j < 5; j++)
            {
                string ex = _PolicyDic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + j];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return 0;
                }
            }

            //全部相同的字符认为级别为0
            if (PasswordPolicyUtility.IsAllSameCharInStr(password))
            {
                return 0;
            }
            
            ///强度表达式
            for (int i = 0; i < 3; i++)
            {
                string ex = _PolicyDic[PasswordPolicyConstWord.PasswordRuleRegularExpression + (i + 1)];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return i + 1;
                }
            }

            ///添加的一种强度检查（使用了三类字符但是长度不够8位的）
            string ex2 = @"(^(?!(?:[^a-zA-Z]+$|\D|(^[A-Za-z0-9]+$))).{6,7}$)";

            if (PasswordPolicyUtility.IsFormatValidate(@password, ex2))
                return 2;

            return 0;
        }


        /// <summary>
        /// 检查密码强度是否符合指定的密码策略，
        /// fromSystem参数可以暂时仅仅保留，
        /// 但逻辑上不做判断
        /// 可以根据不同的系统采用不同强度策略
        /// 具体的策略机制可以通过配置文件获取
        /// 返回数字1、2、3级别越来越高（返回0是不都不符合）
        /// 1.	密码字符个数：6～12个字符（修改版本最多20位）
        /// 2.	允许使用的字符范围：英文字母、数字和可打印符号
        /// 3.	安全级别：不安全－普通－安全
        /// a)	不安全：只使用了一类字符，即只有英文字母，或只有数字，或只有符号
        /// b)	普通：只使用了两类字符(添加了一种方式就是使用了三类字符但是长度不够8位的)
        /// c)	安全：使用了三类字符（增强了检查策略只有长度够8为位的三类字符才为强）
        /// d)  做一些排除规则字典（数字完全重复的、字母连续重复3的等返回0认为必须更改）
        /// </summary>
        /// <param name="passportname">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="fromsystem">系统标示</param>
        /// <returns></returns>
        public int CheckPasswordStrongPolicy(string passportname,string password,string fromsystem)
        {
            //例外表达式字典判断的操作
            PasswordPolicyEntity ppe = GetPasswordPolicyEntityByFromsystem(fromsystem);

            //用户名处理
            if(fromsystem == "my")
            {
                return CheckPasswordStrongPolicyByMy(passportname, password,string .Empty , ppe);
            }

            IDictionary<string, string> expdic = ppe.ExPasswordRuleRegularExpressionDic;
            IDictionary<string, string> pdic = ppe.PasswordRuleRegularExpressionDic;

            ///长度判断
            string exprre = expdic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + "1"];
            if (!PasswordPolicyUtility.IsFormatValidate(@password, exprre))
            {
                return 0;
            }

            //排除表达式
            for (int j = 2; j < 5; j++)
            {
                string ex = expdic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + j];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return 0;
                }
            }

            //全部相同的字符认为级别为0
            if (PasswordPolicyUtility.IsAllSameCharInStr(password))
            {
                return 0;
            }

            ///强度表达式
            for (int i = 0; i < 3; i++)
            {
                string ex = pdic[PasswordPolicyConstWord.PasswordRuleRegularExpression + (i + 1)];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return i + 1;
                }
            }

            ///添加的一种强度检查（使用了三类字符但是长度不够8位的）
            string ex2 = @"(^(?!(?:[^a-zA-Z]+$|\D|(^[A-Za-z0-9]+$))).{6,7}$)";

            if (PasswordPolicyUtility.IsFormatValidate(@password, ex2))
                return 2;

            return 0;
        }


        /// <summary>
        /// 检查密码强度是否符合指定的密码策略，
        /// fromSystem参数可以暂时仅仅保留，
        /// 但逻辑上不做判断
        /// 可以根据不同的系统采用不同强度策略
        /// 具体的策略机制可以通过配置文件获取
        /// 返回数字1、2、3级别越来越高（返回0是不都不符合）
        /// 1.	密码字符个数：6～12个字符（修改版本最多20位）
        /// 2.	允许使用的字符范围：英文字母、数字和可打印符号
        /// 3.	安全级别：不安全－普通－安全
        /// a)	不安全：只使用了一类字符，即只有英文字母，或只有数字，或只有符号
        /// b)	普通：只使用了两类字符(添加了一种方式就是使用了三类字符但是长度不够8位的)
        /// c)	安全：使用了三类字符（增强了检查策略只有长度够8为位的三类字符才为强）
        /// d)  做一些排除规则字典（数字完全重复的、字母连续重复3的等返回0认为必须更改）
        /// </summary>
        /// <param name="passportname">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="email">邮件地址</param>
        /// <param name="fromsystem">系统标示</param>
        /// <returns></returns>
        public int CheckPasswordStrongPolicy(string passportname, string password,string email, string fromsystem)
        {
            //例外表达式字典判断的操作
            PasswordPolicyEntity ppe = GetPasswordPolicyEntityByFromsystem(fromsystem);

            if (fromsystem == "my")
            {
                return CheckPasswordStrongPolicyByMy(passportname, password, email, ppe);
            }

            IDictionary<string, string> expdic = ppe.ExPasswordRuleRegularExpressionDic;
            IDictionary<string, string> pdic = ppe.PasswordRuleRegularExpressionDic;

            ///长度判断
            string exprre = expdic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + "1"];
            if (!PasswordPolicyUtility.IsFormatValidate(@password, exprre))
            {
                return 0;
            }

            //排除表达式
            for (int j = 2; j < 5; j++)
            {
                string ex = expdic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + j];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return 0;
                }
            }

            //全部相同的字符认为级别为0
            if (PasswordPolicyUtility.IsAllSameCharInStr(password))
            {
                return 0;
            }

            ///强度表达式
            for (int i = 0; i < 3; i++)
            {
                string ex = pdic[PasswordPolicyConstWord.PasswordRuleRegularExpression + (i + 1)];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return i + 1;
                }
            }

            ///添加的一种强度检查（使用了三类字符但是长度不够8位的）
            string ex2 = @"(^(?!(?:[^a-zA-Z]+$|\D|(^[A-Za-z0-9]+$))).{6,7}$)";

            if (PasswordPolicyUtility.IsFormatValidate(@password, ex2))
                return 2;

            return 0;
        }


        #region 密码策略区分各个系统


        /// <summary>
        /// 检查密码强度是否符合指定的密码策略，
        /// fromSystem参数可以暂时仅仅保留，
        /// 但逻辑上不做判断
        /// 可以根据不同的系统采用不同强度策略
        /// 具体的策略机制可以通过配置文件获取
        /// 返回数字1、2、3级别越来越高（返回0是不都不符合）
        /// 1.	密码字符个数：6～12个字符（修改版本最多20位）
        /// 2.	允许使用的字符范围：英文字母、数字和可打印符号
        /// 3.	安全级别：不安全－普通－安全
        /// a)	不安全：只使用了一类字符，即只有英文字母，或只有数字，或只有符号
        /// b)	普通：只使用了两类字符(添加了一种方式就是使用了三类字符但是长度不够8位的)
        /// c)	安全：使用了三类字符（增强了检查策略只有长度够8为位的三类字符才为强）
        /// d)  做一些排除规则字典（数字完全重复的、字母连续重复3的等返回0认为必须更改）
        /// </summary>
        /// <param name="passportname">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="email">用户邮件</param>
        /// <param name="fromsystem">系统标示</param>
        /// <returns></returns>
        private int CheckPasswordStrongPolicyByMy(string passportname, string password,string email, PasswordPolicyEntity ppe)
        {
            //例外表达式字典判断的操作

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

            ///长度判断
            string exprre = expdic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + "1"];
            if (!PasswordPolicyUtility.IsFormatValidate(@password, exprre))
            {
                return 0;
            }

            //刚才咱们讨论的那个密码策略再确认一下：
            //1、	字母和数字 Ok
            //2、	字母和特殊字符 ok
            //3、	字母和特殊字符和数字 ok
            //4、	数字和特殊字符 no
            //5、	只有字母 no
            //6、	只有数字no
            //7、	只有特殊字符no
            //8、	长度为6-20 ok
            //9、	如果有特殊字符不能保含    ’@，&-“<  ok
            //10、密码中至少使用一个字母，一个数字或者一个符号 ok

            //排除表达式
            for (int j = 2; j < 5; j++)
            {
                string ex = expdic[PasswordPolicyConstWord.ExPasswordRuleRegularExpression + j];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return 0;
                }
            }

            //全部相同的字符认为级别为0
            if (PasswordPolicyUtility.IsAllSameCharInStr(password))
            {
                return 0;
            }

            //只有字母或者数字或者特殊字符
            string ex1 = pdic[PasswordPolicyConstWord.PasswordRuleRegularExpression + 1];

            if (PasswordPolicyUtility.IsFormatValidate(@password, ex1))
            {
                return 0;
            }

            //数字和特殊字符组合
            ex1 = @"^[\W+\d]+$";
            if (PasswordPolicyUtility.IsFormatValidate(@password, ex1))
            {
                return 0;
            }

            //判断特殊字符是否存在
            if (PasswordPolicyUtility.IsSpecialCharInStr(@password))
            {
                return 0;
            }

            ///强度表达式
            for (int i = 0; i < 2; i++)
            {
                string ex = pdic[PasswordPolicyConstWord.PasswordRuleRegularExpression + (i + 2)];
                if (PasswordPolicyUtility.IsFormatValidate(@password, ex))
                {
                    return i + 1;
                }
            }

            ///添加的一种强度检查（使用了三类字符但是长度不够8位的）
            string ex2 = @"(^(?!(?:[^a-zA-Z]+$|\D|(^[A-Za-z0-9]+$))).{6,7}$)";

            if (PasswordPolicyUtility.IsFormatValidate(@password, ex2))
                return 2;

            return 0;
        }

        #endregion

        /// <summary>
        /// 根据系统标示获取密码策略对象
        /// </summary>
        /// <param name="fromsystem">系统标示</param>
        /// <returns></returns>
        public PasswordPolicyEntity GetPasswordPolicyEntityByFromsystem(string fromsystem)
        {
            if (!PassworPolicyDic.ContainsKey(fromsystem))
                fromsystem = "xueda.com";
            PasswordPolicyEntity ppe = PassworPolicyDic[fromsystem];

            return ppe;
        }


        /// <summary>
        /// Hash密码，用户名作为混杂参数
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public string HashPassword(string username, string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;
            
            string unp = PasswordPolicyUtility.GetPasswordPolicyStr(username.ToLower(), @password);

            return PasswordPolicyUtility.GetEncryptPassword(unp);
        }

        /// <summary>
        /// 加密用户密码，用户名作为混杂参数
        /// 第三和第四个方法所需要的Key，可以要求在AppSettings节内配置Key文件的路径
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public string EncryptPasswordByDES(string username, string password)
        {
            string keyV = _PolicyDic[PasswordPolicyConstWord.PasswordDesKeyVerSion];
            if (string.IsNullOrEmpty(keyV))
                keyV = _KeyVersion;

            return EncryptPasswordByDES(username.ToLower(), @password, keyV);
        }

        /// <summary>
        /// 解密用户密码，用户名作为混杂参数
        /// 第三和第四个方法所需要的Key，可以要求在AppSettings节内配置Key文件的路径
        /// </summary>
        /// <param name="encryptPasswordText">要解密的密码</param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public string UnEncryptPasswordByDES(string encryptPasswordText, string userName)
        {
            string keyV = _PolicyDic[PasswordPolicyConstWord.PasswordDesKeyVerSion];
            if (string.IsNullOrEmpty(keyV))
                keyV = _KeyVersion;

            return UnEncryptPasswordByDES(encryptPasswordText, userName.ToLower(), keyV);
        }



        /// <summary>
        /// 加密用户密码，用户名作为混杂参数
        /// 第三和第四个方法所需要的Key，可以要求在AppSettings节内配置Key文件的路径
        /// 配置版本也可以从文件读取或者是配置节上读取
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="keyVersion">密码Key的版本号</param>
        /// <returns></returns>
        public string EncryptPasswordByDES(string userName, string password, string keyVersion)
        {
            //string fileName = _PolicyDic["PasswordDesKeyFile"];

            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            //读文件名字取文件中的Key和IV
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

            //加入用户名为空时的判断
            string unp = string.Empty;

            if (string.IsNullOrEmpty(userName))
                unp = @password;
            else
                unp = PasswordPolicyUtility.GetPasswordPolicyAndStr(userName.ToLower(), @password);
            //string unp = PasswordPolicyUtility.GetPasswordPolicyStr(username, password);

            //加密实例取得
            IAlgorithm algorithm = AlgorithmManager.GetAlgorithm(AlgorithmType.DES);

            //返回解密字符
            string strEecrypt = (algorithm as DESAlgorithm).EncryptTextToMemory(unp, key, iv);

            return strEecrypt;
        }

        /// <summary>
        /// 解密用户密码，用户名作为混杂参数
        /// 第三和第四个方法所需要的Key，可以要求在AppSettings节内配置Key文件的路径
        /// 配置版本也可以从文件读取或者是配置节上读取
        /// </summary>
        /// <param name="encryptPasswordText">要解密的密码</param>
        /// <param name="userName">用户名</param>
        /// <param name="keyVersion">密码Key的版本号</param>
        /// <returns></returns>
        public string UnEncryptPasswordByDES(string encryptPasswordText, string userName, string keyVersion)
        {
            //string fileName = _PolicyDic["PasswordDesKeyFile"];

            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            //读文件名字取文件中的Key和IV
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

            //加密实例取得
            IAlgorithm algorithm = AlgorithmManager.GetAlgorithm(AlgorithmType.DES);

            //返回解密字符
            string strDecrypt = (algorithm as DESAlgorithm).DecryptTextFromMemory(encryptPasswordText, key, iv);

            //加入用户名为空时的判断
            string unp = string.Empty;

            if (string.IsNullOrEmpty(userName))
                unp = strDecrypt;
            else
                unp = PasswordPolicyUtility.GetUnPasswordPolicyAndStr(userName.ToLower(), strDecrypt); 
            //string unp = PasswordPolicyUtility.GetUnPasswordPolicyStr(userName, strDecrypt);

            return unp;
        }

        /// <summary>
        /// 解密用户密码，用户名作为混杂参数
        /// 第三和第四个方法所需要的Key，可以要求在AppSettings节内配置Key文件的路径
        /// 配置版本也可以从文件读取或者是配置节上读取
        /// </summary>
        /// <param name="encryptPasswordText">要解密的密码</param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public string UnOldEncryptPasswordByDES(string encryptPasswordText, string userName)
        {
            //string fileName = _PolicyDic["PasswordDesKeyFile"];

            Byte[] key = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef };
            Byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

            ////读文件名字取文件中的Key和IV
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

            //加密实例取得
            IAlgorithm algorithm = AlgorithmManager.GetAlgorithm(AlgorithmType.DES);

            //返回解密字符
            string strDecrypt = (algorithm as DESAlgorithm).DecryptTextFromMemory(encryptPasswordText, key, iv);

            //加入用户名为空时的判断
            string unp = string.Empty;

            if (string.IsNullOrEmpty(userName))
                unp = strDecrypt;
            else
                unp = PasswordPolicyUtility.GetUnPasswordPolicyAndStr(userName.ToLower(), strDecrypt);
            //string unp = PasswordPolicyUtility.GetUnPasswordPolicyStr(userName, strDecrypt);

            return unp;
        }

        /// <summary>
        /// 取得老系统用户名去掉后缀的
        /// </summary>
        /// <param name="inputName">输入用户名</param>
        /// <param name="fromsystem">系统标示</param>
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

        #region 私有方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="fromSystem">标识</param>
        /// <param name="keyVersion">密码版本</param>
        private void Init(string fromSystem, string keyVersion)
        {
            #region 初始化

            string fileName = PasswordConfiguration.Instance().PasswordPolicyFileName;

            if (string.IsNullOrEmpty(fileName))
                fileName = "";

            //取得默认系统名称
            if (string.IsNullOrEmpty(fromSystem))
            {
                fromSystem = PasswordPolicyConstWord.DefaultStr;
            }

            //取得默认版本号
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