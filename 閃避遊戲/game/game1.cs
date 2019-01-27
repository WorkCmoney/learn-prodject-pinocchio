using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using 閃避遊戲.game;
using 閃避遊戲.control;

namespace 閃避遊戲
{
    public partial class Game1 : Form
    {
        Random random;
        Game1_player player;
        List<Game1_monster1> ball_list ;
        List<Game1_eatball> eatball_list ;
        game1first game1First;
        win_control1 win_Control = new win_control1();
        lose_control lose_Control = new lose_control();
        Teach1 teach1;
        Teach12 teach12;
        Teach13 teach13;
        Image img0;
        List<Image> start=new List<Image>();  
   
        int x ,y ;
        int gameover;
        int winner;
        int next_leveltimer ;
        int level;
        int creat_ball_time;
        int show_leveltime ;
        int new_eat_ball_timer;
        int touch;
        int eat_score;
        int stoptime = 0;
        public Game1()
        {
            InitializeComponent();
            for (int i = 0; i <= 6; i++)
            {
                img0 = new Bitmap("pic\\game1\\倒數\\" + i + ".png");
                start.Add(img0);
            }
        }
        private void Game1_Load(object sender, EventArgs e)
        {
            pictureBox3.Image = start[4];
            pictureBox2.Image = start[2];
            music.mciMusic("BK.wav", "stop");
            music.mciMusic("1.wav", "play");
            random = new Random();
            player = new Game1_player();
            ball_list = new List<Game1_monster1>();
            eatball_list = new List<Game1_eatball>();
            teach1 = new Teach1();
            teach12 = new Teach12();
            teach13 = new Teach13();
            x = 430; y = 350;
            gameover = 0;
            winner = 0;
            next_leveltimer = 0;
            level = 1;
            creat_ball_time = 0;
            show_leveltime = 0;
            new_eat_ball_timer = 0;          
            eat_score = 0;
         
            game1First = new game1first();
            Controls.Add(game1First);
            Controls.Add(teach1);
            game1First.BringToFront();

            game1First.teachstart += teach1start;
            teach1.Timestart += teach2start;
            teach12.Timestart += teach3start;
            teach13.Timestart += levelstart;
            
            Cursor = new Cursor("pic\\bkmouse.ico");
        }      
        public void levelstart(object sender, EventArgs e)
        {
            leveltime.Start();         
        }
        public void teach1start(object sender, EventArgs e)
        {          
            teach1.BringToFront();
        }
        public void teach2start(object sender, EventArgs e)
        {
            Controls.Add(teach12);
            teach12.BringToFront();
        }
        public void teach3start(object sender, EventArgs e)
        {
            Controls.Add(teach13);
            teach13.BringToFront();
        }
        private void Game_start_MouseDown(object sender, MouseEventArgs e)
        {
            if (process_time.Enabled&& game1First.sttop)
            {               
                x = e.Location.X - 60;
                y = e.Location.Y - 60; 
                if (creat_ball_time % 1 == 0)//每點三下會有一顆球
                {
                    if (level == 1&& next_leveltimer <= 7)//第一關球數生成
                        {                        
                            Game1_monster1 moster = new Game1_monster1();
                            moster.first_location(x, y);     
                            ball_list.Add(moster);
                            music.mciMusic("monst01.wav", "play");
                    }                   
                    if (level == 2 && next_leveltimer <=7)//第二關球數生成
                        {                           
                            Game1_monster1 moster1 = new Game1_monster1();
                            moster1.get_speed = 15;
                            moster1.first_location(x, y);
                            ball_list.Add(moster1);
                            music.mciMusic("monst01.wav", "play");
                    }
                    if (level == 3 && next_leveltimer <= 5)//第三關球數生成
                        {
                            music.mciMusic("monst01.wav", "play");
                            Game1_monster1 moster1 = new Game1_monster1();
                            moster1.get_speed = 16;
                            moster1.first_location(x, y);
                            ball_list.Add(moster1);
                            Game1_monster1 moster2 = new Game1_monster1();
                            moster2.get_speed = 16;
                            ball_list.Add(moster2);
                            moster2.first_location(x, y);
                        }                                                                        
                    next_leveltimer += 1;
                }
                creat_ball_time += 1;
            }
        }
       private void Game_start_Paint(object sender, PaintEventArgs e)
        {
                if (process_time.Enabled)
                {
                    player.draw_img(e.Graphics);//graphics
                    for (int n = ball_list.Count - 1; n >= 0; n--)
                    {
                        ball_list[n].draw_img(e.Graphics);
                    }
                    if (eatball_list != null)
                    {
                        foreach (Game1_eatball n in eatball_list.ToArray())
                        {
                            n.draw_img(e.Graphics);
                            /////////////////////眼睛與小眼睛碰撞判定//////////////////////////
                            touch = n.collision(x + 60, y + 60, 120, 120);
                            if (touch == 1)
                            {
                                eat_score += 1;
                                //score_lable.Text = "" + eat_score;
                                touch = 0;
                                /////////////////////當碰到後把白球移除//////////////////////////
                                eatball_list.Remove(n);
                            }
                        }
                    }
                }              
        }
        private void Showleveltime(object sender, EventArgs e)//控制開始倒數的時間
        {
            show_leveltime += 1;
            process_time.Stop();
            timeimg.Stop();
            switch (show_leveltime)
            {
                case 1: pictureBox3.Visible = true; player.setCoord(800, 1200); Invalidate(); break;
                case 10: pictureBox3.Visible = false; pictureBox3.Image= start[3]; pictureBox2.Visible = true; break;
                case 15: pictureBox2.Visible = false; pictureBox2.Image = start[1]; break;
                case 20: pictureBox2.Visible = true; break;
                case 25: pictureBox2.Visible = false; pictureBox2.Image = start[0]; break;
                case 30: pictureBox2.Visible = true; break;
                case 35: pictureBox2.Visible = false; pictureBox2.Image = start[2]; break;//倒數條回原本3關閉
                case 40: pictureBox3.Visible = true; break;//start標語打開
                case 45:pictureBox3.Visible = false; leveltime.Stop(); process_time.Start(); timeimg.Start();
                                                                   show_leveltime = -0;x = 430;y = 350;break;
            }
        }
        ////////小球換圖片單獨控制時間/////////
        private void Timeimg_Tick(object sender, EventArgs e)
        {
                for (int n = ball_list.Count - 1; n >= 0; n--)
                {
                    ball_list[n].chg_img();
                }
        }
        private void process_Tick(object sender, EventArgs e)
        {
            //label5.Text = "" + winner;
            player.setCoord(x, y);
            for (int n = ball_list.Count - 1; n >= 0; n--)//每顆攻擊怪物目前狀態
            {
                ball_list[n].go_way(x, y);//移動位置
                gameover = ball_list[n].collision(x+60, y+60,120,120);//碰撞即遊戲結束
                if (gameover == 1) { break; }
            }
            //label6.Text = "" + gameover;
            ////////////////////////////////生成收集眼睛////////////////////////////////////////
            if (eatball_list.Count == 0)
            {
                new_eat_ball_timer += 1;//開始倒數,開始增加眼睛
                if (new_eat_ball_timer == 30)
                {
                    Game1_eatball new_eat_ball = new Game1_eatball();
                    eatball_list.Add(new_eat_ball);
                    new_eat_ball_timer = 0;
                }
            }           
            //////////////////準備進入下關清除怪物 重新開始//////////////////////////
            if (eat_score >= 5)
            {              
                foreach (Game1_monster1 circle in ball_list.ToArray())
                {
                    ball_list.Remove(circle);
                }                               
                next_leveltimer = 0;
                eat_score = 0;
                level += 1;
                if (level == 4) { winner = 1;}
                if (level < 4)
                {
                    switch (level)
                    {
                        case 2: pictureBox3.Image=start[5]; break;
                        case 3: pictureBox3.Image = start[6]; break;
                    }                                             
                    leveltime.Start();
                }
            }
            Invalidate();
            ////////////////////////////勝利條件/////////////////////////////////
            if (winner==1)
            {
                process_time.Stop(); leveltime.Stop();
                level_control.game1_unlock = 1;
                level_control.timer = true;
                Controls.Add(win_Control);
                win_Control.BringToFront();
                Endtime.Start();
                music.mciMusic("1.wav", "stop");
                music.mciMusic("win.wav", "play");
            }
            ////////////////////////////////失敗條件///////////////////////////////////
            if (gameover == 1)
            {
                process_time.Stop(); leveltime.Stop();
                Controls.Add(lose_Control);
                lose_Control.BringToFront();
                Endtime.Start();
                music.mciMusic("1.wav", "stop");
                music.mciMusic("gameover.wav", "play");
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {      
            if (stoptime == 0)
            {
                process_time.Stop(); timeimg.Stop();
                stoptime += 1;
            }
            else if (stoptime == 1)
            {
                process_time.Start(); timeimg.Start();
                stoptime -= 1;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Endtime_Tick(object sender, EventArgs e)
        {
            if (lose_control.over == 1 || win_control1.over == 1)
            {
                Endtime.Stop();
                lose_control.over = 0;
                win_control1.over = 0;
                
                this.Close();
            }
        }
    }
}
