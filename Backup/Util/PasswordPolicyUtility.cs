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
    /// ��ȡ��������ļ���
    /// </summary>
    public class PasswordPolicyUtility
    {
        #region ������̬����

        /// <summary>
        /// �������ý��趨����
        /// </summary>
        /// <param name="xmlFileName">�ļ�����</param>
        /// <param name="fromSystem">ϵͳ���ýڿ���</param>
        /// <returns></returns>
        public static IDictionary<string, string> GetPasswordPolicyXml(string xmlFileName, string fromSystem)
        {
            #region �������ý��趨����

            var passwordPolicyD = new Dictionary<string, string>();

            var policyXml = new XmlDocument();

            //�޸ĳɶ�ȡǨ����Դ
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

                    //��Ҫ���ֵ
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
        /// �������ý��趨����
        /// </summary>
        /// <param name="xmlFileName">�ļ�����</param>
        /// <returns></returns>
        public static IDictionary<string, PasswordPolicyEntity> GetPasswordPolicyXmlAll(string xmlFileName)
        {
            var passwordPolicyD = new Dictionary<string, PasswordPolicyEntity>();

            var policyXml = new XmlDocument();

            //�޸ĳɶ�ȡǨ����Դ
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
                        ///��Ҫ���ֵ
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
        /// ȡ�õ������Ժ���ַ���
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="password">����</param>
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
        /// ȡ�÷��������Ժ���ַ���
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="password">����</param>
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
        /// ȡ�õ������Ժ���ַ���
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="password">����</param>
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
                    //ȡ��
                    int modi = b % a;
                    //ȡģ
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
        /// ȡ�÷��������Ժ���ַ���
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="password">����</param>
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
                    //ȡ��
                    int modi = b % a;
                    //ȡģ
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

                    //    //ȡ��
                    //    int modi = b % a;
                    //    //ȡģ
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
        /// �����ļ����ֺ�KeyVersionȡ���ļ���¼�����뱣��Key
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="keyVersion"></param>
        /// <returns></returns>
        public static string[] GetPasswordKeyFileKeyIv(string fileName, string keyVersion)
        {
            //���ļ�����ʾ������ 
            StreamReader reader = null;
            if (string.IsNullOrEmpty(fileName))
            {
                return new string[] { };
            }

            try
            {
                //������Դ���뷽ʽ
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
        /// �Զ���������ʽ��֤
        /// </summary>
        /// <param name="valueStr"></param>
        /// <param name="validationExpression"></param>
        /// <returns></returns>
        public static bool IsFormatValidate(string valueStr, string validationExpression)
        {
            #region �Զ���������ʽ��֤

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
        /// ȡ�ü����ַ���(��ֹ����)
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static string GetEncryptPassword(string strValue)
        {
            #region ȡ�ü����ַ���

            //����ʵ��ȡ��
            IAlgorithm algorithm = AlgorithmManager.GetAlgorithm(AlgorithmType.MD5);

            //�ȷ��ؼ����ַ�
            string strEncryptText = algorithm.EncryptTextToMemory(strValue);

            //���ؼ����ַ�
            return strEncryptText;

            #endregion
        }

        /// <summary>
        /// �ж������ַ����Ƿ���
        /// </summary>
        /// <param name="passwordStr"></param>
        /// <returns></returns>
        public static bool IsAllSameCharInStr(string passwordStr)
        {
            #region �ж������ַ����Ƿ���

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
        /// �ж������ַ����Ƿ����ų��������ַ�
        /// </summary>
        /// <param name="passwordStr">����</param>
        /// <returns></returns>
        public static bool IsSpecialCharInStr(string passwordStr)
        {
            #region �ж������ַ����Ƿ����ų��������ַ�

            if (string.IsNullOrEmpty(passwordStr))
            {
                return true;
            }

            int strL = passwordStr.Length;

            char[] passwordChars = passwordStr.ToCharArray();

            //��@��&��-��<

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

        #region ˽�з���

        /// <summary>
        /// �����ļ���ȡ�õ�Key���뵽�ֵ���
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="keyVersion">Key�汾</param>
        private static void AddKeyIvToDic(string fileName, string keyVersion, IDictionary<string, string> dic)
        {
            #region �����ļ���ȡ�õ�Key���뵽�ֵ���

            //���ļ�����ȡ�ļ��е�Key��IV
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
        /// ȡ�ڵ�ֵ
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
        /// ���ݽڵ����������ֵ�
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
    /// ��������
    /// </summary>
    public class PasswordPolicyConstWord
    {
        /// <summary>
        /// ϵͳ�ڵ���
        /// </summary>
        public static readonly string FromSystem = "FromSystem";
        /// <summary>
        /// ����Des����Key�����ļ���
        /// </summary>
        public static readonly string PasswordDesKeyFile = "PasswordDesKeyFile";

        /// <summary>
        /// ����ǿ�ȱ��ʽ
        /// </summary>
        public static readonly string PasswordRuleRegularExpression = "PasswordRuleRegularExpression";

        /// <summary>
        /// һЩ���������ǿ�ȱ��ʽ
        /// </summary>
        public static readonly string ExPasswordRuleRegularExpression = "ExPasswordRuleRegularExpression";

        /// <summary>
        /// ������
        /// </summary>
        public static readonly string ValueStr = "value";

        /// <summary>
        /// DesKey�汾
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
        /// �������
        /// </summary>
        public static readonly string PasswordPolicy = "PasswordPolicy";

        /// <summary>
        /// ��������ļ���
        /// </summary>
        public static readonly string PasswordPolicyFileName = "PasswordPolicyFileName";

        /// <summary>
        /// default�ַ���
        /// </summary>
        public static readonly string DefaultStr = "default";

        /// <summary>
        /// Zero�ַ���
        /// </summary>
        public static readonly string ZeroStr = "0";

        /// <summary>
        /// ��������ļ�·����Ƕ����Դ��
        /// </summary>
        public static readonly string PolicyConfigRFilePath = "PasswordPolicy.Config.PasswordPolicyConfig.xml";

        /// <summary>
        /// �������Key�ļ�·����Ƕ����Դ��
        /// </summary>
        public static readonly string PolicyKeyRFilePath = "PasswordPolicy.Config.PasswordDesKeyFile.txt";

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public static readonly string PasswordUnlockMinute = "PasswordUnlockMinute";

        /// <summary>
        /// ��¼ʧ�ܴ���
        /// </summary>
        public static readonly string PasswordFailedCount = "PasswordFailedCount";
        
        /// <summary>
        /// ��������¼����
        /// </summary>
        public static readonly string PasswordLastMCount = "PasswordLastMCount";

        /// <summary>
        /// ���������޸�ʱ��
        /// </summary>
        public static readonly string PasswordMinMDayCount = "PasswordMinMDayCount";

        /// <summary>
        /// ��������޸�ʱ��
        /// </summary>
        public static readonly string PasswordMixMDayCount="PasswordMixMDayCount";

        /// <summary>
        /// �����Ƿ���û�������
        /// </summary>
        public static readonly string PasswordIsSamePassportName = "PasswordIsSamePassportName";
    }
}