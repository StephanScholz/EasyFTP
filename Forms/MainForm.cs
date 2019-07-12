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
                    TreeNode rootNode = new TreeNode(info.Name)
                    {
                        Tag = info
                    };
                    tvLocal.Nodes.Add(rootNode);
                    rootNode.Nodes.Add(PLACEHOLDER);
                }
            }
        }
        
        private void PopulateTreeViewRemote()
        {
            TreeNode rootNode = new TreeNode("/")
            {
                Tag = "/"
            };
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
                aNode = new TreeNode(item.Name, 0, 0)
                {
                    Tag = item.FullName,
                    ImageKey = "folder"
                };
                
                if (item.Type == FtpFileSystemObjectType.Directory)
                {
                    subSubDirs = ftp.GetDirectoryListing(item.FullName);
                    GetFtpDirectories(subSubDirs, aNode);
                    nodeToAddTo.Nodes.Add(aNode);
                }
            }
        }

        /* Populates the ListView with the directories and files of the current selection
         * in the TreeView. "newSelected" default is always the SelectedNode of the respective TreeView*/
        private void PopulateListViewLocal(TreeNode newSelected = null)
        {
            // Default param
            if (newSelected == null) newSelected = tvLocal.SelectedNode;

            // clear old items
            listViewLocal.Items.Clear();
            DirectoryInfo nodeDirInfo = new DirectoryInfo(newSelected.Tag.ToString());
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;
            
            try
            {
                // load new items
                foreach (FileInfo file in nodeDirInfo.GetFiles())
                {
                    item = new ListViewItem(file.Name, 1)
                    {
                        // Adding information about the full path on the file system to the ListViewItem.
                        Tag = file.FullName
                    };

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
        private void PopulateListViewRemote(TreeNode newSelected = null)
        {
            // Default param
            if (newSelected == null) newSelected = tvRemote.SelectedNode;

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
                    lvItem = new ListViewItem(item.Name, 0)
                    {
                        Tag = item.FullName
                    };

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
        private void TvLocal_BeforeExpand(object sender, TreeViewCancelEventArgs e)
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
                        TreeNode node = new TreeNode(di.Name)
                        {
                            Tag = dir,
                            ImageIndex = 0,
                            SelectedImageIndex = 0
                        };
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
            tsDownload.Enabled = flag;
            downloadFileToolStripMenuItem.Enabled = flag;
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
            else
            {
                return;
            }

            // initiate a server-session
            if (cred != null)
                // Login successful?
                if (ftp.InitFtpServerSession(cred))
                    sessionTimer.Start();
                else
                    return;

            PopulateTreeViewRemote();

            //Enable all necessary buttons
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

        private void TvLocal_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            PopulateListViewLocal(e.Node);
            tbLocalPath.Text = e.Node.Tag.ToString();
        }

        private void TreeViewRemote_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
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
        
        private async void UploadFile_ClickAsync(object sender, EventArgs e)
        {
            if (await PerformTransfer(true))
                PopulateListViewRemote();
        }
        
        private async void DownloadFile_ClickAsync(object sender, EventArgs e)
        {
            if (await PerformTransfer(false))
                PopulateListViewLocal();
        }

        // Transfers (uploads/downloads) files from and to the ftp server.
        private async Task<bool> PerformTransfer(bool upload)
        {
            ResetTimer();

            string pathLocal = "";
            string pathRemote = "";

            if (upload)
            {
                foreach (ListViewItem item in listViewLocal.SelectedItems)
                {
                    pathLocal = tbLocalPath.Text + "\\" + item.Text;
                    pathRemote = tbRemotePath.Text + "/" + item.Text;
                }
                return await ftp.UploadFileAsync(pathLocal, pathRemote, progressBar1);
            }
            else
            {
                foreach (ListViewItem item in listViewRemote.SelectedItems)
                {
                    pathLocal = tbLocalPath.Text + "\\" + item.Text;
                    pathRemote = tbRemotePath.Text + "/" + item.Text;
                }
                return await ftp.DownloadFileAsync(pathLocal, pathRemote, progressBar1);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            ftp.CloseUserSession();
        }

        /* Restarts the timer. Call this when user comes out of idle */
        public void ResetTimer()
        {
            sessionTimer.Stop();
            sessionTimer.Start();
        }

        /* Called every 10 Minutes, when the user is in idle state
         * Intervals cannot be changed and are hardcoded. No transaction
         * should take more than 10 minutes, because during transaction the user
         * will be in idle state.
         */
        private void SessionTimerTick(object sender, EventArgs e)
        {
            CleanUp();
            sessionTimer.Stop();
        }

        /* Open contextMenu after MouseButton check */
        private void View_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                        contextMenu1.Show(new Point(Cursor.Position.X, Cursor.Position.Y));
                }
                break;
            }
        }

        /* Open contextMenu dynamically for every View. As there are different
         * actions to be performed, every view gets its own set of ToolStripMenuItems */
        private void ContextMenu1_Opening(object sender, CancelEventArgs e)
        {
            ToolStripMenuItem menuItemUpload = new ToolStripMenuItem("&Upload File", null, UploadFile_ClickAsync);
            ToolStripMenuItem menuItemDownload = new ToolStripMenuItem("&Download File", null, DownloadFile_ClickAsync);
            ToolStripMenuItem menuItemDelete = new ToolStripMenuItem("&Delete");

            Control c = contextMenu1.SourceControl;

            // Clear all previously added ToolStripMenuItems.
            contextMenu1.Items.Clear();
            if (c == tvLocal)
            {
                contextMenu1.Items.Add(menuItemDelete);
            }
            else if (c == tvRemote)
            {

            }
            else if (c == listViewLocal)
            {
                contextMenu1.Items.Add(menuItemUpload);
                contextMenu1.Items.Add(menuItemDelete);
            }
            else if (c == listViewRemote)
            {
                contextMenu1.Items.Add(menuItemDownload);
            }
            e.Cancel = false;
        }
    }
}
