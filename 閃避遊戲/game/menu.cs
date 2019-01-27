using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Media;

namespace 閃避遊戲
{
    public partial class menu : Form
    {
        static string palyername;
        Meautext title = new Meautext();       
        level_control level_page = new level_control();
        starttext starttext = new starttext();
        Actortext actortext = new Actortext();
        what_to_do what_To_Do = new what_to_do();
        skip skip = new skip();
        int n=0;
        public menu()
        {
            InitializeComponent();
            Program.meau = this;
            timer1.Start();
            //Start_lab.Visible = false;
            //label1.Visible = false;
            //textBox1.Visible = false;
        }
        private void meau_Load(object sender, EventArgs e)
        {
            music.mciMusic("BK.wav", "play");
            Cursor = new Cursor("pic\\mouse.ico");

            Controls.Add(level_page);
            level_page.Visible = false;
            level_page.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(level_page, true, null);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            title.draw_img(e.Graphics);         
            if (title.ok >0)
            {
                starttext.draw_img(e.Graphics);
            }
            actortext.draw_img(e.Graphics);

            if (starttext.ok >= 2)
            {
                skip.draw_img(e.Graphics);
            }
            //if (title.ok == 4)
            //{
            //    drawline.draw_img(e.Graphics);
            //}
            if (title.ok == 5)
            {
                what_To_Do.draw_img(e.Graphics);
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
            if (title.ok == 4)
            {
                //textBox1.Visible = true;
                n = 1;
                //label1.Visible = true;
            }          
        }            
        private void Exit_lab_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            if (starttext.ok == 1)//開始往後刪除
            {
                title.ok = 2;
                starttext.ok = 2;
            }
            if (title.ok == 4)
            {
                //textBox1.Visible = true;
                //n = 1;
                //label1.Visible = true;
            }
            if (n == 1 && title.ok == 4)
            {
                level_page.Visible = true;
                level_page.BringToFront();
                timer1.Stop();
            }
        }
        private void menu_MouseDown(object sender, MouseEventArgs e)
        {
            if (skip.isClick(e.X, e.Y))
            {
                music.mciMusic("click1.wav", "play");
                //palyername = textBox1.Text;
                //textBox1.Enabled = false;
                level_page.Visible = true;
                level_page.BringToFront();
                timer1.Stop();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

