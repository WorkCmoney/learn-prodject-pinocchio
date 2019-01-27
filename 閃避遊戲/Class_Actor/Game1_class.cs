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
using 閃避遊戲.control;
using System.Media;

namespace 閃避遊戲.game
{
    class Game1_player : Drawing_objects
    {
        public Game1_player()
        {
            x = 430;
            y = 350;
            width = 120;
            height = 120;
            img0 = new Bitmap("pic\\game1\\EYE.png");
            imgPainted = img0;
        }
        public Game1_player(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }
    /// <summary>
    /// 小球
    /// </summary>
    class Game1_monster1 : Drawing_objects, Icollision
    {
        int dis = 0, move_speed = 10;
        int right, left, up, down;
        int first_left = 0, first_down = 0, first_time = 0;
        Image img1;
        public Game1_monster1()
        {
            img0 = new Bitmap("pic\\game1\\怪物A-開口.png");
            img1 = new Bitmap("pic\\game1\\閉口.png");
            x = 270;
            y = 240;
            width = 60;
            height = 60;
            imgPainted = img0;
        }
        public Game1_monster1(int x, int y, int width, int height)
        {
            img0 = new Bitmap("pic\\game1\\怪物A-開口.png");
            imgPainted = img0;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public int get_speed
        {
            get { return move_speed; }
            set { move_speed = value; }
        }
        public void go_way(int x, int y)
        {
            if (dis == 0)//一開始行走的方向
            {
                if (x > this.x)
                {
                    this.x -= move_speed;
                    first_left = 1;
                }
                if (x < this.x)
                {
                    this.x += move_speed;
                    first_left = 0;
                }
                if (y > this.y)
                {
                    this.y -= move_speed;
                    first_down = 0;
                }
                if (y < this.y)
                {
                    this.y += move_speed;
                    first_down = 1;
                }
            }
            if (this.x >= 900)//碰到右方牆壁轉向
            {
                dis = 1;
                left = 1;
                right = 0;
                if (first_down == 1 && first_time == 0)
                {
                    down = 1; up = 0; first_time = 1;
                }
                if (first_down == 0 && first_time == 0)
                {
                    down = 0; up = 1; first_time = 1;
                }
            }
            if (this.x <= -5) //碰到左方牆壁轉向
            {
                dis = 1;
                right = 1;
                left = 0;
                if (first_down == 1 && first_time == 0)
                {
                    down = 1; up = 0; first_time = 1;
                }
                if (first_down == 0 && first_time == 0)
                {
                    down = 0; up = 1; first_time = 1;
                }
            }
            if (this.y >= 680)//碰到下方牆壁轉向
            {
                dis = 1;
                up = 1;
                down = 0;
                if (first_left == 1 && first_time == 0)
                {
                    left = 1; right = 0; first_time = 1;
                }
                if (first_left == 0 && first_time == 0)
                {
                    left = 0; right = 1; first_time = 1;
                }
            }
            if (this.y <= 0)//碰到上方牆壁轉向
            {
                dis = 1;
                up = 0;
                down = 1;
                if (first_left == 1 && first_time == 0)
                {
                    left = 1; first_time = 1; right = 0; right = 0;
                }
                if (first_left == 0 && first_time == 0)
                {
                    left = 0; first_time = 1; right = 1;
                }
            }
            if (left == 1) { this.x -= move_speed; } //碰牆壁後的轉向控制
            if (right == 1) { this.x += move_speed; }
            if (up == 1) { this.y -= move_speed; }
            if (down == 1) { this.y += move_speed; }
        }
        public void first_location(int x, int y) //小球初始生成位置
        {
            switch (random.Next(8) + 1)
            {
                case 1:
                    this.x = x + 120;
                    this.y = y + random.Next(30) + 1; break;
                case 2:
                    this.x = x + 120;
                    this.y = y - random.Next(30) + 1; break;
                case 3:
                    this.x = x - 120;
                    this.y = y + random.Next(30) + 1; break;
                case 4:
                    this.x = x - 120;
                    this.y = y - random.Next(30) + 1; break;
                case 5:
                    this.x = x - random.Next(30) + 1;
                    this.y = y + 120; break; 
                case 6:
                    this.x = x - random.Next(30) + 1;
                    this.y = y + 120; break;
                case 7:
                    this.x = x + random.Next(30) + 1;
                    this.y = y - 120; break;
                case 8:
                    this.x = x - random.Next(30) + 1;
                    this.y = y - 120; break;
            }
        }
        public int collision(int x, int y, int width, int height)
        {
            if (Math.Sqrt(Math.Pow((this.x + this.width / 2) - x, 2) + Math.Pow((this.y + this.width / 2) - y, 2)) < this.width / 2 + width / 2)
            {
                return 1;
            }
            return 0;
        }
        public override void draw_img(Graphics g)
        {
            if (imgPainted != null)
            {
                g.DrawImage(imgPainted, x, y, width, height);
            }
        }
        public void chg_img()
        {
            imgPainted = (imgPainted == img0) ? img1 : img0;
        }
    }
    class Game1_eatball : Drawing_objects,Icollision
    {
        public Game1_eatball()
        {
            img0 = new Bitmap("pic\\game1\\shoes.png");
            x = random.Next(100, 700);
            y = random.Next(150, 550);
            width = 78; height = 65;
            imgPainted = img0;
        }
        public Game1_eatball(int x, int y, int width, int height)
        {
            img0 = new Bitmap("pic\\game1\\shoes.png");
            imgPainted = img0;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public int collision(int x, int y, int width, int height)
        {
            if (Math.Sqrt(Math.Pow((this.x + this.width / 2) - x, 2) + Math.Pow((this.y + this.width / 2) - y, 2)) < this.width / 2 + width / 2)
            {
                return 1;
            }
            return 0;
        }
    }
    
    

    
}
