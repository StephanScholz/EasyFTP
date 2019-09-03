using EasyFTP.Classes.View.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyFTP.Classes.Model
{
    public abstract class BasicModel
    {
        public MainForm Form { get; set; }
        public FtpOperations Ftp { get; set; }

        protected BasicModel(MainForm form)
        {
            Form = form;
        }

        public bool CheckFtpConnection()
        {
            if (Ftp != null)
                return Ftp.IsClientConnected();
            else
                return false;
        }
    }
}
