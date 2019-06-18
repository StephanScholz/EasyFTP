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

        private bool Init()
        {
            if (textBox != null && textBox.IsDisposed)
            {
                // back to null if the control is disposed
                textBox = null;
            }
            // find the logger text box
            if (textBox == null)
            {
                // open forms
                foreach (Form f in Application.OpenForms)
                {
                    // controls on those forms
                    foreach (Control c in f.Controls)
                    {
                        // does the name match 
                        if (c is RichTextBox)
                        {
                            // found one!
                            textBox = (RichTextBox)c;
                            break;
                        }
                    }
                }
            }
            return textBox != null && !textBox.IsDisposed;
        }

        public override void Write(string message)
        {
            if (Init())
            {
                textBox.AppendText(message);
            }
        }

        public override void WriteLine(string message)
        {
            if (Init())
            {
                textBox.AppendText(message + "\r\n");
            }
        }
    }
}
