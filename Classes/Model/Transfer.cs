using EasyFTP.Classes.Model;
using EasyFTP.Classes.View.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyFTP.Classes.Model
{
    internal class Transfer : BasicModel
    {
        internal Transfer(MainForm form) : base(form)
        {
            
        }

        // Transfers (uploads/downloads) files from and to the ftp server.
        internal async Task<bool> PerformTransfer(bool upload, string pathLocal, string pathRemote, ProgressBar progressBar)
        {
            if (upload)
            {
                return await Ftp.UploadFileAsync(pathLocal, pathRemote, progressBar);
            }
            else
            {
                return await Ftp.DownloadFileAsync(pathLocal, pathRemote, progressBar);
            }
        }
    }
}
