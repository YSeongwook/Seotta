using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Seotta
{
    public partial class HelpForm : Form
    {
        public HelpForm(string title, string content)
        {
            InitializeComponent();
            titleLabel.Text = title;
            contentTextBox.Text = content;
        }
    }
}
