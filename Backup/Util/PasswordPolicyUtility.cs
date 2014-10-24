using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using PasswordPolicy.Algorithm;
using System.Diagnostics;

namespace PasswordPolicy.Util
{
    /// <summary>
    /// 读取密码策略文件类
    /// </summary>
    public class PasswordPolicyUtility
    {
        #region 公共静态方法

        /// <summary>
        /// 返回配置节设定内容
        /// </summary>
        /// <param name="xmlFileName">文件名字</param>
        /// <param name="fromSystem">系统配置节控制</param>
        /// <returns></returns>
        public static IDictionary<string, string> GetPasswordPolicyXml(string xmlFileName, string fromSystem)
        {
            #region 返回配置节设定内容

            var passwordPolicyD = new Dictionary<string, string>();

            var policyXml = new XmlDocument();

            //修改成读取迁入资源
            if (string.IsNullOrEmpty(xmlFileName))
            {
                policyXml.Load(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(@PasswordPolicyConstWord.PolicyConfigRFilePath));
            }
            else
            {
                policyXml.Load(xmlFileName);
            }

            string xpath = PasswordPolicyConstWord.FromSystem;
            XmlNode node = policyXml.DocumentElement.SelectSingleNode(xpath);

            if (node.Attributes[PasswordPolicyConstWord.ValueStr].Value.ToLower().Equals(fromSystem.ToLower()))
            {

                //var policyXmlElementes = policyXml.DocumentElement.SelectNodes(PasswordPolicyConstWord.FromSystem);

                XmlNode fromNode = node;

                //foreach (XmlNode fromNode in policyXmlElementes)
                //{
                    passwordPolicyD.Add(PasswordPolicyConstWord.FromSystem, PasswordPolicyConstWord.DefaultStr);
                    passwordPolicyD.Add(PasswordPolicyConstWord.PasswordDesKeyFile, fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordDesKeyFile).InnerText);
                    passwordPolicyD.Add(PasswordPolicyConstWord.PasswordDesKeyVerSion, fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordDesKeyVerSion).InnerText);

                    //需要添加值
                    passwordPolicyD.Add(PasswordPolicyConstWord.PasswordFailedCount,fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordFailedCount).InnerText);
                    passwordPolicyD.Add(PasswordPolicyConstWord.PasswordIsSamePassportName, fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordIsSamePassportName).InnerText);
                    passwordPolicyD.Add(PasswordPolicyConstWord.PasswordLastMCount, fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordLastMCount).InnerText);
                    passwordPolicyD.Add(PasswordPolicyConstWord.PasswordMinMDayCount, fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordMinMDayCount).InnerText);
                    passwordPolicyD.Add(PasswordPolicyConstWord.PasswordMixMDayCount, fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordMixMDayCount).InnerText);
                    passwordPolicyD.Add(PasswordPolicyConstWord.PasswordUnlockMinute, fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordUnlockMinute).InnerText);


                    AddKeyIvToDic(fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordDesKeyFile).InnerText,
                        fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordDesKeyVerSion).InnerText, passwordPolicyD);

                    SetDicByNodesName(fromNode, passwordPolicyD, PasswordPolicyConstWord.PasswordRuleRegularExpression);

                    SetDicByNodesName(fromNode, passwordPolicyD, PasswordPolicyConstWord.ExPasswordRuleRegularExpression);

                //}
            }


            return passwordPolicyD;

            #endregion
        }

        /// <summary>
        /// 返回配置节设定内容
        /// </summary>
        /// <param name="xmlFileName">文件名字</param>
        /// <returns></returns>
        public static IDictionary<string, PasswordPolicyEntity> GetPasswordPolicyXmlAll(string xmlFileName)
        {
            var passwordPolicyD = new Dictionary<string, PasswordPolicyEntity>();

            var policyXml = new XmlDocument();

            //修改成读取迁入资源
            if (string.IsNullOrEmpty(xmlFileName))
            {
                policyXml.Load(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(@PasswordPolicyConstWord.PolicyConfigRFilePath));
            }
            else
            {
                policyXml.Load(xmlFileName);
            }

            string xpath = PasswordPolicyConstWord.FromSystem;
            XmlNode node = policyXml.DocumentElement.SelectSingleNode(xpath);

            string[] s = new string[] { "my", "ehr", "admin" };

            //for (int i = 0; i < s.Length ; i++)
            //{
            //    string fromSystem = s[i];

            //    if (node.Attributes[PasswordPolicyConstWord.ValueStr].Value.ToLower().Equals(fromSystem.ToLower()))
            //    {
                   var policyXmlElementes = policyXml.DocumentElement.SelectNodes(PasswordPolicyConstWord.FromSystem);



                    foreach (XmlNode fromNode in policyXmlElementes)
                    {
                        PasswordPolicyEntity ppe = new PasswordPolicyEntity();

                        ppe.PasswordFromSystem = fromNode.Attributes[PasswordPolicyConstWord.ValueStr].Value.ToLower();
                        
                        ppe.PasswordDesKeyFile = fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordDesKeyFile).InnerText;
                        ppe.PasswordDesKeyVerSion = int.Parse(fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordDesKeyVerSion).InnerText);
                        ///需要添加值
                        ppe.PasswordFailedCount = int.Parse(fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordFailedCount).InnerText);
                        ppe.PasswordIsSamePassportName = int.Parse(fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordIsSamePassportName).InnerText);
                        ppe.PasswordLastMCount = int.Parse(fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordLastMCount).InnerText);
                        ppe.PasswordMinMDayCount = int.Parse(fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordMinMDayCount).InnerText);
                        ppe.PasswordMixMDayCount = int.Parse(fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordMixMDayCount).InnerText);
                        ppe.PasswordUnlockMinute = int.Parse(fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordUnlockMinute).InnerText);

                        AddKeyIvToDic(fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordDesKeyFile).InnerText,
                            fromNode.SelectSingleNode(PasswordPolicyConstWord.PasswordDesKeyVerSion).InnerText, ppe.PasswordDesDic);

                        SetDicByNodesName(fromNode, ppe.PasswordRuleRegularExpressionDic, PasswordPolicyConstWord.PasswordRuleRegularExpression);

                        SetDicByNodesName(fromNode, ppe.ExPasswordRuleRegularExpressionDic, PasswordPolicyConstWord.ExPasswordRuleRegularExpression);


                        passwordPolicyD.Add(ppe.PasswordFromSystem ,ppe );
                    }
            //    }
            //}


            return passwordPolicyD;

        }

        /// <summary>
        /// 取得调整策略后的字符串
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string GetPasswordPolicyStr(string userName, string password)
        {
            char[] charUserName = userName.ToCharArray();

            char[] charPass = password.ToCharArray();

            IList<char> bbbb = new List<char>();

            int a = charUserName.Length;
            int b = charPass.Length;

            int x = 0;

            if (a <= b)
                x = b;
            else
                x = a;

            for (int i = 0; i < x; i++)
            {
                if (a >= b)
                {
                    bbbb.Add(charUserName[i]);
                    if(i<=b-1)
                    bbbb.Add(charPass[i]);
                }
                else if (a < b)
                {
                    if (i <= a - 1)
                    {
                        bbbb.Add(charUserName[i]);
                        bbbb.Add(charPass[i]);
                    }
                    else
                    {
                        bbbb.Add(charPass[i]);
                    }

                }
            }
            
            
            char[] rrrr = new char[bbbb.Count];

            for (int j = 0; j < bbbb.Count;j++ )
            {
                rrrr[j] = bbbb[j];
            }

            return new string(rrrr);
        }

        /// <summary>
        /// 取得反调整策略后的字符串
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string GetUnPasswordPolicyStr(string userName, string password)
        {
            char[] charUserName = userName.ToCharArray();

            char[] charPass = password.ToCharArray();

            IList<char> bbbb = new List<char>();
            IList<char> cccc = new List<char>();

            int a = charUserName.Length;
            int b = charPass.Length;

            int x = 0;
            int y = 1;

            for (int i = 0; i < charPass.Length; i++)
            {
                if (bbbb.Count != a)
                {
                    bbbb.Add(charPass[x]);
                    x = x + 2;
                    if (y <= b - 1)
                    {
                        cccc.Add(charPass[y]);
                        y = y + 2;
                    }
                }
                else
                {
                   i = x;
                   if (i >= charPass.Length)
                       break;
                   cccc.Add(charPass[i]);
                }

            }
            char[] rrrr = new char[cccc.Count];

            for (int j = 0; j < cccc.Count; j++)
            {
                rrrr[j] = cccc[j];
            }

            return new string(rrrr);
        }

        /// <summary>
        /// 取得调整策略后的字符串
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string GetPasswordPolicyAndStr(string userName, string password)
        {
            byte[] bytesUserName = UnicodeEncoding.UTF8.GetBytes(userName);

            byte[] bytesPass = UnicodeEncoding.UTF8.GetBytes(password);

            IList<byte> bbbb = new List<byte>();


            int a = bytesUserName.Length;
            int b = bytesPass.Length;
            for (int i = 0; i < bytesUserName.Length; i++)
            {
                if (a >= b)
                {
                    if (i == b)
                        break;
                    byte tmpb = byte.Parse((bytesUserName[i] ^ bytesPass[i]).ToString());
                    bbbb.Add(tmpb);
                }
                else if (a < b)
                {

                    //if (i == a - 1)
                    //{
                    //    byte tmpb = byte.Parse((bytesUserName[i] ^ bytesPass[i]).ToString());
                    //    bbbb.Add(tmpb);
                    //    for (int j = 0; j < (b - a); j++)
                    //    {
                    //        if (j < bytesUserName.Length)
                    //        {
                    //            tmpb = byte.Parse((bytesUserName[j] ^ bytesPass[a + j]).ToString());
                    //            bbbb.Add(tmpb);
                    //        }
                    //        else
                    //        {
                    //            bbbb.Add(bytesPass[a + j]);
                    //        }
                    //    }

                    //    break;
                    //}
                    //byte tmpb1 = byte.Parse((bytesUserName[i] ^ bytesPass[i]).ToString());
                    //bbbb.Add(tmpb1);
                    //取余
                    int modi = b % a;
                    //取模
                    int quoi = b / a;

                    int x = 0;

                    if (modi == 0)
                    {
                        x = quoi;
                    }
                    else
                    {
                        x = quoi + 1;
                    }

                    for (int y = 0; y < x; y++)
                    {
                        for (int j = 0; j < a; j++)
                        {
                            if ((a * y + j) < bytesPass.Length)
                            {
                                byte tmpb = byte.Parse((bytesUserName[j] ^ bytesPass[a * y + j]).ToString());
                                bbbb.Add(tmpb);
                            }
                            else
                            {
                                bbbb.Add(bytesUserName[j]);
                            }
                        }
                    }

                    break;


                }
            }

            byte[] rrrr = new byte[bbbb.Count];

            for (int j = bbbb.Count - 1; j >= 0; j--)
            {
                rrrr[j] = bbbb[j];
            }

            string utfstr = UnicodeEncoding.UTF8.GetString(rrrr);

            return utfstr;

            //return utfstr.Trim("\0".ToCharArray());
        }

        /// <summary>
        /// 取得反调整策略后的字符串
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string GetUnPasswordPolicyAndStr(string userName, string password)
        {
            byte[] bytesUserName = UnicodeEncoding.UTF8.GetBytes(userName);

            byte[] bytesPass = UnicodeEncoding.UTF8.GetBytes(password);

            IList<byte> bbbb = new List<byte>();


            int a = bytesUserName.Length;
            int b = bytesPass.Length;
            for (int i = 0; i < bytesUserName.Length; i++)
            {
                if (a >= b)
                {
                    if (i == b)
                        break;
                    byte tmpb = byte.Parse((bytesUserName[i] ^ bytesPass[i]).ToString());
                    bbbb.Add(tmpb);
                }
                else if (a < b)
                {
                    //取余
                    int modi = b % a;
                    //取模
                    int quoi = b / a;

                    int x = 0;

                    if (modi == 0)
                    {
                        x = quoi;
                    }
                    else
                    {
                        x = quoi + 1;
                    }

                    for (int y = 0; y < x; y++)
                    {
                        for (int j = 0; j < a; j++)
                        {
                            if ((a * y + j) < bytesPass.Length)
                            {
                                byte tmpb = byte.Parse((bytesUserName[j] ^ bytesPass[a * y + j]).ToString());
                                bbbb.Add(tmpb);
                            }
                            else
                            {
                                //bbbb.Add(bytesUserName[j]);
                            }
                        }
                    }

                    break;
                    
                    
                    //if (i == a - 1)
                    //{
                    //    byte tmpb = byte.Parse((bytesUserName[i] ^ bytesPass[i]).ToString());
                    //    bbbb.Add(tmpb);

                    //    //取余
                    //    int modi = b % a;
                    //    //取模
                    //    int quoi = b / a;

                    //    int x = 0;

                    //    if (modi == 0)
                    //    {
                    //        x = quoi;
                    //    }
                    //    else
                    //    {
                    //        x = quoi + 1;
                    //    }

                    //    for (int y = 0; y < x; y++)
                    //    {
                    //        for (int j = 0; j < a; j++)
                    //        {
                    //            if ((a*y + j) < bytesPass.Length)
                    //            {
                    //                tmpb = byte.Parse((bytesUserName[j] ^ bytesPass[a + j]).ToString());
                    //                bbbb.Add(tmpb);
                    //            }
                    //            else
                    //            {
                    //                bbbb.Add(bytesUserName[a + j]);
                    //            }
                    //        }
                    //    }

                    //    break;
                    //}
                    //byte tmpb1 = byte.Parse((bytesUserName[i] ^ bytesPass[i]).ToString());
                    //bbbb.Add(tmpb1);

                }
            }

            byte[] rrrr = new byte[bbbb.Count];

            for (int j = bbbb.Count - 1; j >= 0; j--)
            {
                rrrr[j] = bbbb[j];
            }

            string utfstr = UnicodeEncoding.UTF8.GetString(rrrr);

            return utfstr.Trim("\0".ToCharArray()); ;
        }

        /// <summary>
        /// 根据文件名字和KeyVersion取得文件记录的密码保存Key
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="keyVersion"></param>
        /// <returns></returns>
        public static string[] GetPasswordKeyFileKeyIv(string fileName, string keyVersion)
        {
            //打开文件并显示其内容 
            StreamReader reader = null;
            if (string.IsNullOrEmpty(fileName))
            {
                return new string[] { };
            }

            try
            {
                //加入资源读入方式
                reader = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(@PasswordPolicyConstWord.PolicyKeyRFilePath));
                string lineStr = string.Empty;
                string[] lineStrs = new string[] { };
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    lineStrs = line.Split(";".ToCharArray());
                    if (lineStrs.Length < 0)
                        continue;
                    if (lineStrs[0].ToLower().Trim().Equals(keyVersion.ToLower().Trim()))
                    {
                        break;
                    }
                }
                if (lineStrs.Length <= 0)
                    return new string[] { };
                string[] result = new string[2];
                result[0] = lineStrs[2];
                result[1] = lineStrs[4];

                return result;
            }
            catch (IOException e)
            {
                Trace.TraceError("File Open Failed: {0} : {1} ", fileName, e.ToString());
                return new string[] { };
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        /// <summary>
        /// 自定义正则表达式验证
        /// </summary>
        /// <param name="valueStr"></param>
        /// <param name="validationExpression"></param>
        /// <returns></returns>
        public static bool IsFormatValidate(string valueStr, string validationExpression)
        {
            #region 自定义正则表达式验证

            Regex matchPattern = new Regex(validationExpression);
            Match match = matchPattern.Match(valueStr);
            if (match.Success)
            {
                return true;
            }
            return false;

            #endregion
        }

        /// <summary>
        /// 取得加密字符串(防止乱码)
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string GetEncryptPassword(string strValue)
        {
            #region 取得加密字符串

            //加密实例取得
            IAlgorithm algorithm = AlgorithmManager.GetAlgorithm(AlgorithmType.MD5);

            //先返回加密字符
            string strEncryptText = algorithm.EncryptTextToMemory(strValue);

            //返回加密字符
            return strEncryptText;

            #endregion
        }

        /// <summary>
        /// 判断输入字符串是否都是
        /// </summary>
        /// <param name="passwordStr"></param>
        /// <returns></returns>
        public static bool IsAllSameCharInStr(string passwordStr)
        {
            #region 判断输入字符串是否都是

            if (string.IsNullOrEmpty(passwordStr))
            {
                return true;
            }

            int strL = passwordStr.Length;
            int sameInt = 0;

            char[] passwordChars = passwordStr.ToCharArray();

            char tmpOne = passwordChars[0];

            for (int i = 0; i < strL; i++)
            {
                if (tmpOne.Equals(passwordChars[i]))
                    sameInt = sameInt + 1;
            }

            if (sameInt == strL)
                return true;

            return false;

            #endregion
        }

        /// <summary>
        /// 判断输入字符串是否有排除的特殊字符
        /// </summary>
        /// <param name="passwordStr">密码</param>
        /// <returns></returns>
        public static bool IsSpecialCharInStr(string passwordStr)
        {
            #region 判断输入字符串是否有排除的特殊字符

            if (string.IsNullOrEmpty(passwordStr))
            {
                return true;
            }

            int strL = passwordStr.Length;

            char[] passwordChars = passwordStr.ToCharArray();

            //’@，&，-“<

            char[] specialChar = new char[]{'"','\'','@','-','&','<'};

            for (int i = 0; i < strL; i++)
            {
                char tmpstr = passwordChars[i];
                
                for (int j = 0; j < specialChar.Length; j++)
                {
                    char tmp = specialChar[j];

                    if (tmp.Equals(tmpstr))
                        return true;
                }

            }

            return false;

            #endregion
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 将从文件里取得的Key加入到字典里
        /// </summary>
        /// <param name="fileName">文件夹名</param>
        /// <param name="keyVersion">Key版本</param>
        private static void AddKeyIvToDic(string fileName, string keyVersion, IDictionary<string, string> dic)
        {
            #region 将从文件里取得的Key加入到字典里

            //读文件名字取文件中的Key和IV
            string[] keys = GetPasswordKeyFileKeyIv(@fileName, keyVersion);

            if (keys.Length != 0)
            {
                dic.Add(PasswordPolicyConstWord.PasswordDesKey, keys[0]);
                dic.Add(PasswordPolicyConstWord.PasswordDesKeyIv, keys[1]);
            }
            else
            {
                dic.Add(PasswordPolicyConstWord.PasswordDesKey, string.Empty);
                dic.Add(PasswordPolicyConstWord.PasswordDesKeyIv, string.Empty);
            }

            #endregion
        }

        /// <summary>
        /// 取节点值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetNodeAttributesValue(XmlNode node, string key)
        {
            if (node.Attributes[key] == null)
            {
                return string.Empty;
            }
            else
            {
                return node.Attributes[key].Value;
            }
        }

        /// <summary>
        /// 根据节点名称设置字典
        /// </summary>
        /// <param name="fromNode"></param>
        /// <param name="passwordPolicyD"></param>
        /// <param name="nodesName"></param>
        private static void SetDicByNodesName(XmlNode fromNode, IDictionary<string, string> passwordPolicyD,string nodesName)
        {
            int i = 1;
            
            foreach (XmlNode subNode in fromNode.SelectNodes(nodesName))
            {
                if (subNode.Attributes[PasswordPolicyConstWord.ValueStr].Value == i.ToString())
                {
                    passwordPolicyD.Add(nodesName + i, subNode.InnerText);
                }

                i = i + 1;
            }
        }

        #endregion
    }

    /// <summary>
    /// 常量定义
    /// </summary>
    public class PasswordPolicyConstWord
    {
        /// <summary>
        /// 系统节点名
        /// </summary>
        public static readonly string FromSystem = "FromSystem";
        /// <summary>
        /// 密码Des加密Key保存文件名
        /// </summary>
        public static readonly string PasswordDesKeyFile = "PasswordDesKeyFile";

        /// <summary>
        /// 密码强度表达式
        /// </summary>
        public static readonly string PasswordRuleRegularExpression = "PasswordRuleRegularExpression";

        /// <summary>
        /// 一些例外的密码强度表达式
        /// </summary>
        public static readonly string ExPasswordRuleRegularExpression = "ExPasswordRuleRegularExpression";

        /// <summary>
        /// 变量名
        /// </summary>
        public static readonly string ValueStr = "value";

        /// <summary>
        /// DesKey版本
        /// </summary>
        public static readonly string PasswordDesKeyVerSion = "PasswordDesKeyVerSion";

        /// <summary>
        /// DesKey
        /// </summary>
        public static readonly string PasswordDesKey = "PasswordDesKey";

        /// <summary>
        /// DesKeyIv
        /// </summary>
        public static readonly string PasswordDesKeyIv = "PasswordDesKeyIv";

        /// <summary>
        /// 密码策略
        /// </summary>
        public static readonly string PasswordPolicy = "PasswordPolicy";

        /// <summary>
        /// 密码策略文件名
        /// </summary>
        public static readonly string PasswordPolicyFileName = "PasswordPolicyFileName";

        /// <summary>
        /// default字符串
        /// </summary>
        public static readonly string DefaultStr = "default";

        /// <summary>
        /// Zero字符串
        /// </summary>
        public static readonly string ZeroStr = "0";

        /// <summary>
        /// 密码策略文件路径（嵌入资源）
        /// </summary>
        public static readonly string PolicyConfigRFilePath = "PasswordPolicy.Config.PasswordPolicyConfig.xml";

        /// <summary>
        /// 密码策略Key文件路径（嵌入资源）
        /// </summary>
        public static readonly string PolicyKeyRFilePath = "PasswordPolicy.Config.PasswordDesKeyFile.txt";

        /// <summary>
        /// 解锁时间
        /// </summary>
        public static readonly string PasswordUnlockMinute = "PasswordUnlockMinute";

        /// <summary>
        /// 登录失败次数
        /// </summary>
        public static readonly string PasswordFailedCount = "PasswordFailedCount";
        
        /// <summary>
        /// 密码最后登录次数
        /// </summary>
        public static readonly string PasswordLastMCount = "PasswordLastMCount";

        /// <summary>
        /// 密码最少修改时间
        /// </summary>
        public static readonly string PasswordMinMDayCount = "PasswordMinMDayCount";

        /// <summary>
        /// 密码最大修改时间
        /// </summary>
        public static readonly string PasswordMixMDayCount="PasswordMixMDayCount";

        /// <summary>
        /// 密码是否和用户名重名
        /// </summary>
        public static readonly string PasswordIsSamePassportName = "PasswordIsSamePassportName";
    }
}