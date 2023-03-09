using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace work_01
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void courseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            frm.MdiParent = this;
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudent frmS = new frmStudent();
            frmS.Show();
            frmS.MdiParent = this;
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployees frmemp = new frmEmployees();
            frmemp.Show();
            frmemp.MdiParent = this;
        }
    }
}
