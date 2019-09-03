namespace EasyFTP.Classes.View.Forms
{
    partial class DialogConnect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.buttonDialogCancel = new System.Windows.Forms.Button();
            this.buttonDialogConfirm = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP-Adress:";
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(12, 29);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(232, 20);
            this.tbIP.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "User:";
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(6, 30);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(220, 20);
            this.tbUser.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Password:";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(6, 69);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(220, 20);
            this.tbPassword.TabIndex = 4;
            // 
            // buttonDialogCancel
            // 
            this.buttonDialogCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDialogCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonDialogCancel.Location = new System.Drawing.Point(170, 189);
            this.buttonDialogCancel.Name = "buttonDialogCancel";
            this.buttonDialogCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonDialogCancel.TabIndex = 6;
            this.buttonDialogCancel.Text = "Cancel";
            this.buttonDialogCancel.UseVisualStyleBackColor = true;
            this.buttonDialogCancel.Click += new System.EventHandler(this.buttonDialogCancel_Click);
            // 
            // buttonDialogConfirm
            // 
            this.buttonDialogConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDialogConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDialogConfirm.Location = new System.Drawing.Point(89, 189);
            this.buttonDialogConfirm.Name = "buttonDialogConfirm";
            this.buttonDialogConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonDialogConfirm.TabIndex = 5;
            this.buttonDialogConfirm.Text = "Confirm";
            this.buttonDialogConfirm.UseVisualStyleBackColor = true;
            this.buttonDialogConfirm.Click += new System.EventHandler(this.buttonDialogConfirm_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbUser);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Location = new System.Drawing.Point(12, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 103);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credentials";
            // 
            // DialogConnect
            // 
            this.AcceptButton = this.buttonDialogConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 221);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonDialogConfirm);
            this.Controls.Add(this.buttonDialogCancel);
            this.Controls.Add(this.tbIP);
            this.Controls.Add(this.label1);
            this.Name = "DialogConnect";
            this.Text = "Connect to FTP";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button buttonDialogCancel;
        private System.Windows.Forms.Button buttonDialogConfirm;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}