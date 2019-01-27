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
using 閃避遊戲.control;

namespace 閃避遊戲
{
    public partial class level_control : UserControl
    {
        public static int game1_unlock = 0; //1=解鎖
        public static int game2_unlock = 0;
        public static int game3_unlock = 0;
        public static bool timer;

        Record Record = new Record();//存檔
        levelGAME1 levelGAME1 = new levelGAME1();//GDI畫圖 +判斷點
        levelGAME2 LevelGAME2 = new levelGAME2();
        levelGAME3 levelGAME3 = new levelGAME3();
        End end;

        int time = 0;
        public level_control()
        {
            InitializeComponent();
            Record.demo_reader();
            game1_unlock = Record.game1;
            game2_unlock = Record.game2;
            game3_unlock = Record.game3;
            timer1.Start();
          
        }
        private void level_Load(object sender, EventArgs e)
        {        
            pictureBox5.Visible = true; pictureBox6.Visible =true; pictureBox7.Visible = true;
            Cursor = new Cursor("pic\\bkmouse.ico");
        }
        private void pictureBox_Click_(object sender, EventArgs e)
        {          
            if (sender == pictureBox4)
            {
                this.Visible = false;
                timer1.Stop();
                Program.meau.timer1.Start();
            }
        }
        private void level_control_Paint(object sender, PaintEventArgs e)
        {
            levelGAME1.draw_img(e.Graphics);
            LevelGAME2.draw_img(e.Graphics);
            levelGAME3.draw_img(e.Graphics);
        }
        private void level_control_MouseDown(object sender, MouseEventArgs e)
        {
            ///////觸碰到第一關////////
            if (levelGAME1.isClick(e.X, e.Y))
            {
                music.mciMusic("click1.wav", "play");
                Game1 new_game = new Game1();
                timer1.Stop();
                new_game.ShowDialog();
                new_game.Dispose();
                GC.Collect();
                timer1.Start();
                if (timer)
                {                  
                     Record.demo_writer(game1_unlock, game2_unlock, game3_unlock);
                }
            }
            ///////觸碰到第二關////////

            if (LevelGAME2.isClick(e.X, e.Y))
            {
                music.mciMusic("click1.wav", "play");
                game2 game2 = new game2();
                timer1.Stop();
                game2.ShowDialog();
                game2.Dispose();
                GC.Collect();
                timer1.Start();
                if (timer)
                {                   
                    Record.demo_writer(game1_unlock, game2_unlock, game3_unlock);
                }
            }
            ///////觸碰到第三關////////
            if (levelGAME3.isClick(e.X, e.Y))
            {
                music.mciMusic("click1.wav", "play");
                game_3 new_game = new game_3();
                timer1.Stop();
                new_game.ShowDialog();
                new_game.Dispose();
                GC.Collect();
                timer1.Start();
                if (timer)
                {                 
                     Record.demo_writer(game1_unlock, game2_unlock, game3_unlock);
                }
            }      
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
            if (game1_unlock == 1) //解鎖
            {
                pictureBox5.Visible = false;               
            }
            if(game2_unlock == 1)
            {
                pictureBox6.Visible = false;
            }
            if(game3_unlock == 1)
            {
                pictureBox7.Visible = false;
            }
            if (time == 0)
            {
                if (game1_unlock == 1 && game2_unlock == 1 && game3_unlock == 1)
                {                  
                    time += 1;//進入最終 勝利畫面
                }
            }
            if (time == 1)
            {
                end = new End();
                game3_unlock = 0;
                Record.demo_writer(game1_unlock, game2_unlock, game3_unlock);
                Controls.Add(end);
                end.BringToFront();
                end.Timestart += levelstart;
                time = 2;
            }
        }
        public void levelstart(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        private void level_control_MouseMove(object sender, MouseEventArgs e)
        {
            if (levelGAME1.isClick(e.X, e.Y))   //碰觸關卡放大縮小
            {
                levelGAME1.width = 130;
                levelGAME1.height = 150;
                levelGAME1.get_x = 180;
            }
            if(!(levelGAME1.isClick(e.X, e.Y)))
            {
                levelGAME1.width = 120;
                levelGAME1.height = 140;
                levelGAME1.get_x = 185;
            }           
            if (LevelGAME2.isClick(e.X, e.Y))
            {
                LevelGAME2.width = 190;
                LevelGAME2.height = 320;
                LevelGAME2.get_x = 390;
            }
            if (!(LevelGAME2.isClick(e.X, e.Y)))
            {
                LevelGAME2.width = 180;
                LevelGAME2.height = 310;
                LevelGAME2.get_x = 395;
            }
            if (levelGAME3.isClick(e.X, e.Y))
            {              
                levelGAME3.width = 210;
                levelGAME3.height = 290;
                levelGAME3.get_x = 645;
            }
            if (!(levelGAME3.isClick(e.X, e.Y)))
            {
                levelGAME3.width = 200;
                levelGAME3.height = 280;
                levelGAME3.get_x = 650;
            }
        }
        private void end_pic_Click(object sender, EventArgs e)
        {
            end = new End();
            Controls.Add(end);
            end.BringToFront();
            end.Timestart += levelstart;
        }
    }
    
}
