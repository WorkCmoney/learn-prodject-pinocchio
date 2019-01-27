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

namespace 閃避遊戲.Class_Actor
{
    class Game2_playerball : Drawing_objects
    {
        int dir = 0, move_speed = 3;
        int left, right, up, down, stoptime = 0;
        bool touchup = false, touchdown = false, touchleft = false, touchright = false;
        int dis_add_time = 0;
        public int distance = 0;

        public Game2_playerball()
        {
            Image img0 = new Bitmap("pic\\game2\\毛線球.png");
            imgPainted = img0;
            x = random.Next(500) + 200;
            y = random.Next(200) + 200;
            width = 60;
            height = 60;
        }
        public Game2_playerball(int x, int y, int width, int height)
        {
            Image img0 = new Bitmap("pic\\game2\\毛線球.png");
            imgPainted = img0;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public void first_location()
        {
            if (dir == 0)
            {
                if (random.Next(2) + 1 == 1) { left = 1; right = 0; }
                else left = 0; right = 1;
            }
            if (dir == 0)
            {
                if (random.Next(2) + 1 == 1) { up = 1; down = 0; }
                else up = 0; down = 1;
            }
        }
        public int collision(int dir, int x, int y, int width, int height)
        {
            if (!touchup && !touchdown && !touchleft && !touchright)
            {
                if ((Math.Abs(this.x - x) < this.width / 2 + width / 2 && Math.Abs(this.y - y) < this.height / 2 + height / 2))
                {
                    if (y == 570 && dir == 3)
                    {
                        down = 0; up = 0; touchdown = true;

                    }
                    else if (x == 820 && dir == 2)
                    {
                        left = 0; right = 0; touchright = true;

                    }
                    else if (y == 110 && dir == 1)
                    {
                        down = 0; up = 0; touchup = true;

                    }
                    else if (x == 40 && dir == 4)
                    {
                        right = 0; left = 0; touchleft = true;
                    }
                    //回傳碰撞到                
                }
            }
            if (this.x < -25 || this.x > 950 || this.y < -25 || this.y > 710)
            {
                return 2;//判斷回傳gamover
            }

            if (touchright || touchleft || touchdown || touchup)
            {
                if (touchright && dir == 2)
                {
                    stoptime += 1;
                    if (stoptime >= 1)
                    {
                        touchright = false; left = 1; right = 0; stoptime = 0; distance = 0; dis_add_time = 0;
                        if (y  >= this.y)
                        {
                            down = 0; up = 1;
                        }
                        else { down = 1; up = 0; }
                        //else if(y + 30 < this.y) { down = 1; up = 0; }
                    }
                    return 1;
                }
                else if (touchleft && dir == 4)
                {
                    stoptime += 1;

                    if (stoptime >= 1)
                    {
                        touchleft = false; left = 0; right = 1; stoptime = 0; distance = 0; dis_add_time = 0;
                        if (y  >= this.y)
                        {
                            down = 0; up = 1;
                        }
                        else { down = 1; up = 0; }
                        //else if (y + 30 < this.y) { down = 1; up = 0; }
                    }
                    return 1;
                }
                else if (touchdown && dir == 3)
                {
                    stoptime += 1;
                    if (stoptime >= 1)
                    {
                        touchdown = false; up = 1; down = 0; stoptime = 0; distance = 0; dis_add_time = 0;
                        if (x <= this.x)
                        {
                            right = 1; left = 0;
                        }
                        else { left = 1; right = 0; }
                    }
                    return 1;
                }
                else if (touchup && dir == 1)
                {

                    stoptime += 1;
                    if (stoptime >= 1)
                    {
                        touchup = false; up = 0; down = 1; stoptime = 0; distance = 0; dis_add_time = 0;
                        if (x  <= this.x)
                        {
                            right = 1; left = 0;
                        }
                        else { left = 1; right = 0; }
                    }
                    return 1;
                }
            }
            if (!touchup && !touchdown && !touchleft && !touchright)
            {
                if (up == 1) { down = 0; this.y -= move_speed; }
                if (down == 1) { up = 0; this.y += move_speed; }
                if (right == 1) { left = 0; this.x += move_speed; }
                if (left == 1) { right = 0; this.x -= move_speed; }
            }

            return 0;
        }
    }
    class Game2_wall : Drawing_objects
    {
        int Up1;
        public int touch { get; set; }
        int chg1, chg2;
        bool touch1;
        public Game2_wall()
        {
            img0 = new Bitmap("pic\\pin.png");
            imgPainted = img0;
            x = random.Next(200) + 100;
            y = random.Next(200) + 100;
            width = 36;
            height = 36;
        }
        public Game2_wall(int up, int x, int y, int width, int height)
        {
            if (up == 0)
            {
                Up1 = 0;
                img0 = new Bitmap("pic\\game2\\針A.png");
                imgPainted = img0;
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
            }
            if (up == 1)
            {
                Up1 = 1;
                img0 = new Bitmap("pic\\game2\\針Aup.png");
                imgPainted = img0;
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
            }
        }
        public void chg_pic()
        {
            if (touch == 1)
            {
                touch1 = true;
            }
            if (Up1 == 0 && touch1)
            {
                switch (chg1)
                {
                    case 0: chg1 += 1; imgPainted = new Bitmap("pic\\game2\\針B.png"); break;
                    case 1: chg1 += 1; imgPainted = new Bitmap("pic\\game2\\針C.png"); break;
                    case 2: chg1 -= 2; imgPainted = new Bitmap("pic\\game2\\針A.png"); touch1 = false; break;

                }
            }
            else if (Up1 == 1 && touch1)
            {
                switch (chg2)
                {
                    case 0: chg2 += 1; imgPainted = new Bitmap("pic\\game2\\針Bup.png"); break;
                    case 1: chg2 += 1; imgPainted = new Bitmap("pic\\game2\\針Cup.png"); break;
                    case 2: chg2 -= 2; imgPainted = new Bitmap("pic\\game2\\針Aup.png"); touch1 = false; break;
                }
            }
        }
    }
    class Game2_brain : Drawing_objects
    {
        int n = 2;
        public int score { get; set; }
        public Game2_brain()
        {
            x = 780; y = 40; width = 90; height = 80;
            img0 = new Bitmap("pic\\game2\\腦0.png");
            score = 0;
            imgPainted = img0;
        }
        public void chg()
        {

            switch (score)
            {
                case 1: img0 = new Bitmap("pic\\game2\\腦" + score + ".png"); break;
                case 2: img0 = new Bitmap("pic\\game2\\腦" + score + ".png"); break;
                case 3: img0 = new Bitmap("pic\\game2\\腦" + score + ".png"); break;
                case 4: img0 = new Bitmap("pic\\game2\\腦" + score + ".png"); break;
                case 5: img0 = new Bitmap("pic\\game2\\腦" + score + ".png"); break;
                case 6: img0 = new Bitmap("pic\\game2\\腦" + score + ".png"); break;
                case 8: img0 = new Bitmap("pic\\game2\\腦" + score + ".png"); break;
                case 9: img0 = new Bitmap("pic\\game2\\腦" + score + ".png"); break;
                case 10: img0 = new Bitmap("pic\\game2\\腦" + score + ".png"); break;
                case 11: img0 = new Bitmap("pic\\game2\\腦" + score + ".png"); break;
                case 12: img0 = new Bitmap("pic\\game2\\腦" + score + ".png"); break;
                
            }
            imgPainted = img0;
        }

    }
    class Game2_eatball : Drawing_objects, Icollision
    {

        public Game2_eatball()
        {
            img0 = new Bitmap("pic\\game2\\腦12.png");
            x = random.Next(100, 700);
            y = random.Next(150, 550);
            width = 50; height = 40;
            imgPainted = img0;
        }
        public Game2_eatball(int x, int y, int width, int height)
        {
            img0 = new Bitmap("pic\\game2\\skull.png");
            imgPainted = img0;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public int collision(int x, int y, int width, int height)
        {
            if ((Math.Abs(this.x - x) < this.width / 2 + width / 2 && Math.Abs(this.y - y) < this.height / 2 + height / 2))
            {
                return 1;
            }
            return 0;
        }
    }
    class Game2_canteatball : Drawing_objects, Icollision
    {
        public Game2_canteatball()
        {
            img0 = new Bitmap("pic\\game2\\腦0.png");
            x = random.Next(100, 700);
            y = random.Next(150, 550);
            width = 50; height = 40;
            imgPainted = img0;
        }
        public Game2_canteatball(int x, int y, int width, int height)
        {
            img0 = new Bitmap("pic\\game2\\腦0.png");
            imgPainted = img0;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public int collision(int x, int y, int width, int height)
        {
            if ((Math.Abs(this.x - x) < this.width / 2 + width / 2 && Math.Abs(this.y - y) < this.height / 2 + height / 2))
            {
                return 1;
            }
            return 0;
        }
    }
}
