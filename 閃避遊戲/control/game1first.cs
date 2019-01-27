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
    public partial class game1first : UserControl
    {
        List<Image> images = new List<Image>();
        Image image,image1;
        int ok = 0;
        int a = 0;
        int a1 = 1;
        int timer = 0;
        int x, y, width, height;
        int x1, y1, width1, height1;
        public bool sttop;
        public event EventHandler teachstart;
        public game1first()
        {
            InitializeComponent();x = 0;y = 0;width = 980; height = 710;
            timer1.Start(); x1 = 830; y1 = 655; width1 = 120; height1 = 40;
        }
        private void game1first_Paint(object sender, PaintEventArgs e)
        {
            this.draw_img(e.Graphics);
            this.draw_img1(e.Graphics);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            stop();
        }
        private void game1first_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 7; i++)
            {
                image = new Bitmap("pic\\game1\\start\\" + i + ".png");
                images.Add(image); 
            }
            image = images[0];
            image1 = images[6];
        }
        public void draw_img(Graphics g)
        {
                if (timer < 5)
                {
                    a += 1;
                    if (a % 20 == 0)
                    {
                        timer += 1;
                        image = images[timer];
                    }
                }
            g.DrawImage(image, x, y, width, height);           
        }
        public void draw_img1(Graphics g)
        {
            a1 += 1;
            switch (a1)
            {
                case 0: image1 = images[6]; break;
                case 16: image1 = images[7];a1=-8; break;
            }
            g.DrawImage(image1, x1, y1, width1, height1);
        }
        public void stop()
        {
                images.Clear();
                sttop = true;
                this.Visible = false;
                this.Enabled = false;
                 timer1.Stop();
            teachstart(this, EventArgs.Empty);
        }
    }
}

