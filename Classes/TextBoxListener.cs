using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace EasyFTP.Classes
{
    class TextBoxListener : TraceListener
    {
        RichTextBox textBox;
        public TextBoxListener(RichTextBox box)
        {
            textBox = box;
        }

        public override void Write(string message)
        {
            if (FtpOperations.Instance.IsClientConnected())
                textBox.AppendText(message);
        }

        public override void WriteLine(string message)
        {
            if (FtpOperations.Instance.IsClientConnected())
                textBox.AppendText(message + "\r\n");
        }
    }
}
