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

        /* now with multithreading capability. Is sometimes called from asynchronous */
        public override void Write(string message)
        {
            if (textBox.IsDisposed)
                return;
            textBox.Invoke(new Action(() =>
            {
                textBox.AppendText(message);
            }));
        }

        /* now with multithreading capability. Is sometimes called from asynchronous */
        public override void WriteLine(string message)
        {
            if (textBox.IsDisposed)
                return;
            textBox.Invoke(new Action(() =>
            {
                textBox.AppendText(message + "\r\n");
            }));
        }
    }
}
