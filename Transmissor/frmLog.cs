using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transmissor.code;
using TransmissorLog;

namespace Transmissor
{
    public partial class frmLog : Form
    {
        public frmLog()
        {
            InitializeComponent();
        }

        private void frmLog_Load(object sender, EventArgs e)
        {
            txtLog.Text = Log.Ler(dtData.Value);
        }

        private void dtData_ValueChanged(object sender, EventArgs e)
        {
            txtLog.Text=  Log.Ler(dtData.Value);
        }
    }
}
