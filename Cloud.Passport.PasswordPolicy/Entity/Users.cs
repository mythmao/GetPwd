using System;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace PasswordPolicy
{
    /// <summary>
    /// �û���,�û�ID��Ϊ�����ֶ�,����һ�������ϴ��ڵġ��ˡ�
///abcd
    /// </summary>
    [Serializable]
    public  partial class Users
    {

        #region ˽�г�Ա


        // �û�ID�������ֶ���ʼֵ1������10000
        long _UserID;

        // �û�����,���ˡ����ǳƣ��ɸ��ģ�ȫ��Ψһ
        string _UserName;

        // �ֻ��ţ���CloudCustomer..Mobileͬ���������������������ֻ���¼ʱ����֤�ҳ�����ѧ�����ֻ�����
        string _Mobile;

        // �û���ʵ����,���ˡ�����ʵ�������ɸ��ģ����ظ�
        string _RealName;

        // �û�����ʱ��
        DateTime _CreateDate;

        // �û�����ʱ��
        DateTime _UpdateDate;

        // �û����һ�ε�¼ʱ��
        DateTime _LastLoginDate;

        // �û���¼������ӦΪ����Passport��¼�����ܺ�
        int _LoginNumber;

        // �û����ĸ���ϵͳ������
        string _FromSystem;

        // ��ϵͳ��UserID
        long _FromSysID;

        // �Ƿ���������û���0����1����
        bool _IsAllowModName;

        // �û���¼״̬��0��δ��¼��1����¼��2������,-1-ɾ�����---------------------0-��ʾ�û�����1-��ʾ�û�����
        byte _UserState;

        // �û�����
        string _UserLanguage;

        // �Ƿ���Guest�û�
        bool _IsGuestUser;

        // �û���ʽID
        long _StyleID;

        // ��¼Guest�û���ע���û��Ĺ�ϵ
        long _GuestToRegisterID;

        // �û��ǳ�
        string _NickName;

        #endregion

        #region ��������

        /// <summary>
        /// �û�ID�������ֶ���ʼֵ1������10000
        /// </summary>
        [DisplayName("UserID��long��")]
        public long UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        /// <summary>
        /// �û�����,���ˡ����ǳƣ��ɸ��ģ�ȫ��Ψһ
        /// </summary>
        [DisplayName("UserName��string��")]
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        /// <summary>
        /// �ֻ��ţ���CloudCustomer..Mobileͬ���������������������ֻ���¼ʱ����֤�ҳ�����ѧ�����ֻ�����
        /// </summary>
        [DisplayName("Mobile��string��")]
        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }

        /// <summary>
        /// �û���ʵ����,���ˡ�����ʵ�������ɸ��ģ����ظ�
        /// </summary>
        [DisplayName("RealName��string��")]
        public string RealName
        {
            get { return _RealName; }
            set { _RealName = value; }
        }

        /// <summary>
        /// �û�����ʱ��
        /// </summary>
        [DisplayName("CreateDate��DateTime��")]
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        /// <summary>
        /// �û�����ʱ��
        /// </summary>
        [DisplayName("UpdateDate��DateTime��")]
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        /// <summary>
        /// �û����һ�ε�¼ʱ��
        /// </summary>
        [DisplayName("LastLoginDate��DateTime��")]
        public DateTime LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }

        /// <summary>
        /// �û���¼������ӦΪ����Passport��¼�����ܺ�
        /// </summary>
        [DisplayName("LoginNumber��int��")]
        public int LoginNumber
        {
            get { return _LoginNumber; }
            set { _LoginNumber = value; }
        }

        /// <summary>
        /// �û����ĸ���ϵͳ������
        /// </summary>
        [DisplayName("FromSystem��string��")]
        public string FromSystem
        {
            get { return _FromSystem; }
            set { _FromSystem = value; }
        }

        /// <summary>
        /// ��ϵͳ��UserID
        /// </summary>
        [DisplayName("FromSysID��long��")]
        public long FromSysID
        {
            get { return _FromSysID; }
            set { _FromSysID = value; }
        }

        /// <summary>
        /// �Ƿ���������û���0����1����
        /// </summary>
        [DisplayName("IsAllowModName��bool��")]
        public bool IsAllowModName
        {
            get { return _IsAllowModName; }
            set { _IsAllowModName = value; }
        }

        /// <summary>
        /// �û���¼״̬��0��δ��¼��1����¼��2������,-1-ɾ�����---------------------0-��ʾ�û�����1-��ʾ�û�����
        /// </summary>
        [DisplayName("UserState��byte��")]
        public byte UserState
        {
            get { return _UserState; }
            set { _UserState = value; }
        }

        /// <summary>
        /// �û�����
        /// </summary>
        [DisplayName("UserLanguage��string��")]
        public string UserLanguage
        {
            get { return _UserLanguage; }
            set { _UserLanguage = value; }
        }

        /// <summary>
        /// �Ƿ���Guest�û�
        /// </summary>
        [DisplayName("IsGuestUser��bool��")]
        public bool IsGuestUser
        {
            get { return _IsGuestUser; }
            set { _IsGuestUser = value; }
        }

        /// <summary>
        /// �û���ʽID
        /// </summary>
        [DisplayName("StyleID��long��")]
        public long StyleID
        {
            get { return _StyleID; }
            set { _StyleID = value; }
        }

        /// <summary>
        /// ��¼Guest�û���ע���û��Ĺ�ϵ
        /// </summary>
        [DisplayName("GuestToRegisterID��long��")]
        public long GuestToRegisterID
        {
            get { return _GuestToRegisterID; }
            set { _GuestToRegisterID = value; }
        }

        /// <summary>
        /// �û��ǳ�
        /// </summary>
        [DisplayName("NickName��string��")]
        public string NickName
        {
            get { return _NickName; }
            set { _NickName = value; }
        }


        #endregion

        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        public Users()
        {
            this._UserID=0;
            this._GuestToRegisterID=0;

        }

        #endregion

        

        


    }
}
