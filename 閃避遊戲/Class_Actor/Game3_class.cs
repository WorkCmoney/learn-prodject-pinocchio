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
    class Game3_player : Drawing_objects
    {
        int hp = 9, now_change = 1, right = 1, temp_hp = 10;
        Image imgleft1; Image imgleft2; Image imgleft3; Image imgleftstand; Image imgleftjump;
        Image imgright1; Image imgright2; Image imgright3; Image imgrightstand; Image imgrightjump;
        public Game3_player()
        {
            imgleft1 = new Bitmap("pic\\player\\left1.png");
            imgleft2 = new Bitmap("pic\\player\\left2.png");
            imgleft3 = new Bitmap("pic\\player\\left3.png");
            imgleftstand = new Bitmap("pic\\player\\leftstand.png");
            imgleftjump = new Bitmap("pic\\player\\leftjump.png");
            imgright1 = new Bitmap("pic\\player\\right1.png");
            imgright2 = new Bitmap("pic\\player\\right2.png");
            imgright3 = new Bitmap("pic\\player\\right3.png");
            imgrightstand = new Bitmap("pic\\player\\rightstand.png");
            imgrightjump = new Bitmap("pic\\player\\rightjump.png");

            imgPainted = imgrightstand;
            x = 350;
            y = 300;
            width = 140;
            height = 140;
        }
        public Game3_player(int x, int y, int width, int height)
        {
            imgrightstand = new Bitmap("pic\\player\\rightstand.png");
            imgPainted = imgrightstand;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public int get_right
        {
            get { return right; }
            set { right = value; }
        }
        public int get_hp
        {
            get { return hp; }
            set { hp = value; }
        }
        public override void setCoord(int x, int y)
        {
            this.x = x; this.y = y;
        }
        public void change_img_left()
        {
           
            switch (now_change)
            {
                case 1: imgPainted = imgleft1; now_change += 1; break;
                case 2: imgPainted = imgleft2; now_change += 1; break;
                case 3: imgPainted = imgleft3; now_change -= 2; break;
            }
            right = 0;
        }
        public void change_img_right()
        {
            switch (now_change)
            {
                case 1: imgPainted = imgright1; now_change += 1; break;
                case 2: imgPainted = imgright2; now_change += 1; break;
                case 3: imgPainted = imgright3; now_change -= 2; break;
            }
            right = 1;
        }
        public void change_img_stand()
        {
            if (right == 1)
            {
                imgPainted = imgrightstand;
            }
            else if (right == 0)
            {
                imgPainted = imgleftstand;
            }
        }
        public void change_img_jump()
        {
            if (right == 1)
            {
                imgPainted = imgrightjump;
            }
            else if (right == 0)
            {
                imgPainted = imgleftjump;
            }
        }
        public int getvoc()
        {
                if (temp_hp > hp) { temp_hp = this.hp; return 1; }
                temp_hp = this.hp;
            return 0;
        }
    }
    class Game3_Spider : Drawing_objects, Icollision
    {
        int move_speed = 7;
        Image img1;
        int chg;
        bool down = true;
        public Game3_Spider()
        {
            img0 = new Bitmap("pic\\game3start\\spider\\0.png");
            img1 = new Bitmap("pic\\game3start\\spider\\1.png");
            imgPainted = img0;
            x = random.Next(175, 500);
            y = -300;
            width = 50;
            height = 389;
        }
        public Game3_Spider(int speed,int x ,int y)
        {
            img0 = new Bitmap("pic\\game3start\\spider\\0.png");
            img1 = new Bitmap("pic\\game3start\\spider\\1.png");
            imgPainted = img0;
            this.x = x;
            this.y = y;

            width = 50;
            height = 389;
            this.move_speed = speed;
        }
        public void go_way()
        {
            if (down)
            {
                y += move_speed;
            }
            if (y >= 10)
            {
                down = false;
            }
            if (!down)
            {
                y -= move_speed;
            }
            if (y <=-500)
            {

                down = true;
                x = random.Next(175, 500);
            }
            chg += 1;
            switch (chg)
            {
                case 0: imgPainted = img0; break;
                case 4: imgPainted = img1; chg = -4; break;
            }

        }
        public int collision(int x, int y, int width, int height)
        {

            if ((Math.Abs(this.x+ this.width / 2 -( x+ width / 2)) < (this.width / 2 + width / 2)-30 && Math.Abs(this.y - y) < this.height / 2 + height / 2)) { 
                this.y = -500;
                return 1;
            }
            return 0;
        }
    }
    class Game3_flowerup : Drawing_objects, Icollision
    {
        int move_speed = 10;
        Image img1;
        int chg;
        bool up = true;
        int n = 0;
        public Game3_flowerup()
        {
            img0 = new Bitmap("pic\\game3start\\f\\0.png");
            img1 = new Bitmap("pic\\game3start\\f\\1.png");
            imgPainted = img0;
            x = random.Next(150, 700);
            y = 370;
            width = 141;
            height = 450;
        }
        public Game3_flowerup(int x, int y, int width, int height)
        {
            img0 = new Bitmap("pic\\game3start\\f\\1.png");
            img1 = new Bitmap("pic\\game3start\\f\\0.png");
            imgPainted = img0;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public void go_way()
        {
            if (up)
            {
                y -= move_speed;
            }
            if (y <= 330)
            {
                up = false;
            }
            if (!up)
            {
                y += move_speed;
            }
            if (y >=750)
            {             
                up = true;
            }
            if (chg <= 0)
            {
                chg += 1;
            }
            if (y <= 450&&n==0&&up)
            {
                chg = 7;
                n = 1;
            }
            
            switch (chg)
            {
                case 0: imgPainted = img0;n = 0; break;
                case 7: imgPainted = img1;chg = -12; break;
            }                
        }
        public int collision(int x, int y, int width, int height)
        {

            if ((Math.Abs(this.x - x) < (this.width / 2 + width / 2)-60 && Math.Abs(this.y - y) < this.height / 2 + height / 2))
            {
                this.y = 750;
                return 1;
            }
            return 0;
        }
    }
    class Game3_Attack_right : Drawing_objects, Icollision
    {
        int move_speed = 10;
        Image img1;
        int chg_img = 0;
        public Game3_Attack_right()
        {
            img0 = new Bitmap("pic\\1.png");
            img1 = new Bitmap("pic\\2.png");
            imgPainted = img0;
            x = -5;
            y = 400;//玩家水平位置
            width = 40;
            height = 40;
        }
        public Game3_Attack_right(int x, int y, int width, int height)
        {
            imgPainted = img0;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public void go_way()
        {
            x += move_speed;
            if (x > 920)
            {
                x = -10;
            }
        }
        public int collision(int x, int y, int width, int height)
        {
            if ((Math.Abs(this.x - x) < this.width / 2 + width / 2 && Math.Abs(this.y - y) < this.height / 2 + height / 2))
            {
                this.x = -10; this.y = 400;
                return 1;
            }
            return 0;
        }
        public override void draw_img(Graphics g)
        {
            if (imgPainted != null)
            {
                switch (chg_img)
                {
                    case 0: imgPainted = img0; g.DrawImage(imgPainted, x, y, width, height);chg_img= 1; break;
                    case 1: imgPainted = img1; g.DrawImage(imgPainted, x, y, width, height); chg_img= 0; break;
                }
            }
        }
    }
   
    class Game3_feather : Drawing_objects, Icollision
    {
        int move_speed = 8;
        Image img1;

        public int left = 0;
        public Game3_feather()
        {
            img1 = new Bitmap("pic\\feather.png");
            img0 = new Bitmap("pic\\feather1.png");
            imgPainted = img0;
            x = random.Next(175, 680);
            y = 0;
            width = 40;
            height = 40;
        }
        public Game3_feather(int x, int y, int width, int height)
        {
            imgPainted = img0;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public void go_way()
        {
            if (left < 20)
            {
                x -= random.Next(2, 7);
                left += 1;

            }

            if (left >= 20 && left < 40)
            {
                x += random.Next(2, 7);
                left += 1;
            }
            if (left >= 40)
            {
                left = 0;
            }
            y += move_speed;
            if (y > 670)
            {
                x = random.Next(175, 680); y = -5;
            }
        }
        public int collision(int x, int y, int width, int height)
        {
            if ((Math.Abs(this.x - x) < this.width / 2 + width / 2 && Math.Abs(this.y - y) < this.height / 2 + height / 2))
            {
                this.x = random.Next(175, 680); this.y = -5;
                return 1;
            }
            return 0;
        }
    }
    class Game3_heart : Drawing_objects
    {
        public Game3_heart()
        {
            img0 = new Bitmap("pic\\heart\\0.png");
            x = 860; y = 70; width = 80; height = 119;
            imgPainted = img0;
        }
        public void change_img(int score)
        {
            if (score < 16)
            {
                imgPainted = new Bitmap("pic\\heart\\" + score + ".png");
            }
        }
    }
    class Game3_road : Drawing_objects
    {
        public Game3_road()
        {
            img0 = new Bitmap("pic\\road.png");
            x = 90; y = 340; width = 820; height = 300;
            imgPainted = img0;
        }
        public void chg_size(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    } 
    class Game3_Hp : Drawing_objects
    {
        int chg; 
        List <Image>img=new List<Image>();

        public Game3_Hp()
        {
            img0 = new Bitmap("pic\\game3start\\f\\0.png");

            x = 10;
            y = 10;
            width = 156;
            height = 40;
            for (int i = 0; i <= 9; i++)
            {
                img0 = new Bitmap("pic\\game3start\\hp\\" + i + ".png");
                img.Add(img0);
            }

            imgPainted = img0;
        }
        public override void draw_img(Graphics g)
        {
            if (imgPainted != null)
            {
                imgPainted = img0;
                g.DrawImage(imgPainted, x, y, width, height);
            }
        }
        public void How_muchHp(int HP)
            {
            if (HP >= 0)
            {
                img0 = img[HP];
                imgPainted = img0;
            }
            }
    }
}
