﻿using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EasyFTP.Classes
{
    /* This class offers different operations for communicating with FTP-Servers,
     * downloading files, uploading files, checking status/directory integrity and more */
    class FtpOperations
    {
        public FtpClient client;

        public FtpOperations() { }

        public bool InitFtpServerSession(string[] credentials)
        {
            // create an FTP client. cred[0] is IP-Adress
            client = new FtpClient(credentials[0])
            {

                // if FTP needs credentials
                Credentials = new NetworkCredential(credentials[1], credentials[2]),

                /* Support TLS-Protocol 1.1 and 1.2
                 * use only for late testing, because Certificates are expensive! */
                //client.EncryptionMode = FtpEncryptionMode.Explicit;

                // standard FTP-Port, change if needed
                Port = 21
            };

            try
            {
                // begin connecting to the server
                client.Connect();
                return true;
            }
            catch (FtpAuthenticationException)
            {
                MessageBox.Show("Failed to connect to FTP-Server. Please check your Username and Password",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            /* Just for testing, this needs to be catched, 
             * because Testserver runs self signed certificate */
            catch (System.Security.Authentication.AuthenticationException)
            {
                MessageBox.Show("You are using a self-signed protocol. Are you sure you want to continue?",
                    "Outdated Certificate", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

            return false;
        }
        
        public FtpListItem[] GetDirectoryListing(string root)
        {
            return client.GetListing(root);
        }

        /* Uploads one or many selected files from the local listView.
         * This currently only works with MultiSelect == false. MultiSelect feature
         * will probably be added in the future
         */
        public bool UploadFile(string localPath, string remotePath)
        {
            if (!File.Exists(localPath))
            {
                MessageBox.Show("It seems the file you wanted to upload, does not exist. Please try again.", 
                    "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(localPath);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                MessageBox.Show("You cannot upload directories. Please select a File and try again.",
                    "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;                
            }
            else
            {
                Console.WriteLine(client.GetChmod(remotePath));
                return client.UploadFile(localPath, remotePath);
            }
        }

        // close current server session and disable necessary buttons
        public void CloseUserSession()
        {
            if (client != null && client.IsConnected) client.Disconnect();
        }

    }
}