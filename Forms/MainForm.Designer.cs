namespace EasyFTP
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbLocalPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainerLocal = new System.Windows.Forms.SplitContainer();
            this.tvLocal = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listViewLocal = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbRemotePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainerRemote = new System.Windows.Forms.SplitContainer();
            this.tvRemote = new System.Windows.Forms.TreeView();
            this.listViewRemote = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.uploadFiletoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsConnect = new System.Windows.Forms.ToolStripButton();
            this.tsDisconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsUpload = new System.Windows.Forms.ToolStripButton();
            this.EasyConsole = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sessionTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuLocal = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLocal)).BeginInit();
            this.splitContainerLocal.Panel1.SuspendLayout();
            this.splitContainerLocal.Panel2.SuspendLayout();
            this.splitContainerLocal.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRemote)).BeginInit();
            this.splitContainerRemote.Panel1.SuspendLayout();
            this.splitContainerRemote.Panel2.SuspendLayout();
            this.splitContainerRemote.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuLocal.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.tbLocalPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.splitContainerLocal);
            this.groupBox1.Location = new System.Drawing.Point(12, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 414);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tbLocalPath
            // 
            this.tbLocalPath.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbLocalPath.Location = new System.Drawing.Point(3, 34);
            this.tbLocalPath.Name = "tbLocalPath";
            this.tbLocalPath.ReadOnly = true;
            this.tbLocalPath.Size = new System.Drawing.Size(347, 20);
            this.tbLocalPath.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Local Machine:";
            // 
            // splitContainerLocal
            // 
            this.splitContainerLocal.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.splitContainerLocal.Location = new System.Drawing.Point(3, 60);
            this.splitContainerLocal.Name = "splitContainerLocal";
            this.splitContainerLocal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLocal.Panel1
            // 
            this.splitContainerLocal.Panel1.Controls.Add(this.tvLocal);
            // 
            // splitContainerLocal.Panel2
            // 
            this.splitContainerLocal.Panel2.Controls.Add(this.listViewLocal);
            this.splitContainerLocal.Size = new System.Drawing.Size(347, 348);
            this.splitContainerLocal.SplitterDistance = 149;
            this.splitContainerLocal.TabIndex = 0;
            // 
            // tvLocal
            // 
            this.tvLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvLocal.ImageIndex = 2;
            this.tvLocal.ImageList = this.imageList1;
            this.tvLocal.Location = new System.Drawing.Point(0, 0);
            this.tvLocal.Name = "tvLocal";
            this.tvLocal.SelectedImageIndex = 2;
            this.tvLocal.Size = new System.Drawing.Size(347, 149);
            this.tvLocal.TabIndex = 1;
            this.tvLocal.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvLocal_BeforeExpand);
            this.tvLocal.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvLocal_NodeMouseClick);
            this.tvLocal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.View_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Folder.png");
            this.imageList1.Images.SetKeyName(1, "Document.png");
            this.imageList1.Images.SetKeyName(2, "Drive.png");
            // 
            // listViewLocal
            // 
            this.listViewLocal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLocal.Location = new System.Drawing.Point(0, 0);
            this.listViewLocal.MultiSelect = false;
            this.listViewLocal.Name = "listViewLocal";
            this.listViewLocal.Size = new System.Drawing.Size(347, 195);
            this.listViewLocal.SmallImageList = this.imageList1;
            this.listViewLocal.TabIndex = 2;
            this.listViewLocal.UseCompatibleStateImageBehavior = false;
            this.listViewLocal.View = System.Windows.Forms.View.Details;
            this.listViewLocal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.View_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Last Modified";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.tbRemotePath);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.splitContainerRemote);
            this.groupBox2.Location = new System.Drawing.Point(435, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(353, 414);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // tbRemotePath
            // 
            this.tbRemotePath.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbRemotePath.Location = new System.Drawing.Point(3, 34);
            this.tbRemotePath.Name = "tbRemotePath";
            this.tbRemotePath.ReadOnly = true;
            this.tbRemotePath.Size = new System.Drawing.Size(347, 20);
            this.tbRemotePath.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Remote Machine:";
            // 
            // splitContainerRemote
            // 
            this.splitContainerRemote.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.splitContainerRemote.Location = new System.Drawing.Point(3, 60);
            this.splitContainerRemote.Name = "splitContainerRemote";
            this.splitContainerRemote.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRemote.Panel1
            // 
            this.splitContainerRemote.Panel1.Controls.Add(this.tvRemote);
            // 
            // splitContainerRemote.Panel2
            // 
            this.splitContainerRemote.Panel2.Controls.Add(this.listViewRemote);
            this.splitContainerRemote.Size = new System.Drawing.Size(347, 348);
            this.splitContainerRemote.SplitterDistance = 149;
            this.splitContainerRemote.TabIndex = 0;
            // 
            // tvRemote
            // 
            this.tvRemote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvRemote.ImageIndex = 0;
            this.tvRemote.ImageList = this.imageList1;
            this.tvRemote.Location = new System.Drawing.Point(0, 0);
            this.tvRemote.Name = "tvRemote";
            this.tvRemote.SelectedImageIndex = 0;
            this.tvRemote.Size = new System.Drawing.Size(347, 149);
            this.tvRemote.TabIndex = 4;
            this.tvRemote.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewRemote_NodeMouseClick);
            this.tvRemote.MouseDown += new System.Windows.Forms.MouseEventHandler(this.View_MouseDown);
            // 
            // listViewRemote
            // 
            this.listViewRemote.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewRemote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewRemote.Location = new System.Drawing.Point(0, 0);
            this.listViewRemote.MultiSelect = false;
            this.listViewRemote.Name = "listViewRemote";
            this.listViewRemote.Size = new System.Drawing.Size(347, 195);
            this.listViewRemote.SmallImageList = this.imageList1;
            this.listViewRemote.TabIndex = 5;
            this.listViewRemote.UseCompatibleStateImageBehavior = false;
            this.listViewRemote.View = System.Windows.Forms.View.Details;
            this.listViewRemote.MouseDown += new System.Windows.Forms.MouseEventHandler(this.View_MouseDown);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Type";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Last Modified";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.toolStripSeparator2,
            this.uploadFiletoolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.connectToolStripMenuItem.Text = "Connect...";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.Connect_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Enabled = false;
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
            // 
            // uploadFiletoolStripMenuItem
            // 
            this.uploadFiletoolStripMenuItem.Enabled = false;
            this.uploadFiletoolStripMenuItem.Name = "uploadFiletoolStripMenuItem";
            this.uploadFiletoolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.uploadFiletoolStripMenuItem.Text = "Upload File";
            this.uploadFiletoolStripMenuItem.Click += new System.EventHandler(this.uploadFile_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsConnect,
            this.tsDisconnect,
            this.toolStripSeparator1,
            this.tsUpload});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsConnect
            // 
            this.tsConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsConnect.Image")));
            this.tsConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsConnect.Name = "tsConnect";
            this.tsConnect.Size = new System.Drawing.Size(23, 22);
            this.tsConnect.Text = "Connect";
            this.tsConnect.ToolTipText = "Connect to Server";
            this.tsConnect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // tsDisconnect
            // 
            this.tsDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDisconnect.Enabled = false;
            this.tsDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("tsDisconnect.Image")));
            this.tsDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDisconnect.Name = "tsDisconnect";
            this.tsDisconnect.Size = new System.Drawing.Size(23, 22);
            this.tsDisconnect.Text = "Disconnect";
            this.tsDisconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsUpload
            // 
            this.tsUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsUpload.Enabled = false;
            this.tsUpload.Image = ((System.Drawing.Image)(resources.GetObject("tsUpload.Image")));
            this.tsUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsUpload.Name = "tsUpload";
            this.tsUpload.Size = new System.Drawing.Size(23, 22);
            this.tsUpload.Text = "toolStripButton1";
            this.tsUpload.Click += new System.EventHandler(this.uploadFile_Click);
            // 
            // EasyConsole
            // 
            this.EasyConsole.HideSelection = false;
            this.EasyConsole.Location = new System.Drawing.Point(15, 505);
            this.EasyConsole.Name = "EasyConsole";
            this.EasyConsole.Size = new System.Drawing.Size(770, 103);
            this.EasyConsole.TabIndex = 6;
            this.EasyConsole.Text = "";
            this.EasyConsole.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 489);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Status Console:";
            // 
            // sessionTimer
            // 
            this.sessionTimer.Interval = 600000;
            this.sessionTimer.Tick += new System.EventHandler(this.sessionTimer_Tick);
            // 
            // contextMenuLocal
            // 
            this.contextMenuLocal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuLocal.Name = "contextMenuLocal";
            this.contextMenuLocal.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 620);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EasyConsole);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "EasyFTP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainerLocal.Panel1.ResumeLayout(false);
            this.splitContainerLocal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLocal)).EndInit();
            this.splitContainerLocal.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainerRemote.Panel1.ResumeLayout(false);
            this.splitContainerRemote.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRemote)).EndInit();
            this.splitContainerRemote.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuLocal.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainerLocal;
        private System.Windows.Forms.TreeView tvLocal;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listViewLocal;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainerRemote;
        private System.Windows.Forms.TreeView tvRemote;
        private System.Windows.Forms.ListView listViewRemote;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsConnect;
        private System.Windows.Forms.RichTextBox EasyConsole;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsDisconnect;
        private System.Windows.Forms.Timer sessionTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsUpload;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem uploadFiletoolStripMenuItem;
        private System.Windows.Forms.TextBox tbLocalPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRemotePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuLocal;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}

