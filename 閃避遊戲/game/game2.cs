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
using System.Threading;
using 閃避遊戲.Class_Actor;
using 閃避遊戲.control;
using 閃避遊戲.game;

namespace 閃避遊戲
{
    public partial class game2 : Form
    {
        int wall_weight;
        int wall_height;
        win_control2 win_Control = new win_control2();
        lose_control lose_Control = new lose_control();
        Game2_playerball new_ball;
        Game2_wall buttom_wall;
        Game2_wall top_wall;
        Game2_wall right_wall;
        Game2_wall left_wall;
        game2first game2First;
        Game2_brain brain1 ;
        List<Game2_eatball> eatball_list;
        List<Game2_canteatball> canteatball_list;
        Teach2 teach2;

        game2p game2P;
        int stoptime = 0;
        int x = 400, y = 60, gameover = 0, score = 0;
        int new_eat_ball_timer = 0;
        int new_cant_eatball_timer = 0;
        bool left,right, control = true;
        int touch = 0;
        public static bool one_player=false;

        public game2()
        {
            InitializeComponent();
            Controls.Add(lose_Control);
            lose_Control.BringToFront(); lose_Control.Visible = false;
        }
        private void Ball2_Load(object sender, EventArgs e)
        {      
            //onetwotimer.Start();

            wall_weight =150;
            wall_height=35;
            
            new_ball = new Game2_playerball();
            buttom_wall = new Game2_wall(1, 400, 600, wall_weight, wall_height);
            top_wall = new Game2_wall(1, 400, 100, wall_weight, wall_height);
            right_wall = new Game2_wall(0, 850, 300, wall_height, wall_weight);
            left_wall = new Game2_wall(0, 60, 300, wall_height, wall_weight);
            brain1 = new Game2_brain();
           
            eatball_list = new List<Game2_eatball>();
            canteatball_list = new List<Game2_canteatball>();

            new_ball.first_location();

            teach2 = new Teach2();
            game2First = new game2first();
            Controls.Add(game2First);
            game2First.BringToFront();
            game2First.Timestart += teachstop;
            teach2.Timestart += first_story_stop;

            music.mciMusic("BK.wav", "stop");
            music.mciMusic("2.wav", "play");
            Cursor = new Cursor("pic\\bkmouse.ico");
        }
        public void teachstop(object sender, EventArgs e)
        {          
            Controls.Add(teach2);
            teach2.BringToFront();
        }
        public void first_story_stop(object sender, EventArgs e)
        {
            game2P = new game2p();
            Controls.Add(game2P);
            game2P.BringToFront();
            game2P.down = false;
            game2P.Timestart += Timestart;
        }
        public void Timestart(object sender, EventArgs e)
        {
            process_time.Start();          
        }
        private void Ball2_Paint(object sender, PaintEventArgs e)
        {
            new_ball.draw_img(e.Graphics);
            ///////////////wall////////////
            buttom_wall.draw_img(e.Graphics);
            top_wall.draw_img(e.Graphics);
            right_wall.draw_img(e.Graphics);
            left_wall.draw_img(e.Graphics);

            brain1.draw_img(e.Graphics);
            if (eatball_list != null)
            {
                foreach (Game2_eatball n in eatball_list.ToArray())
                {
                    n.draw_img(e.Graphics);
                    /////////////////////白球與毛線球碰撞判定//////////////////////////
                    touch = n.collision(new_ball.get_x, new_ball.get_y,new_ball.width,new_ball.height);
                    if (touch == 1)
                    { 
                        music.mciMusic("touch.wav", "play");
                        score += 2;
                        //score_lable.Text = "Score " + score;
                        touch = 0;
                        /////////////////////當碰到後把白球移除//////////////////////////
                        eatball_list.Remove(n);
                        brain1.score += 2;
                    }
                }
            }
            if (canteatball_list != null)
            {
                foreach (Game2_canteatball n in canteatball_list.ToArray())
                {
                    
                    n.draw_img(e.Graphics);
                    /////////////////////混亂球與毛線球碰撞判定//////////////////////////
                    touch = n.collision(new_ball.get_x, new_ball.get_y, new_ball.width, new_ball.height);
                    if (touch == 1)
                    {
                        if (control)
                        {
                            control = false;
                        }
                        else if (!control)
                        {
                            control = true;
                        }
                        //score_lable.Text = "Score " + score;
                        touch = 0;
                        /////////////////////當碰到後把混亂球移除//////////////////////////
                        canteatball_list.Remove(n);
                    }
                }
            }
        }
        private void Ball2_MouseMove(object sender, MouseEventArgs e)
        {
           if (one_player)
            {
                    x = e.Location.X; /*tempX = x;*/              
            }           
            y = e.Location.Y;            
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            process_time.Stop(); music.mciMusic("BK.wav", "play");
            music.mciMusic("2.wav", "stop"); this.Close();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            if (stoptime == 0)
            {
                process_time.Stop();
                stoptime += 1;
            }
            else if (stoptime == 1)
            {
                process_time.Start();
                stoptime -= 1;
            }
        }
        private void process_time_Tick(object sender, EventArgs e)
        {
            Invalidate();
            
            if (!one_player)
            {
                if (control)
                {
                    if (left)
                    {
                        x -= 60;
                    }
                    if (right)
                    {
                        x += 60;
                    }
                }
                else if (!control)
                {
                    if (left)
                    {
                        x += 60;
                    }
                    if (right)
                    {
                        x -= 60;
                    }
                }
            }
/////////////////////////////////當白球場上數量為0的時候//////////////
            if (eatball_list.Count == 0)
            {
                new_eat_ball_timer += 1;////開始倒數,開始增加小白球///
                if (new_eat_ball_timer == 30)
                {
                    Game2_eatball new_eat_ball = new Game2_eatball();
                    eatball_list.Add(new_eat_ball);
                    new_eat_ball_timer = 0;
                }
            }
            /////////////////////////////當混亂球場上數量為0的時候//////////////
            if (canteatball_list.Count == 0&& !one_player)
            {
                new_cant_eatball_timer += 1;////開始倒數,開始增加混亂球///
                if (new_cant_eatball_timer == 40)
                {
                    Game2_canteatball new_canteat_ball = new Game2_canteatball();
                    canteatball_list.Add(new_canteat_ball);
                    new_cant_eatball_timer = 0;
                }
            }
            ////////////////////////////四邊擋板移動/////////////////////////////
            buttom_wall.setCoord(x,110);
            top_wall.setCoord(x,590);
            right_wall.setCoord(840, y);
            left_wall.setCoord(70, y);           
///////////////////////////碰撞+邊界判定條件////////////////////////           
            top_wall.touch = new_ball.collision(3,x+45,570,100,1);//buttom    //判斷碰撞畫圖&反彈
            buttom_wall.touch= new_ball.collision(1,x+45,110,100,1);//top
            right_wall.touch = new_ball.collision(2,820,y+45,1,100);//right
            left_wall.touch= new_ball.collision(4,40,y+45,1,100);//left
            //label1.Text = x.ToString();    
            //score_lable.Text = "Score " + brain1.score;
            //if (top_wall.touch==1 || buttom_wall.touch==1 || right_wall.touch==1 || left_wall.touch == 1)
            //{
               
            //}
            brain1.chg();
            Invalidate();

            gameover = new_ball.collision(0,80, y, 10, 140);//判斷是否遊戲結束
/////////////////////////畫圖判斷///////////////////////////////////
            top_wall.chg_pic(); buttom_wall.chg_pic(); right_wall.chg_pic(); left_wall.chg_pic();
/////////////////////////失敗條件///////////////////////////////////
            if (gameover == 2)
            {
                 process_time.Stop();
                lose_Control.Visible = true;
                Endtime.Start();
                music.mciMusic("gameover.wav", "play");
                music.mciMusic("2.wav", "stop");
            }                      
/////////////////////////勝利條件////////////////////////////////////
            if (score >= 12)
            {
                process_time.Stop(); 
                level_control.game2_unlock = 1;
                level_control.timer = true;
                Controls.Add(win_Control);
                win_Control.BringToFront();
                Endtime.Start();
                music.mciMusic("win.wav", "play");
                music.mciMusic("2.wav", "stop");
            }            
        }
        private void Endtime_Tick_(object sender, EventArgs e)//勝利or失敗介面
        {
            if (lose_control.over==1||win_control2.over==1)
            {
                Endtime.Stop();                
                lose_control.over = 0;
                win_control2.over = 0;
                this.Close();
            }         
        }
        private void Game2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {   
                case Keys.Left: left = true;break;
                case Keys.Right:right = true;break;
            }           
        }
        private void Game2_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: left = false; break;
                case Keys.Right: right = false; break;
            }
        }
    }
}
