namespace GetPwdForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMobile = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnName = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.btnEmail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "请输入手机号：";
            // 
            // tbMobile
            // 
            this.tbMobile.Location = new System.Drawing.Point(106, 10);
            this.tbMobile.Name = "tbMobile";
            this.tbMobile.Size = new System.Drawing.Size(129, 21);
            this.tbMobile.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(13, 100);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "手机查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码：";
            // 
            // tbPwd
            // 
            this.tbPwd.Location = new System.Drawing.Point(15, 166);
            this.tbPwd.Multiline = true;
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPwd.Size = new System.Drawing.Size(311, 153);
            this.tbPwd.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "请输入姓名：";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(106, 38);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(129, 21);
            this.tbName.TabIndex = 7;
            // 
            // btnName
            // 
            this.btnName.Location = new System.Drawing.Point(95, 100);
            this.btnName.Name = "btnName";
            this.btnName.Size = new System.Drawing.Size(75, 23);
            this.btnName.TabIndex = 8;
            this.btnName.Text = "姓名查询";
            this.btnName.UseVisualStyleBackColor = true;
            this.btnName.Click += new System.EventHandler(this.btnName_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "请输入Email：";
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(106, 66);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(129, 21);
            this.tbEmail.TabIndex = 10;
            // 
            // btnEmail
            // 
            this.btnEmail.Location = new System.Drawing.Point(176, 100);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(75, 23);
            this.btnEmail.TabIndex = 11;
            this.btnEmail.Text = "Email查询";
            this.btnEmail.UseVisualStyleBackColor = true;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 344);
            this.Controls.Add(this.btnEmail);
            this.Controls.Add(this.tbEmail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnName);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbPwd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbMobile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "密码工具箱";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMobile;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btnName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.Button btnEmail;
    }
}

