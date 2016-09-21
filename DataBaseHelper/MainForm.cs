using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseHelper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //http://www.111cn.net/database/mssqlserver/45135.htm

        }

        private void label3_Click(object sender, EventArgs e)
        {
            FrmAbout frm = new FrmAbout();
            frm.Show();
        }
    }
}
