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
using System.Runtime.InteropServices;


namespace 閃避遊戲
{
    interface Icollision
    {
        int collision(int x, int y, int width, int height);
    }
    interface Imoving_methods 
    {
        int game1_goway(int x, int y);
        int game2_go_way(int chose, int x, int y, int width, int height);
        int game3_go_way();
    }
    class music
    {      
        [DllImport("winmm.dll")]
        public static extern int mciSendString(string m_strCmd, string m_strReceive, int m_v1, int m_v2);

        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        static extern Int32 GetShortPathName(String path, StringBuilder shortPath, Int32 shortPathLength);

        /// <summary>
        /// 使用mciSendString播放音樂
        /// </summary>
        /// <param name="name">檔案名稱</param>
        /// <param name="command">命令</param>
        public  static void mciMusic(string name, string command)
        {           
            StringBuilder shortpath = new StringBuilder();
            int result = GetShortPathName(name, shortpath, shortpath.Capacity);
            name = shortpath.ToString();
            string buf = string.Empty;
            mciSendString(command + " " + name, buf, buf.Length, 0); //播放 
        }
    }
    class Record
    {
        string savestring;
        int game1_unlock = 0;
        int game2_unlock = 0;
        int game3_unlock = 0;
        string[] items;
        string line;
        System.IO.StreamReader file;
        System.IO.StreamWriter wtr;
        public int game1{ get { return game1_unlock; } }
        public int game2 { get { return game2_unlock; } }
        public int game3 { get { return game3_unlock; } }
        public void demo_writer(int gameone,int gametwo,int gamethree)
        {
            string filename = "gamecord.text";
            wtr = new System.IO.StreamWriter(filename);
            game1_unlock = gameone;
            game2_unlock = gametwo;
            game3_unlock = gamethree;
            savestring = game1_unlock.ToString() + ',' + game2_unlock.ToString() + ',' + game3_unlock.ToString();

            wtr.WriteLine(savestring);
            wtr.Close();
        }
        public void demo_reader()
        {
            string filename = "gamecord.text";
            try
            {
                file = new System.IO.StreamReader(filename);
            }
            catch
            {
                wtr = new System.IO.StreamWriter(filename);
                wtr.Close();
                file = new System.IO.StreamReader(filename);               
            }
            finally
            {               
                if ((line = file.ReadLine()) != null)
                {                    
                    items = line.Split(',');                   
                    game1_unlock = Int32.Parse(items[0]);                  
                    game2_unlock = Int32.Parse(items[1]);
                    game3_unlock = Int32.Parse(items[2]);
                }
            }
            file.Close();
        }
    }
abstract class Drawing_objects
    {
        protected int x, y;
        protected Image imgPainted;
        protected Image img0;
        protected Random random = new Random(Guid.NewGuid().GetHashCode());
        //Pen greenpen = new Pen(Color.FromArgb(255, 0, 255, 0), 2);
        public int width { get; set; }
        public int height { get; set; }
        public Drawing_objects() { }
        public Drawing_objects(int x, int y, int width, int height)
        {
            imgPainted = img0;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public virtual void setCoord(int x, int y) {this.x = x; this.y = y;}
        public virtual int get_x  { get { return x; } set { x = value; } }
        public virtual int get_y  {get { return y; }set { y = value; }}
        public virtual  void draw_img(Graphics g)
        {
            if (imgPainted != null)
            {
                g.DrawImage(imgPainted, x, y, width, height);
                //g.DrawRectangle(greenpen, x, y, width, height);
            }
        }
    }
    class drawline : Drawing_objects
    {
        Pen greenpen = new Pen(Color.FromArgb(255, 255, 255, 255), 2);
        public drawline(int x, int y, int width, int height)
        {
            imgPainted = img0;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public override void draw_img(Graphics g)
        {
            g.DrawRectangle(greenpen, x, y, width, height);
        }
    }
    class Meautext:Drawing_objects
    {
        int timer = 0;int a = 0;
        int slow = 0;
        public int ok { get; set; }
        List<Image> imageitem = new List<Image>();
        List<Image> imageitem1 = new List<Image>();
        List<Image> imageitem2 = new List<Image>();
        public Meautext()
        {
            x = 250;y = 100;width = 444;height =140;
            for (int i = 0; i <=105 ; i++)
            {
                img0 = new Bitmap("pic\\封面\\" + i + ".png");
                imageitem.Add(img0);
            }
            for(int i = 0; i <= 26; i++)
            {
                img0 = new Bitmap("pic\\help\\" + i + ".png");
                imageitem1.Add(img0);
            }
            for (int i = 0; i <= 11; i++)
            {
                img0 = new Bitmap("pic\\howto\\" + i + ".png");
                imageitem2.Add(img0);
            }
            imgPainted = imageitem[0];
        }
        public override void draw_img(Graphics g)
        {            
            if (ok == 0 || ok == 1)   ////皮諾丘
            { 
                if (timer < 103)     
                {                  
                    imgPainted = imageitem[timer];
                    timer += 1;
                }
                else if (timer >= 103 || ok < 2)   
                {
                    ok = 1;
                    a += 1;
                    switch (a)
                    {
                        case 0: imgPainted = imageitem[timer]; timer += 1; break;
                        case 12: imgPainted = imageitem[timer]; timer += 1; break;
                        case 24: imgPainted = imageitem[timer]; timer -= 2; a = -12; break;
                    }                
                }
            }
            else if (timer >0 && ok == 2)
            {
                timer -= 1;
                imgPainted = imageitem[timer];               
                if (timer ==2)
                {
                    ok = 3; timer = 0;
                    imageitem.Clear();
                }
            }
            else if (ok == 3 && timer <= 26)  ///can you help me
            {              
                slow += 1;
                if (slow % 5 == 0&&timer < 26)
                {
                    width = 450; height = 109;
                    timer += 1;
                    imgPainted = imageitem1[timer];
                    if (timer == 26)
                    {
                        slow = 0;
                    }
                }
                if (timer >= 26)
                {                    
                    if (slow >= 17)
                    {
                        ok += 1;
                        timer = 0;
                        imageitem1.Clear();
                        slow = 0;
                    }
                }
            } 
            else if(ok>=4 && timer <= 11)    // how to play
            {              
                slow += 1;
                if (timer < 10&& slow % 5 == 0)
                {
                    x = 180; width = 588; height = 70;
                    timer += 1;
                    imgPainted = imageitem2[timer];
                                    
                    if (timer == 10)
                    {
                        ok = 5; a = 0;
                    }
                }
                else if (ok==5)
                {                 
                    a += 1;
                    switch (a)
                    {
                        case 1: imgPainted = imageitem2[timer];timer += 1;  break;
                        case 17: imgPainted = imageitem2[timer]; timer -= 1;a = 0;  break;                    
                    }                   
                }
            }     
            g.DrawImage(imgPainted, x, y, width, height);
        }
    }
    class starttext : Drawing_objects
    {
        int timer = 1;
        public int ok { get; set; }
        List<Image> imageitem = new List<Image>();
        public starttext()
        {
            x = 415; y = 320; width = 150; height = 68;
            img0 = new Bitmap("pic\\START\\1.png");
            imgPainted = img0;
            for (int i = 0; i <= 37; i++)
            {
                img0 = new Bitmap("pic\\START\\" + i + ".png");
                imageitem.Add(img0);
            }
        }
        public override void draw_img(Graphics g)
        {
            if (ok == 0 || ok == 1)
            {
                if (timer < 37)
                {
                    timer += 1;
                    imgPainted = imageitem[timer];
                }
                else if (timer == 37)
                {
                    ok = 1;
                    imgPainted = imageitem[timer];
                }
            }
            else if (ok == 2 && timer >= 1)
            {
                timer -= 1;
                imgPainted = imageitem[timer];
                g.DrawImage(imgPainted, x, y, width, height);
                if (timer == 0)
                {
                    ok = 3;
                }
            }
            else if (ok == 3)
            {
                imageitem.Clear();
            }
            g.DrawImage(imgPainted, x, y, width, height);
        }
    }
    class Actortext : Drawing_objects
    {
        int timer = 1; int a = 0;
        public int ok { get; set; }
        List<Image> imageitem = new List<Image>();
        public Actortext()
        {
            x = 360; y = 450; width = 250; height = 200;
            for (int i = 0; i <= 37; i++)
            {
                img0 = new Bitmap("pic\\主角臉\\" + i + ".png");
                imageitem.Add(img0);
            }
        }
        public override void draw_img(Graphics g)
        {
            ok = 0;
            if (timer < 35)
            {
                timer += 1;
                imgPainted =imageitem[timer];
                g.DrawImage(imgPainted, x, y, width, height);
            }
            else if (timer >= 35)
            {
                ok = 1;
                a += 1;
                switch (a)
                {
                    case 0: imgPainted = imageitem[timer]; timer += 1;  break;
                    case 12: imgPainted = imageitem[timer]; timer += 1;  break;
                    case 24: imgPainted = imageitem[timer]; timer -= 2; a = -12; break;
                }
                g.DrawImage(imgPainted, x, y, width, height);
            }
        }
    }
    class what_to_do: Drawing_objects
    {
        List<Image> imageitem = new List<Image>();
        int ok = 0;
        int timer = 0;
        int a;
        public what_to_do()
        {
            x = 180; y = 250; width = 616; height = 150;
            
            for (int i = 0; i <= 13; i++)
            {
                img0 = new Bitmap("pic\\howtop\\" + i + ".png");
                imageitem.Add(img0);
            }
            imgPainted = imageitem[0];
        }
        public override void draw_img(Graphics g)
        {
            ok = 0;
            if (timer < 13)
            {
                a += 1;
                if (a % 6 == 0)
                {
                    timer += 1;
                    imgPainted = imageitem[timer];
                    g.DrawImage(imgPainted, x, y, width, height);
                }
            }
            else if (timer == 13)
            {
                ok = 1;
            }
            g.DrawImage(imgPainted, x, y, width, height);
        }
    }
    class draw_click : Drawing_objects
    {
        protected Rectangle rec;
        public override void draw_img(Graphics g)
        {                     
            g.DrawImage(imgPainted, x, y, width, height);
            rec = new Rectangle(x, y, width, height);
        }
        public bool isClick(int x, int y)
        {
            rec.Contains(x, y);
            if (rec.Contains(x, y)) return true;
            else return false;
        }
    }
    class skip : draw_click
    {
        int timer = 0;
        int next = 0;
        List<Image> imageitem = new List<Image>();
        public skip()
        {
            x = 780; y = 600; width = 125; height = 40;
            rec = new Rectangle(x, y, width, height);
            for (int i = 0; i <= 7; i++)
            {
                img0 = new Bitmap("pic\\skip\\" + i + ".png");
                imageitem.Add(img0);
            }
        }
        public skip(int x,int y,int width,int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            rec = new Rectangle(x, y, width, height);
            for (int i = 0; i <= 7; i++)
            {
                img0 = new Bitmap("pic\\skip\\" + i + ".png");
                imageitem.Add(img0);
            }

        }
        public override void draw_img(Graphics g)
        {
            if (timer < 6)
            {
                timer += 1;
                imgPainted = imageitem[timer];
            }
            else if (timer >= 6)
            {
                next += 1;
                switch (next)
                {
                    
                    case 0: imgPainted = imageitem[timer]; timer += 1; break;
                    case 15: imgPainted = imageitem[timer]; timer -= 1; next = -15; break;
                }               
            }

            g.DrawImage(imgPainted, x, y, width, height);
        }
    }
    class levelGAME1 : draw_click
    {
        int timer = 1;
       
        public levelGAME1()
        {
            x = 185; y = 240; width = 120; height = 140;
            img0 = new Bitmap("pic\\level\\A.png");
            rec = new Rectangle(x, y, width, height);
        }
        public override void draw_img(Graphics g)
        {               
                timer += 1;
                imgPainted = img0;
                g.DrawImage(imgPainted, x, y, width, height);         
        }
    }
    class levelGAME2 : draw_click
    {
        int timer = 1; 
        
        public levelGAME2()
        {
            x = 395; y = 140; width = 180; height = 310;
            img0 = new Bitmap("pic\\level\\B.png");
            rec = new Rectangle(x, y, width, height);
        }
        public override void draw_img(Graphics g)
        {
            timer += 1;
            imgPainted = img0;
            g.DrawImage(imgPainted, x, y, width, height);
        }
    }
    class levelGAME3 : draw_click
    {
        int timer = 1;
       
        public levelGAME3()
        {
            x = 650; y = 140; width = 200; height = 280;
            img0 = new Bitmap("pic\\level\\C.png");
            rec = new Rectangle(x, y, width, height);
        }
        public override void draw_img(Graphics g)
        {
            timer += 1;
            imgPainted = img0;
            g.DrawImage(imgPainted, x, y, width, height);

        }
    }


}
