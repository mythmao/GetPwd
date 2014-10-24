using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using PasswordPolicy;

namespace GetPwdForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string ConnStr = ConfigurationManager.ConnectionStrings["CloudPassport"].ConnectionString;
        PasswordUtility passwordUtility = PasswordUtility.GetInstance();

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string mobile = tbMobile.Text.Trim();
            if (string.IsNullOrEmpty(mobile))
            {
                MessageBox.Show("请输入手机号");
                return;
            }
            List<Users> userList = GetUsersByMobile(mobile);
            if (userList.Count <= 0)
            {
                MessageBox.Show("没有找到记录");
                return;
            }
            StringBuilder sb=new StringBuilder();
            foreach (Users user in userList)
            {
                sb.AppendLine(user.UserName + "  " + GetPassword(user));
            }
            tbPwd.Text = sb.ToString();
        }
        private void btnName_Click(object sender, EventArgs e)
        {
            string name = tbName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请输入姓名");
                return;
            }
            List<Users> userList = GetUsersByName(name);
            if (userList.Count <= 0)
            {
                MessageBox.Show("没有找到记录");
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach (Users user in userList)
            {
                sb.AppendLine(user.UserName + "  " + GetPassword(user));
            }
            tbPwd.Text = sb.ToString();
        }
        private void btnEmail_Click(object sender, EventArgs e)
        {
            string email = tbEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("请输入Email");
                return;
            }
            List<Users> userList = GetUsersByEmail(email);
            if (userList.Count <= 0)
            {
                MessageBox.Show("没有找到记录");
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach (Users user in userList)
            {
                sb.AppendLine(user.UserName + "  " + GetPassword(user));
            }
            tbPwd.Text = sb.ToString();
        }
        /// <summary>
        /// 根据手机号查找用户
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public List<Users> GetUsersByMobile(string mobile)
        {
            List<Users> list = new List<Users>();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string sqlStr = "select * from [Users] where [Mobile]='" + mobile + "'";
                    cmd.CommandText = sqlStr;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                Users user = new Users();
                                user.UserID = Convert.ToInt64(dr["UserID"]);
                                user.UserName = dr["UserName"].ToString();
                                user.Mobile = dr["Mobile"].ToString();
                                user.RealName = dr["RealName"].ToString();
                                list.Add(user);
                            }
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 根据姓名查找用户
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        public List<Users> GetUsersByName(string name)
        {
            List<Users> list = new List<Users>();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string sqlStr = "select * from [Users] where [RealName]='" + name + "'";
                    cmd.CommandText = sqlStr;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                Users user = new Users();
                                user.UserID = Convert.ToInt64(dr["UserID"]);
                                user.UserName = dr["UserName"].ToString();
                                user.Mobile = dr["Mobile"].ToString();
                                user.RealName = dr["RealName"].ToString();
                                list.Add(user);
                            }
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 根据Email查询用户
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns></returns>
        public List<Users> GetUsersByEmail(string email)
        {
            List<Users> list = new List<Users>();
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string sqlStr = "select * from [Users] where [UserName]='" + email + "'";
                    cmd.CommandText = sqlStr;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                Users user = new Users();
                                user.UserID = Convert.ToInt64(dr["UserID"]);
                                user.UserName = dr["UserName"].ToString();
                                user.Mobile = dr["Mobile"].ToString();
                                user.RealName = dr["RealName"].ToString();
                                list.Add(user);
                            }
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetPassword(Users user)
        {
            string pwd = "";
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string sqlStr = "select [KeyPassword] from [PassportProfile] where UserID=" + user.UserID;
                    cmd.CommandText = sqlStr;
                    object obj = cmd.ExecuteScalar();
                    if (obj != null)
                    {
                        string key = obj.ToString();
                        pwd = passwordUtility.UnEncryptPasswordByDES(key, user.UserName);
                    }
                    
                }
            }
            return pwd;
        }

        

        
    }
}
