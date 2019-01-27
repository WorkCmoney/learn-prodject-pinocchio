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
using 閃避遊戲.Class_Actor;
using 閃避遊戲.control;
using System.Threading;
    
    
    namespace 閃避遊戲
{
    public partial class game_3 : Form
    {
        Game3_player new_player;
        List<Game3_Spider> swords;
        Game3_Spider new_sword1 ;
        Game3_Spider new_sword2 ;
        Game3_flowerup new_sword2up;
        Game3_flowerup new_swordup;
        Game3_flowerup new_sword3up;
        Game3_feather new_feather;
        Game3_road roadd ;
        Game3_heart heart;
        Game3_Hp hpimg;
        Game3_Attack_right new_Arrowright;
      
        win_control3 win_Control = new win_control3();
        lose_control lose_Control = new lose_control();
        Teach3 teach3;
        game3first game3First ;
        bool left, right, jump;
        int x , y, speed, G ;
        int update,update_leveltime ;
        int gameover ;
        int score;
        int gamestop = 0;
        public game_3()
        {
            InitializeComponent();
            
        }
        private void Game_3_Load_1(object sender, EventArgs e)
        {

            new_player = new Game3_player();
            swords = new List<Game3_Spider>();
            new_sword1 = new Game3_Spider(5,500,-300);
            new_sword2 = new Game3_Spider(5,300,-500);
            hpimg = new Game3_Hp();
            new_sword2up = new Game3_flowerup(200,370,141,450);
            new_swordup = new Game3_flowerup(350,670,141,450);
            new_sword3up = new Game3_flowerup(550,450, 141, 450);
            new_feather = new Game3_feather();
            roadd = new Game3_road();

            heart = new Game3_heart();

            new_Arrowright = new Game3_Attack_right();
            //game3_arrowleft new_Arrowleft = new game3_arrowleft();

            x = 350; y = 300; speed = 20; G = 25;
            update = 0; update_leveltime = 0;
            gameover = 0;
            score = 0;
            music.mciMusic("BK.wav", "stop");
            music.mciMusic("3.wav", "play");

            teach3 = new Teach3();
            game3First = new game3first();
            Controls.Add(game3First);
            game3First.BringToFront();
            game3First.Timestart += teachstart;
            teach3.Timestart += timerstart;
            Cursor = new Cursor("pic\\bkmouse.ico");
        }
        
        public void teachstart(object sender, EventArgs e)
        {
            Controls.Add(teach3);
            teach3.BringToFront();           
        }

        public void timerstart(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
            music.mciMusic("BK.wav", "play");
            music.mciMusic("3.wav", "stop");
        }
        private void Stop_Click(object sender, EventArgs e)
        {
            if (gamestop == 0)
            {
                timer1.Stop();gamestop += 1;
            }
            else if (gamestop == 1)
            {
                timer1.Start(); gamestop -= 1;
            }
        }
        private void Game_3_Paint(object sender, PaintEventArgs e)
        {
            /////////////////////////////////繪畫//////////////////////////////
            //tree.draw_img(e.Graphics);
            roadd.draw_img(e.Graphics);
            new_player.draw_img(e.Graphics);

            new_sword1.draw_img(e.Graphics);
            new_sword2.draw_img(e.Graphics);
            new_sword2up.draw_img(e.Graphics);
            new_sword3up.draw_img(e.Graphics);
            new_Arrowright.draw_img(e.Graphics);
            //new_Arrowleft.draw_img(e.Graphics);
            new_swordup.draw_img(e.Graphics);
            new_feather.draw_img(e.Graphics);

            heart.draw_img(e.Graphics);
            hpimg.draw_img(e.Graphics);
        }       
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //////////////////////////////主角移動//////////////////////////////
            if (left) {
                if (update_leveltime >= 4)///////////////場景變短限制移動/////////////////
                {
                    if (x > 500) { x = 500; }
                    if (x < 175) { x = 175; }
                    if (x >= 175 && x <= 500)
                    {
                        x -= speed; new_player.get_right = 0;
                    }
                }
                else
                if (x > 680) { x = 680; }
                if (x < 175) { x = 175; }
                if (x >= 175 && x <= 680)
                {
                    x -= speed; new_player.get_right = 0;
                }
                if (!jump) { new_player.change_img_left(); }               
            }
            if (right) {

                if (update_leveltime >= 4)
                {
                    if (x > 500) { x = 500; }
                    if (x < 175) { x = 175; }
                    if (x >= 175 && x <= 500)
                    {
                        x += speed; new_player.get_right = 1;
                    }
                }
                else
                if (x > 680) { x = 680; }
                if (x < 175) { x = 175; }
                if (x >= 175 && x <= 680)
                {
                    x += speed; new_player.get_right = 1;
                }
                if (!jump) { new_player.change_img_right(); }              
            }
            ////////////////////////////主角跳躍&重力///////////////////////////
            if (jump) { y -= G; G -= 6; new_player.change_img_jump(); if (y >= 300) { y = 300;G = 25;jump = false; new_player.change_img_stand(); } }
            new_player.setCoord(x, y);
            ////////////////////////////各種雜物碰//////////////////////////////
            new_sword1.go_way(); new_player.get_hp -= new_sword1.collision(x, y-100, 140, 140);
            new_sword2.go_way(); new_player.get_hp -= new_sword2.collision(x , y-100 , 140, 140);

            new_sword2up.go_way();
            new_player.get_hp -= new_sword2up.collision(x, y - 170, 140, 130);//width控制右邊碰撞 x+越多 人物左邊越容易碰
            new_swordup.go_way();
            new_player.get_hp -= new_swordup.collision(x, y - 170, 140, 130);
            new_sword3up.go_way();
            new_player.get_hp -= new_sword3up.collision(x, y - 170, 140, 130);

            new_Arrowright.go_way(); new_player.get_hp -= new_Arrowright.collision(x + 50, y + 20, 70, 140);
            //從左邊往右邊
            //new_Arrowleft.go_way(); new_player.get_hp -= new_Arrowleft.collision(x, y+20, 140, 140);

            new_feather.go_way();score += new_feather.collision(x + 70, y + 50, 140, 140);

            /////////////////////////////心臟刷新///////////////////////////////
            heart.change_img(score);
            /////////////////////////////血量刷新///////////////////////////////
            hpimg.How_muchHp(new_player.get_hp);
            /////////////////////////////關卡往上條件///////////////////////////
            update += 1;//計數器 
            if (update >= 100)
            {
                update = 0;
                update_leveltime += 1;
            }  
            ////////////////////////////地板變短////////////////////////////////
            if (update_leveltime == 4)
            {
                speed = 10;
                roadd.setCoord(100, 370);
                roadd.chg_size(600, 200);
                update_leveltime += 1;
                if (x > 500) { x = 500; }
                if (x < 175) { x = 175; }
            }
            ////////////////////////////遊戲勝利條件//////////////////////////// 
            if (score>15)
            {
                level_control.game3_unlock = 1;
                level_control.timer = true;
                timer1.Stop();
                Controls.Add(win_Control);
                win_Control.BringToFront();
                Endtime.Start();
                music.mciMusic("win.wav", "play");
                music.mciMusic("3.wav", "stop");
            }
            ////////////////////////////遊戲失敗條件////////////////////////////
            if (new_player.get_hp <=-1)
            {
                gameover = 1;
            }
            if (gameover == 1)
            {

                timer1.Stop();
                Controls.Add(lose_Control);
                lose_Control.BringToFront();
                Endtime.Start();
                music.mciMusic("gameover.wav", "play");
                music.mciMusic("3.wav", "stop");
            }
            ///////////////////////////////GDI刷新///////////////////////////////
            Invalidate();
            //label1.Text = new_feather.left + "";
        }
        private void Endtime_Tick(object sender, EventArgs e)
        {
            if (lose_control.over == 1 || win_control3.over == 1)
            {
                Endtime.Stop();
                lose_control.over = 0;
                win_control3.over = 0;
                this.Close();
            }
        }
        private void Game_3_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: left = true; break;
                case Keys.Right: right = true; break;
                case Keys.Up: jump = true; new_player.change_img_jump();/* music.mciMusic("jump.wav", "play");*/ break;
            }
        }
        private void Game_3_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: left = false;new_player.change_img_stand(); break;
                case Keys.Right: right = false; new_player.change_img_stand(); break;                
            }
        }
    }
}
