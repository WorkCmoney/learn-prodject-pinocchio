using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 閃避遊戲.control
{
    public partial class Teach12 : UserControl
    {
        public event EventHandler Timestart;
        public Teach12()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            stop();
        }
        public void stop()
        {
            this.Visible = false;
            this.Enabled = false;           
            Timestart(this, EventArgs.Empty);
        }
    }
}
