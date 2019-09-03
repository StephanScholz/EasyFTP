﻿using EasyFTP.Classes;
using EasyFTP.Classes.Model;
using FluentFTP;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyFTP.Classes.View.Forms
{
    public partial class MainForm : Form
    {

        private FtpOperations ftp;
        private bool isRenaming = false;
        // All transfer ops happen here
        private Transfer transfer;
        // All modifications concerning files and dirs happen here
        private Modify modify;

        // Returns the path of the respective TextBox
        public string GetPathText(bool local)
        {
            if (local)
            {
                if (tbLocalPath != null)
                    return tbLocalPath.Text;
            }
            else
            {
                if (tbRemotePath != null)
                    return tbRemotePath.Text;
            }
            return "";
        }

        public MainForm()
        {
            InitializeComponent();
            
            ftp = FtpOperations.Instance;
        }

        // Placeholder for subdirs that have not been opened (yet).
        private const string PLACEHOLDER = "...";

        //--------------------Loading/Closing Events--------------------
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            // custom debug console for FTP-connections
            FtpTrace.AddListener(new TextBoxListener(EasyConsole));

            FtpTrace.LogUserName = false;   // hide FTP user names
            FtpTrace.LogPassword = false;   // hide FTP passwords

            // Create Models
            modify = new Modify(this);
            transfer = new Transfer(this);

            // populates the local TreeView
            PopulateTreeViewLocal();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            ftp.CloseUserSession();
        }

        //--------------------Click Events--------------------

        private void listViewLocal_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // TODO Working on restricting the drive directories, making them never changable
            if (!listViewLocal.SelectedItems.IsBlank())
                tbLocalPath.Text += "\\" + listViewLocal.SelectedItems[0].Text;
            else
                tbLocalPath.Text = tbLocalPath.Text.Substring(0, tbLocalPath.Text.LastIndexOf('\\'));
        }

        private void listViewRemote_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!listViewRemote.SelectedItems.IsBlank())
                tbRemotePath.Text += "\\" + listViewRemote.SelectedItems[0].Text;
            else
                tbRemotePath.Text = tbRemotePath.Text.Substring(0, tbRemotePath.Text.LastIndexOf('\\'));
        }

        private void TvLocal_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            PopulateListViewLocal(e.Node);
            // Only when not in Rename-Mode
            if (!isRenaming)
                tbLocalPath.Text = e.Node.Tag.ToString();
        }

        private void TvRemote_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            PopulateListViewRemote(e.Node);
            // Only when not in Rename-Mode
            if (!isRenaming)
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

        private void DeleteFile_Click(object sender, EventArgs e)
        {
            //TODO Test Delete-Function
            string cName = ((ToolStripMenuItem)sender).Tag.ToString();
            modify.Delete(cName);
        }

        //Starts editing the Label of the View (to rename files or directories)
        private void RenameFile_Click(object sender, EventArgs e)
        {
            string cName = ((ToolStripMenuItem)sender).Tag.ToString();

            switch (cName)
            {
                case "tvLocal":
                    tvLocal.SelectedNode.BeginEdit();
                    break;

                case "tvRemote":
                    tvRemote.SelectedNode.BeginEdit();
                    break;

                case "listViewLocal":
                    listViewLocal.SelectedItems[0].BeginEdit();
                    break;

                case "listViewRemote":
                    listViewRemote.SelectedItems[0].BeginEdit();
                    break;
            }
        }

        private void BeforeLabelEdit(object sender, EventArgs e)
        {
            isRenaming = true;
        }

        // Invoked after the Renaming of the respective Node or Item took place
        private void tvLocal_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // TODO Simplify AfterLabelEdits
            string oldPath = tbLocalPath.Text;
            FtpTrace.WriteLine(oldPath);
            // Checks if Label is in correct layout
            if (CheckLabel(e.Label))
            {
                // Add the new Label to the newPath
                string newPath = oldPath.Substring(0, oldPath.LastIndexOf('\\') + 1) + e.Label;
                // Rename the directory on the local system
                if (Directory.Exists(oldPath))
                {
                    Directory.Move(oldPath, newPath);
                    e.Node.Name = e.Label;
                    e.Node.Tag = newPath;
                    tbLocalPath.Text = newPath;
                }

                // Stop editing without canceling the label change.
                e.Node.EndEdit(false);
                isRenaming = false;
            }
            else
            {
                /* Cancel the label edit action, inform the user, and 
                   place the node in edit mode again. */
                e.CancelEdit = true;
                if (e.Label != null)
                    e.Node.BeginEdit();
            }
        }
        // Invoked after the Renaming of the respective Node or Item took place
        private void tvRemote_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string oldPath = tbRemotePath.Text;
            // Checks if Label is in correct layout
            if (CheckLabel(e.Label))
            {
                // Add the new Label to the newPath
                string newPath = oldPath.Substring(0, oldPath.LastIndexOf('/') + 1) + e.Label;
                // Rename on server
                if (ftp.Rename(oldPath, newPath))
                    tbRemotePath.Text = newPath;
                // set the new Tag
                e.Node.Tag = newPath;
                // Stop editing without canceling the label change.
                e.Node.EndEdit(false);
                isRenaming = false;
            }
            else
            {
                /* Cancel the label edit action, inform the user, and 
                   place the node in edit mode again. */
                e.CancelEdit = true;
                if (e.Label != null)
                    e.Node.BeginEdit();
            }
        }
        // Invoked after the Renaming of the respective Node or Item took place
        private void listViewLocal_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string oldPath = tbLocalPath.Text + "\\" + listViewLocal.Items[e.Item].Text;
            // Checks if Label is in correct layout
            if (CheckLabel(e.Label, true))
            {
                // Add the new Label to the newPath
                string newPath = tbLocalPath.Text + "\\" + e.Label;
                // Rename the directory on the local system
                if (File.Exists(oldPath))
                {
                    File.Move(oldPath, newPath);
                    listViewLocal.Items[e.Item].Text = e.Label;
                    listViewLocal.Items[e.Item].Tag = newPath;
                }
                isRenaming = false;
            }
            else
            {
                // Cancel the label edit action
                e.CancelEdit = true;
            }
        }
        // Invoked after the Renaming of the respective Node or Item took place
        private void listViewRemote_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string oldPath = tbRemotePath.Text + "/" + listViewRemote.Items[e.Item].Text;
            // Checks if Label is in correct layout
            if (CheckLabel(e.Label, true))
            {
                // Add the new Label to the newPath
                string newPath = tbRemotePath.Text + "/" + e.Label;
                // Rename on the server
                if (ftp.Rename(oldPath, newPath))
                {
                    listViewRemote.Items[e.Item].Text = e.Label;
                    listViewRemote.Items[e.Item].Tag = newPath;
                }
                isRenaming = false;
            }
            else
            {
                // Cancel the label edit action
                e.CancelEdit = true;
            }
        }

        // Checks if the Label has the correct Format and does not violate naming rules
        private bool CheckLabel(string lab, bool isFile = false)
        {
            if (lab != null)
            {
                if (lab.Length > 0)
                {
                    if ((lab.IndexOfAny(new char[] {'@', '.', ',', '!'}) == -1))
                    {
                        return true;
                    }
                    else
                    {
                        if (isFile)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Invalid Name.\n" +
                           "The invalid characters are: '@','.', ',', '!'",
                           "Rename File or Directory");
                            return false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Name.\nThe new name cannot be blank",
                       "Rename File or Directory");
                    return false;
                }
            }
            return false;
        }

        // Displays a responsive TextBox to ask for permission to delete files and directories
        internal bool CheckDelete(string path, bool isFile)
        {
            if (isFile)
            {
                return (MessageBox.Show(
                        "Do you want to delete this file?\n\nPath: " + path,
                        "Delete File",
                        MessageBoxButtons.YesNo) == DialogResult.Yes);
            }
            else
            {
                return (MessageBox.Show(
                        "Do you want to delete this directory and all its content?\n\nPath: " + path,
                        "Delete Directory",
                        MessageBoxButtons.YesNo) == DialogResult.Yes);
            }
        }

        // Async Events

        private async void UploadFile_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (await PerformTransfer(true))
                {
                    PopulateListViewRemote();
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DownloadFile_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (await PerformTransfer(false))
                {
                    PopulateListViewLocal();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------TreeView/ListView--------------------

        internal void PopulateTreeViewRemote()
        {
            tvRemote.Nodes.Clear();
            TreeNode rootNode = new TreeNode("/")
            {
                Tag = "/"
            };
            GetFtpDirectories(ftp.GetDirectoryListing("/"), rootNode);
            tvRemote.Nodes.Add(rootNode);
            tbRemotePath.Text = "/";
        }

        internal void PopulateTreeViewLocal()
        {
            tvLocal.Nodes.Clear();
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
        internal void PopulateListViewLocal(TreeNode newSelected = null)
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
        internal void PopulateListViewRemote(TreeNode newSelected = null)
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
                        catch (UnauthorizedAccessException) { }
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

        //--------------------Enable/Disable Buttons-------------------

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

        //--------------------Clean Up--------------------

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

        //--------------------Transfer Files--------------------

        // Transfers (uploads/downloads) files from and to the ftp server.
        private async Task<bool> PerformTransfer(bool upload)
        {
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

        //--------------------Timer--------------------

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

        //--------------------Context Menu--------------------

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
            ToolStripMenuItem menuItemDelete = new ToolStripMenuItem("&Delete", null, DeleteFile_Click);
            ToolStripMenuItem menuItemRename = new ToolStripMenuItem("&Rename", null, RenameFile_Click);

            Control c = ((ContextMenuStrip)sender).SourceControl;

            if (c != null)
            {
                // Save the View
                menuItemDelete.Tag = c.Name;
                menuItemRename.Tag = c.Name;
            }

            // Clear all previously added ToolStripMenuItems.
            contextMenu1.Items.Clear();

            // Add the new ones
            if (c == tvLocal)
            {

            }
            else if (c == tvRemote)
            {

            }
            else if (c == listViewLocal)
            {
                contextMenu1.Items.Add(menuItemUpload);
            }
            else if (c == listViewRemote)
            {
                contextMenu1.Items.Add(menuItemDownload);
            }

            // Add "Delete" to all Menus
            contextMenu1.Items.Add(menuItemDelete);
            // Add "Rename" to all Menus
            contextMenu1.Items.Add(menuItemRename);

            e.Cancel = false;
        }
    }
}