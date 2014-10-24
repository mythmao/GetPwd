using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordPolicy
{
    /// <summary>
    /// ����ϵͳ������Զ���
    /// </summary>
    public class PasswordPolicyEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PasswordPolicyEntity()
        {
            PasswordRuleRegularExpressionDic = new Dictionary<string, string>();
            ExPasswordRuleRegularExpressionDic = new Dictionary<string, string>();
            PasswordDesDic = new Dictionary<string, string>();
        }
        
        /// <summary>
        /// �������ϵͳ��ʾ
        /// </summary>
        public string PasswordFromSystem
        {
            get;
            set;
        }
        
        /// <summary>
        /// ��������Ա��ʽ�ֵ�
        /// </summary>
        public IDictionary<string, string> PasswordRuleRegularExpressionDic
        {
            get;
            set;
        }

        /// <summary>
        /// �����������չ���ʽ�ֵ�
        /// </summary>
        public IDictionary<string, string> ExPasswordRuleRegularExpressionDic
        {
            get;
            set;
        }

        /// <summary>
        /// ����DES�ļ�ֵ�Ͱ汾
        /// </summary>
        public IDictionary<string, string> PasswordDesDic
        {
            get;
            set;
        }

        /// <summary>
        /// ��������ı��ļ�
        /// </summary>
        public string PasswordDesKeyFile
        {
            get;
            set;
        }

        /// <summary>
        /// ����DES���ܰ汾
        /// </summary>
        public int PasswordDesKeyVerSion
        {
            get;
            set;
        }

        /// <summary>
        /// �������ʱ�䣨���ӣ�
        /// </summary>
        public int  PasswordUnlockMinute
        {
            get;
            set;
        }
        
        /// <summary>
        /// ��¼�����������ʧ�ܵĴ���
        /// </summary>
        public int PasswordFailedCount
        {
            get;
            set;
        }

        /// <summary>
        /// ��������޸ļ�¼�Ĵ���
        /// </summary>
        public int PasswordLastMCount
        {
            get;
            set;
        }


        /// <summary>
        /// ���������޸�����
        /// </summary>
        public  int PasswordMinMDayCount
        {
            get;
            set;
        }

        /// <summary>
        /// ��������޸�����
        /// </summary>
        public int PasswordMixMDayCount
        {
            get;
            set;
        }

        /// <summary>
        /// �����Ƿ���û���һ��
        /// 0-����һ�»��߲����
        /// 1-Ҫ��鲻��һ��
        /// 2-Ҫ����û�Eamil�Ƿ�Ҳһ��
        /// </summary>
        public int PasswordIsSamePassportName
        {
            get;
            set;
        }
    }

    
}