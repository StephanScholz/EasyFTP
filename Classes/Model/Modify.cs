using EasyFTP.Classes.View.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyFTP.Classes.Model
{
    internal class Modify : BasicModel
    {
        internal Modify(MainForm form) : base(form)
        {

        }

        internal void Delete(string cName)
        {
            string path = "";

            switch (cName)
            {
                case "tvLocal":
                    path = Form.GetPathText(true);
                    if (Form.ConfirmOperation("Do you want to delete this file?", "Delete file"))
                    {
                        if (Directory.Exists(path))
                        {
                            Directory.Delete(path, true);
                            Form.PopulateTreeViewLocal();
                        }
                    }
                    break;

                case "tvRemote":
                    path = Form.GetPathText(false);
                    if (Form.ConfirmOperation("Do you want to delete this directory and all its content?", "Delete directory"))
                    {
                        Ftp.Delete(path, true);
                        Form.PopulateTreeViewRemote();
                    }
                    break;

                case "listViewLocal":
                    path = Form.GetPathText(true);
                    if (Form.ConfirmOperation("Do you want to delete this file?", "Delete file"))
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                            Form.PopulateListViewLocal();
                        }
                    }
                    break;

                case "listViewRemote":
                    path = Form.GetPathText(false);
                    if (Form.ConfirmOperation("Do you want to delete this directory and all its content?", "Delete directory"))
                    {
                        Ftp.Delete(path, false);
                        Form.PopulateListViewRemote();
                    }
                    break;
            }
        }

        // Renames a File or directory. oldPath specifies the Full Name of the object before renaming,
        // label specifies the new relative name of the label
        internal bool Rename(string oldPath, string newPath, string label, bool remote)
        {
            if (!remote)
            {
                if (Directory.Exists(oldPath))
                {
                    Directory.Move(oldPath, newPath);
                    return true;
                }
                else if (File.Exists(oldPath))
                {
                    File.Move(oldPath, newPath);
                    return true;
                }
            }
            else
            {
                if (Ftp.Rename(oldPath, newPath))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
