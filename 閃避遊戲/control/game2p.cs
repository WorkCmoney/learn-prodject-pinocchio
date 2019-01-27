using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace 閃避遊戲
{
    public partial class game2p : UserControl
    {

        public  bool down { get; set; }
        public game2p()
        {
            InitializeComponent(); 
        }

        public event EventHandler Timestart;

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            game2.one_player = false;
            down = true;
            this.Visible = false;
            this.Enabled = false;
            Timestart(this, EventArgs.Empty);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            game2.one_player = true;
            down = true;
            this.Visible = false;
            this.Enabled = false;
            Timestart(this, EventArgs.Empty);
        }
    }
}
