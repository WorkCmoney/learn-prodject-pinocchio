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
    public partial class End : UserControl
    {
        List<Image> images = new List<Image>();
        List<Image> images1 = new List<Image>();
        List<Image> images2 = new List<Image>();
        Image image, image1,image2;
        int ok = 0;
        int a = 0;
        int a1 = 1;
        int timer = 0;
        int x, y, width, height;
        int x1, y1, width1, height1;
        public bool sttop;
        public event EventHandler Timestart;

      
        public End()
        {
            InitializeComponent(); x = 0; y = 0; width = 980; height = 710;
            timer1.Start(); x1 = 220; y1 = 250; width1 = 500; height1 = 195;
            pictureBox2.Visible = false;

        }
        private void End_Paint(object sender, PaintEventArgs e)
        {
            this.draw_img(e.Graphics);

            if (ok >= 1)
            {
                this.draw_img1(e.Graphics);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            stop(); music.mciMusic("BK.wav", "play");
        }
        private void End_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 13; i++)
            {
                image = new Bitmap("pic\\End\\" + i + ".png");
                images.Add(image);
            }
            for (int i = 0; i <= 20; i++)
            {
                image1 = new Bitmap("pic\\End\\眼腦心\\" + i + ".png");
                images1.Add(image1);
            }
            for (int i = 0; i <= 35; i++)
            {
                image2 = new Bitmap("pic\\End\\變身\\" + i + ".png");
                images2.Add(image2);
            }

            image = images[0];
            image1 = images1[0];
            image2= images2[0];
        }
        public void draw_img(Graphics g)  //畫背景
        {
            if (timer < 13&&ok==0)
            {
                
                    timer += 1;
                    image = images[timer];
                if (timer == 13)
                {
                    ok = 1;timer = 0;
                }              
            }
            g.DrawImage(image, x, y, width, height);
        }
       
        public void draw_img1(Graphics g)//畫物件
        {
            if (timer < 17 && ok == 1)
            {

                timer += 1;
                image1 = images1[timer];
                if (timer == 17)
                {
                    ok = 2;a1 = 0;
                }
            }
            else if ( ok == 2)
            {    
                
                a1 += 1;
                music.mciMusic("heart.mp3", "play");
                switch (a1)
                {
                    case 1: image1 = images1[18]; break;
                    case 5: image1 = images1[19]; break;
                    case 6: image1 = images1[20]; a1 = -1;a += 1; break;
                }
                if (a == 7)
                {
                    ok = 3;
                }
            }
            else if (ok == 3)
            {
                if (timer >=1 )
                {
                    timer -= 1;
                    image1 = images1[timer];
                    if (timer == 0)
                    {
                        ok = 4;timer = 0; a1 = 0;
                    }
                }
            }
            else if(ok==4)    //畫小男孩
            {
                if (timer < 34)
                {
                    if (a1 == 0)
                    {
                        x1 = 400; y1 = 230; width1 = 200; height1 = 287;
                        timer += 1;
                        image1 = images2[timer];
                    }
                    if (timer == 15)
                    {
                        a1 += 1;
                        if (a1 == 15)
                        {
                            a1 = 0;
                        }
                    }
                    if (timer == 34)
                    {
                        ok = 5; a1 = 0;
                    }
                }             
            }
            else if (ok >= 5)//畫小男孩閃爍
            {
                if (ok == 5) { music.mciMusic("littleboy.mp3", "play"); }
                pictureBox2.Visible = true;
                ok = 6;
                a1 += 1;
                switch (a1)
                {
                    case 1: image1 = images2[34]; break;
                    case 6: image1 = images2[35]; a1 = -6; break;
                }               
            }
            g.DrawImage(image1, x1, y1, width1, height1);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
        public void stop()
        {
            images.Clear();
            sttop = true;
            this.Visible = false;
            this.Enabled = false;
            timer1.Stop();
            
        }
    }
}

