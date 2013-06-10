using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BridgeASMX
{
    public partial class frmShowCode : Form
    {
        public frmShowCode(string code)
        {
            InitializeComponent();
            txtCode.Text = code;
        }

        private void frmShowCode_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            txtCode.SelectAll();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtCode.Text);
        }
    }
}
