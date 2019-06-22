using EasyFTP.Classes;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyFTP
{
    public partial class MainForm : Form
    {

        private FtpOperations ftp;

        public MainForm()
        {
            InitializeComponent();
            
            ftp = FtpOperations.Instance;
        }

        // Placeholder for subdirs that have not been opened (yet).
        private const string PLACEHOLDER = "...";

        private void Form1_Load(object sender, EventArgs e)
        {
            // custom debug console for FTP-connections
            FtpTrace.AddListener(new TextBoxListener(EasyConsole));

            FtpTrace.LogUserName = false;   // hide FTP user names
            FtpTrace.LogPassword = false;   // hide FTP passwords

            // Load all drives
            string[] drives = Environment.GetLogicalDrives();
            // Update textbox
            tbLocalPath.Text = drives[0];
            foreach (string drive in drives)
            {
                DirectoryInfo info = new DirectoryInfo(drive);
                if (info.Exists)
                {
                    TreeNode rootNode = new TreeNode(info.Name);
                    rootNode.Tag = info;
                    tvLocal.Nodes.Add(rootNode);
                    rootNode.Nodes.Add(PLACEHOLDER);
                }
            }
        }
        
        private void PopulateTreeViewRemote()
        {
            TreeNode rootNode = new TreeNode("/");
            rootNode.Tag = "/";
            GetFtpDirectories(ftp.GetDirectoryListing("/"), rootNode);
            tvRemote.Nodes.Add(rootNode);
        }

        /* Creates all sub-nodes in the Remote Tree, specified by the parameter "subDirs" and adds them to the nodeToAddTo */
        private void GetFtpDirectories(FtpListItem[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            FtpListItem[] subSubDirs;
            foreach (FtpListItem item in subDirs)
            {
                aNode = new TreeNode(item.Name, 0, 0);
                aNode.Tag = item.FullName;
                aNode.ImageKey = "folder";

                subSubDirs = ftp.GetDirectoryListing(item.FullName);
                if (item.Type == FtpFileSystemObjectType.Directory)
                {
                    GetFtpDirectories(subSubDirs, aNode);
                    nodeToAddTo.Nodes.Add(aNode);
                }
            }
        }

        /* Populates the ListView with the directories and files of the current selection
         * in the TreeView. This Method is in testing and probably needs major improvements*/
        private void PopulateListViewLocal(TreeNode newSelected)
        {
            listViewLocal.Items.Clear();
            DirectoryInfo nodeDirInfo = new DirectoryInfo(newSelected.Tag.ToString());
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;
            
            try
            {
                foreach (FileInfo file in nodeDirInfo.GetFiles())
                {
                    item = new ListViewItem(file.Name, 1);
                    // Adding information about the full path on the file system to the ListViewItem.
                    item.Tag = file.FullName;

                    subItems = new ListViewItem.ListViewSubItem[] {
                    new ListViewItem.ListViewSubItem(item, "File"),
                    new ListViewItem.ListViewSubItem(item, file.LastAccessTime.ToShortDateString())
                };

                    item.SubItems.AddRange(subItems);
                    listViewLocal.Items.Add(item);
                }

                listViewLocal.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (UnauthorizedAccessException)
            {
                //MessageBox.Show("", "Not Authorized", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /* Does the same as PopulateListViewLocal(), but with a Ftp directory structure */
        private void PopulateListViewRemote(TreeNode newSelected)
        {
            // clear old items
            listViewRemote.Items.Clear();
            string root = (string)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem lvItem = null;

            // load new ones
            foreach (FtpListItem item in ftp.GetDirectoryListing(root))
            {
                if (item.Type == FtpFileSystemObjectType.Directory)
                {
                    lvItem = new ListViewItem(item.Name, 0);

                    lvItem.Tag = item.FullName;

                    subItems = new ListViewItem.ListViewSubItem[] {
                        new ListViewItem.ListViewSubItem(lvItem, "Directory"),
                        new ListViewItem.ListViewSubItem(lvItem,  item.Modified.ToShortDateString())
                    };
                }
                else
                {
                    lvItem = new ListViewItem(item.Name, 1);

                    subItems = new ListViewItem.ListViewSubItem[] {
                        new ListViewItem.ListViewSubItem(lvItem, "File"),
                        new ListViewItem.ListViewSubItem(lvItem,  item.Modified.ToShortDateString())
                    };
                }

                lvItem.SubItems.AddRange(subItems);
                listViewRemote.Items.Add(lvItem);
            }

            listViewRemote.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        /* Gradualy fill the subdir-nodes, when the user expands to them
         */
        private void tvLocal_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Nodes[0].Text == PLACEHOLDER)
                {
                    e.Node.Nodes.Clear();
                    string[] dirs = Directory.GetDirectories(e.Node.Tag.ToString());
                    foreach (string dir in dirs)
                    {
                        DirectoryInfo di = new DirectoryInfo(dir);
                        TreeNode node = new TreeNode(di.Name);
                        node.Tag = dir;
                        node.ImageIndex = 0;
                        node.SelectedImageIndex = 0;
                        try
                        {
                            if (di.GetDirectories().GetLength(0) > 0)
                                node.Nodes.Add(null, PLACEHOLDER);
                        }
                        catch (UnauthorizedAccessException)
                        {

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "ExplorerForm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            e.Node.Nodes.Add(node);
                        }
                    }
                }
            }
        }

        // disable/enable the disconnect button and menu item
        private void DisconnectButtonEnabled(bool flag)
        {
            tsDisconnect.Enabled = flag;
            disconnectToolStripMenuItem.Enabled = flag;
        }

        // disable/enable the Upload/Download Buttons
        private void UpDownloadButtonEnabled(bool flag)
        {
            tsUpload.Enabled = flag;
            uploadFiletoolStripMenuItem.Enabled = flag;
        }

        /* establishes a connection to a FTP Server. Initiates the population of
         * the Remote TreeView. */
        private void ConnectToFTP()
        {
            DialogConnect dc = new DialogConnect();
            string[] cred = null;
            if (dc.ShowDialog() == DialogResult.OK)
            {
                cred = dc.credentials;
            }

            // initiate a server-session
            if (cred != null)
                // Login successful?
                if (ftp.InitFtpServerSession(cred))
                    sessionTimer.Start();
                else
                    return;

            PopulateTreeViewRemote();

            DisconnectButtonEnabled(true);
            UpDownloadButtonEnabled(true);
        }

        // cleans up all remote-views and disconnects from the server
        private void CleanUp()
        {
            // clear tree- and listView
            tvRemote.Nodes.Clear();
            listViewRemote.Items.Clear();
            // terminate server-session
            ftp.CloseUserSession();
            // disable neccessary buttons
            DisconnectButtonEnabled(false);
            UpDownloadButtonEnabled(false);
            // clean path textBox
            tbRemotePath.Text = "";
        }

        private void tvLocal_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            PopulateListViewLocal(e.Node);
            tbLocalPath.Text = e.Node.Tag.ToString();
        }

        private void treeViewRemote_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            PopulateListViewRemote(e.Node);
            tbRemotePath.Text = e.Node.Tag.ToString();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            ConnectToFTP();
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            CleanUp();
        }

        /* Uploads a file from the local environment to the remote ftp server. */
        private async void uploadFile_ClickAsync(object sender, EventArgs e)
        {
            resetTimer();
            // Is there a remote directory selected?
            if (tbRemotePath.Text == "")
            {
                MessageBox.Show("Please select a directory, where the file should be uploaded to.", 
                    "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string pathLocal = "";
            string pathRemote = "";
            foreach (ListViewItem item in listViewLocal.SelectedItems)
            {
                pathLocal = tbLocalPath.Text + "\\" + item.Text;
                pathRemote = tbRemotePath.Text + "/" + item.Text;
            }
            await ftp.UploadFileAsync(pathLocal, pathRemote, progressBar1);
            PopulateListViewRemote(tvRemote.SelectedNode);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            ftp.CloseUserSession();
        }

        /* Restarts the timer. Call this when user comes out of idle */
        public void resetTimer()
        {
            sessionTimer.Stop();
            sessionTimer.Start();
        }

        /* Called every 10 Minutes, when the user is in idle state
         * Intervals cannot be changed and are hardcoded. No transaction
         * should take more than 10 minutes, because during transaction the user
         * will be in idle state.
         */
        private void sessionTimer_Tick(object sender, EventArgs e)
        {
            CleanUp();
            sessionTimer.Stop();
        }

        private void View_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    if (sender.GetType() == typeof(ListView))
                    {
                        // If local ListView
                        if (((ListView)sender).Name == "listViewLocal")
                        {
                            ShowListViewContextMenu(true);
                        }
                        else
                        {
                            ShowListViewContextMenu(false);
                        }
                    }
                    else if (sender.GetType() == typeof(TreeView))
                    {
                        // If local TreeView
                        if (((TreeView)sender).Name == "tvLocal")
                        {
                            ShowTreeViewContextMenu(true);
                        }
                        else
                        {
                            ShowTreeViewContextMenu(false);
                        }
                    }
                }
                break;
            }
        }

        /* Shows the context menu for the TreeViews. Params determine if local or not */
        private void ShowTreeViewContextMenu(bool local)
        {
            contextMenuLocal.Show(new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
        }

        /* Shows the context menu for the ListViews. Paras determine if local or not */
        private void ShowListViewContextMenu(bool local)
        {
            contextMenuLocal.Show(new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
        }
    }
}
