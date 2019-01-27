using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 閃避遊戲.game
{
    public partial class Teach1 : UserControl
    {
        List<Image> images = new List<Image>();
        Image image,image1;
        int a = 0;
        int x, y, width, height, x1, y1, width1, height1;
        public bool sttop;
        public event EventHandler Timestart;

        private void Teach1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 2; i++)
            {
                image = new Bitmap("pic\\game1\\start\\howto1\\" + i + ".png");
                images.Add(image);
            }
            image = images[0];
            image1 = images[2];
            timer1.Start();
        }

        private void Teach1_Paint(object sender, PaintEventArgs e)
        {
            chg_img(e.Graphics);
           
        }

        public Teach1()
        {
            InitializeComponent(); x = 420; y = 180; width = 389; height = 350;
            x1 = 5; y1 = 120; width1 = 443; height1=420;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            stop();
        }
        public void stop()
        {
            timer1.Stop();
            this.Visible = false;
            this.Enabled = false;
            Timestart(this, EventArgs.Empty);
        }
        public void chg_img(Graphics g)
        {
            a += 1;
            switch (a)
            {
                case 0: image = images[0]; break;
                case 25: image = images[1];a = -25; break;
            }
            g.DrawImage(image, x, y, width, height);
            g.DrawImage(image1, x1, y1, width1, height1);
        }
    }
}

