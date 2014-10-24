using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace PasswordPolicy
{
    /// <summary>
    /// 用户表,用户ID作为分区字段,代表一个物理上存在的“人”
///abcd
    /// </summary>
    [Serializable]
    public  partial class Users
    {

        #region 私有成员


        // 用户ID，分区字段起始值1，步长10000
        long _UserID;

        // 用户名称,“人”的昵称，可更改，全局唯一
        string _UserName;

        // 手机号，和CloudCustomer..Mobile同步，或许会废弃，用于在手机登录时，验证家长或者学生的手机号码
        string _Mobile;

        // 用户真实名称,“人“的真实姓名，可更改，可重复
        string _RealName;

        // 用户建立时间
        DateTime _CreateDate;

        // 用户更新时间
        DateTime _UpdateDate;

        // 用户最后一次登录时间
        DateTime _LastLoginDate;

        // 用户登录次数，应为所有Passport登录次数总和
        int _LoginNumber;

        // 用户从哪个子系统过来的
        string _FromSystem;

        // 子系统的UserID
        long _FromSysID;

        // 是否允许更改用户名0－否，1－是
        bool _IsAllowModName;

        // 用户登录状态，0－未登录，1－登录，2－休眠,-1-删除标记---------------------0-表示用户启用1-表示用户禁用
        byte _UserState;

        // 用户语言
        string _UserLanguage;

        // 是否是Guest用户
        bool _IsGuestUser;

        // 用户样式ID
        long _StyleID;

        // 记录Guest用户和注册用户的关系
        long _GuestToRegisterID;

        // 用户昵称
        string _NickName;

        #endregion

        #region 公共属性

        /// <summary>
        /// 用户ID，分区字段起始值1，步长10000
        /// </summary>
        [DisplayName("UserID【long】")]
        public long UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        /// <summary>
        /// 用户名称,“人”的昵称，可更改，全局唯一
        /// </summary>
        [DisplayName("UserName【string】")]
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        /// <summary>
        /// 手机号，和CloudCustomer..Mobile同步，或许会废弃，用于在手机登录时，验证家长或者学生的手机号码
        /// </summary>
        [DisplayName("Mobile【string】")]
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }

        /// <summary>
        /// 用户真实名称,“人“的真实姓名，可更改，可重复
        /// </summary>
        [DisplayName("RealName【string】")]
        public string RealName
        {
            get { return _RealName; }
            set { _RealName = value; }
        }

        /// <summary>
        /// 用户建立时间
        /// </summary>
        [DisplayName("CreateDate【DateTime】")]
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        /// <summary>
        /// 用户更新时间
        /// </summary>
        [DisplayName("UpdateDate【DateTime】")]
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        /// <summary>
        /// 用户最后一次登录时间
        /// </summary>
        [DisplayName("LastLoginDate【DateTime】")]
        public DateTime LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }

        /// <summary>
        /// 用户登录次数，应为所有Passport登录次数总和
        /// </summary>
        [DisplayName("LoginNumber【int】")]
        public int LoginNumber
        {
            get { return _LoginNumber; }
            set { _LoginNumber = value; }
        }

        /// <summary>
        /// 用户从哪个子系统过来的
        /// </summary>
        [DisplayName("FromSystem【string】")]
        public string FromSystem
        {
            get { return _FromSystem; }
            set { _FromSystem = value; }
        }

        /// <summary>
        /// 子系统的UserID
        /// </summary>
        [DisplayName("FromSysID【long】")]
        public long FromSysID
        {
            get { return _FromSysID; }
            set { _FromSysID = value; }
        }

        /// <summary>
        /// 是否允许更改用户名0－否，1－是
        /// </summary>
        [DisplayName("IsAllowModName【bool】")]
        public bool IsAllowModName
        {
            get { return _IsAllowModName; }
            set { _IsAllowModName = value; }
        }

        /// <summary>
        /// 用户登录状态，0－未登录，1－登录，2－休眠,-1-删除标记---------------------0-表示用户启用1-表示用户禁用
        /// </summary>
        [DisplayName("UserState【byte】")]
        public byte UserState
        {
            get { return _UserState; }
            set { _UserState = value; }
        }

        /// <summary>
        /// 用户语言
        /// </summary>
        [DisplayName("UserLanguage【string】")]
        public string UserLanguage
        {
            get { return _UserLanguage; }
            set { _UserLanguage = value; }
        }

        /// <summary>
        /// 是否是Guest用户
        /// </summary>
        [DisplayName("IsGuestUser【bool】")]
        public bool IsGuestUser
        {
            get { return _IsGuestUser; }
            set { _IsGuestUser = value; }
        }

        /// <summary>
        /// 用户样式ID
        /// </summary>
        [DisplayName("StyleID【long】")]
        public long StyleID
        {
            get { return _StyleID; }
            set { _StyleID = value; }
        }

        /// <summary>
        /// 记录Guest用户和注册用户的关系
        /// </summary>
        [DisplayName("GuestToRegisterID【long】")]
        public long GuestToRegisterID
        {
            get { return _GuestToRegisterID; }
            set { _GuestToRegisterID = value; }
        }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [DisplayName("NickName【string】")]
        public string NickName
        {
            get { return _NickName; }
            set { _NickName = value; }
        }


        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Users()
        {
            this._UserID=0;
            this._GuestToRegisterID=0;

        }

        #endregion

        

        


    }
}
