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
    public partial class Teach3 : UserControl
    {
        List<Image> images = new List<Image>();
        List<Image> images1 = new List<Image>();
        Image image,image1;
        int a = 0,b=0;
        int x, y, width, height;
        public bool sttop;
        public event EventHandler Timestart;
        public Teach3()
        {
            InitializeComponent();
            InitializeComponent(); x = 400; y = 100; width = 500; height = 500;
            timer1.Start();
        }
        private void Teach3_Paint(object sender, PaintEventArgs e)
        {
            this.draw_img(e.Graphics);
            this.draw_img1(e.Graphics);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void Teach3_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 18; i++)
            {
                image = new Bitmap("pic\\game3start\\3\\" + i + ".png");
                images.Add(image);
            }
            for (int i = 0; i < 2; i++)
            {
                image1 = new Bitmap("pic\\game3start\\攻擊說明\\" + i + ".png");
                images1.Add(image1);
            }
            image = images[0];
            image1 = images1[0];
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            stop();
        }
        public void draw_img(Graphics g)
        {
            a += 1;      
            switch (a)
            {
                case 0: image = images[0]; break;
                case 25: image = images[1]; break;
                case 35: image = images[2]; break;
                case 55: image = images[3]; break;
                case 65: image = images[4];  break;
                case 75: image = images[5]; break;
                case 85: image = images[6]; break;
                case 95: image = images[7]; break;
                case 105: image = images[8]; break;
                case 115: image = images[9];  break;
                case 125: image = images[10]; break;
                case 135: image = images[11]; break;
                case 145: image = images[12]; break;
                case 155: image = images[13]; break;
                case 165: image = images[14]; break;
                case 175: image = images[15]; break;
                case 185: image = images[16]; break;
                case 195: image = images[17]; break;
                case 205: image = images[18];a = -10; break;
            }
            g.DrawImage(image, x, y, width, height);      
        }
        public void draw_img1(Graphics g)
        {
            b += 1;
            switch (b)
            {
                case 0: image1 = images1[0]; break;
                case 15: image1 = images1[1];b=-15; break;
            }
            g.DrawImage(image1, 100, 200, 400, 400);
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
