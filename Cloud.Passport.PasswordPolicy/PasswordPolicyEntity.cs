using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordPolicy
{
    /// <summary>
    /// 各个系统密码策略对象
    /// </summary>
    public class PasswordPolicyEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PasswordPolicyEntity()
        {
            PasswordRuleRegularExpressionDic = new Dictionary<string, string>();
            ExPasswordRuleRegularExpressionDic = new Dictionary<string, string>();
            PasswordDesDic = new Dictionary<string, string>();
        }
        
        /// <summary>
        /// 密码策略系统标示
        /// </summary>
        public string PasswordFromSystem
        {
            get;
            set;
        }
        
        /// <summary>
        /// 密码检查策略表达式字典
        /// </summary>
        public IDictionary<string, string> PasswordRuleRegularExpressionDic
        {
            get;
            set;
        }

        /// <summary>
        /// 密码检查策略扩展表达式字典
        /// </summary>
        public IDictionary<string, string> ExPasswordRuleRegularExpressionDic
        {
            get;
            set;
        }

        /// <summary>
        /// 密码DES的键值和版本
        /// </summary>
        public IDictionary<string, string> PasswordDesDic
        {
            get;
            set;
        }

        /// <summary>
        /// 密码解密文本文件
        /// </summary>
        public string PasswordDesKeyFile
        {
            get;
            set;
        }

        /// <summary>
        /// 密码DES加密版本
        /// </summary>
        public int PasswordDesKeyVerSion
        {
            get;
            set;
        }

        /// <summary>
        /// 密码解锁时间（分钟）
        /// </summary>
        public int  PasswordUnlockMinute
        {
            get;
            set;
        }
        
        /// <summary>
        /// 登录密码输入错误失败的次数
        /// </summary>
        public int PasswordFailedCount
        {
            get;
            set;
        }

        /// <summary>
        /// 密码最后修改记录的次数
        /// </summary>
        public int PasswordLastMCount
        {
            get;
            set;
        }


        /// <summary>
        /// 密码最少修改天数
        /// </summary>
        public  int PasswordMinMDayCount
        {
            get;
            set;
        }

        /// <summary>
        /// 密码最多修改天数
        /// </summary>
        public int PasswordMixMDayCount
        {
            get;
            set;
        }

        /// <summary>
        /// 密码是否和用户名一致
        /// 0-可以一致或者不检查
        /// 1-要检查不能一致
        /// 2-要检查用户Eamil是否也一致
        /// </summary>
        public int PasswordIsSamePassportName
        {
            get;
            set;
        }
    }

    
}