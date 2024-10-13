using Project1.Properties;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Form frm2 = new Form2();

        private void findClientToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("EXit");
        }

        private void fIleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openForm2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm2.MdiParent = this;
            frm2.Show();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
    
}
