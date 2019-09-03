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
                    if (Form.CheckDelete(path, false))
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
                    if (Form.CheckDelete(path, false))
                    {
                        Ftp.Delete(path, true);
                        Form.PopulateTreeViewRemote();
                    }
                    break;

                case "listViewLocal":
                    path = Form.GetPathText(true);
                    if (Form.CheckDelete(path, true))
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
                    if (Form.CheckDelete(path, true))
                    {
                        Ftp.Delete(path, false);
                        Form.PopulateListViewRemote();
                    }
                    break;
            }
        }
    }
}
