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
    public partial class Teach2 : UserControl
    {
        List<Image> images = new List<Image>();
        Image image;
        int a = 0;
        int x, y, width, height;
        public bool sttop;
        public event EventHandler Timestart;
        public Teach2()
        {
            InitializeComponent(); x = 250; y = 100; width = 500; height = 500;
            timer1.Start(); 
        }
        private void Teach2_Paint(object sender, PaintEventArgs e)
        {
            this.draw_img(e.Graphics);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            stop();
        }   
        private void Teach2_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 6; i++)
            {
                image = new Bitmap("pic\\game2\\start\\2\\" + i + ".png");
                images.Add(image);
            }
            image = images[0];

        }
        public void draw_img(Graphics g)
        {
            a += 1;
            switch (a)
            {
                case 0: image = images[0]; break;
                case 10: image = images[1]; break;
                case 15: image = images[2]; break;
                case 25: image = images[3]; break;
                case 35: image = images[4]; break;
                case 45: image = images[5]; break;
                case 55: image = images[6];a = -10; break;
            }
            g.DrawImage(image, x, y, width, height);
        }
        public void stop()
        {
            images.Clear();
            sttop = true;
            this.Visible = false;
            this.Enabled = false;
            timer1.Stop();
            Timestart(this, EventArgs.Empty);
        }
    }
}
