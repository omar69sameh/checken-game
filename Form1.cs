using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Problem_19
{
    public partial class Form1 : Form
    {
        Bitmap off;
        Timer tt = new Timer();

        public class cIamgeActor
        {
            public int X, Y;
            public Bitmap im;
        }

        public class cLand
        {
            public int X, Y;
            public Bitmap im;
            public int dirX;
        }

        public class tweet
        {
            public int X, Y;
            public List<Bitmap> imgs;
            public int iFrame;
            public int dirX;
            public int pos;

        }

        List<cIamgeActor> hel = new List<cIamgeActor>();
        List<tweet> tweety2 = new List<tweet>();
        List<tweet> tweety = new List<tweet>();
        List<cLand> land2 = new List<cLand>();
        List<cLand> land = new List<cLand>();

        int near = -1;
        bool upKey = false;
        bool leftKey = false;
        bool rightKey = false;
        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

            tt.Interval = 100;
            tt.Start();
            tt.Tick += Tt_Tick;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Up)
            {
                upKey = true;
            }
            else if(e.KeyCode==Keys.Left)
            {
                leftKey = true;
            }
            else if(e.KeyCode==Keys.Right)
            {
                rightKey = true;
            }
            else if(e.KeyCode==Keys.Space)
            {
                if (near != -1 && near < tweety.Count)
                {
                    tweet pnn = new tweet();
                    pnn.X = tweety[near].X;
                    pnn.Y = hel[0].Y + 80;
                    pnn.imgs = new List<Bitmap>();
                    for (int i = 0; i < 2; i++)
                    {
                        Bitmap im = new Bitmap("4.bmp");
                        im.MakeTransparent(im.GetPixel(0, 0));
                        pnn.imgs.Add(im);
                    }
                    pnn.iFrame = 1;
                    tweety2.Add(pnn);
                    tweety.RemoveAt(near);
                }
            }
            
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    upKey = false;
                    break;
                case Keys.Left:
                    leftKey = false;
                    break;
                case Keys.Right:
                    rightKey = false;
                    break;
            }
        }
        private void Tt_Tick(object sender, EventArgs e)
        {
            MoveLandsandBirds();

            if (upKey == false)
            {
                MoveHelicopter();
            }
            else
            {
                hel[0].Y -= 8;
                if (tweety2.Count > 0)
                {
                    for (int i = 0; i < tweety2.Count; i++)
                    {
                        tweety2[i].Y -= 8;
                    }
                }
            }
            if (leftKey)
            {
                hel[0].X -= 3;
                if (tweety2.Count > 0)
                {
                    for (int i = 0; i < tweety2.Count; i++)
                    {
                        tweety2[i].X -= 3;
                    }
                }
            }

            if (rightKey)
            {
                hel[0].X += 3;
                if (tweety2.Count > 0)
                {
                    for (int i = 0; i < tweety2.Count; i++)
                    {
                        tweety2[i].X += 3;
                    }
                }
            }

            drawTweet();

            DrawDubb(this.CreateGraphics());
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            CreateHelicopter();
            CreateLandsMoving();
            CreateLandsStable();
            CreateBirdsLand();
        }

        void CreateHelicopter()
        {
            cIamgeActor pnn = new cIamgeActor();
            pnn.im = new Bitmap("1.bmp");
            pnn.im.MakeTransparent(pnn.im.GetPixel(0, 0));
            pnn.X = this.ClientSize.Width - this.ClientSize.Width / 2;
            pnn.Y = 200;
            hel.Add(pnn);

        }

        void MoveHelicopter()
        {
            hel[0].Y += 3;
            if (tweety2.Count > 0)
            {
                for (int i = 0; i < tweety2.Count; i++)
                {
                    tweety2[i].Y += 3;
                }
            }
        }
        void CreateLandsMoving()
        {
            Random rr = new Random();
            int xLands = 400;
            for (int i = 0; i < 4; i++)
            {
                cLand pnn = new cLand();
                pnn.im = new Bitmap("2.bmp");
                pnn.im.MakeTransparent(pnn.im.GetPixel(0, 0));
                pnn.X = xLands;
                pnn.Y = this.ClientSize.Height - 200;
                if (i == 0 || i == 1)
                {
                    pnn.dirX = -1;
                }
                else
                {
                    pnn.dirX = 1;
                }

                xLands += 200;

                land2.Add(pnn);

            }

        }
        void CreateLandsStable()
        {
            cLand pnn = new cLand();
            pnn.im = new Bitmap("2.bmp");
            pnn.im.MakeTransparent(pnn.im.GetPixel(0, 0));
            pnn.X = 0;
            pnn.Y = this.ClientSize.Height - 200;
            land.Add(pnn);

            pnn = new cLand();
            pnn.im = new Bitmap("2.bmp");
            pnn.im.MakeTransparent(pnn.im.GetPixel(0, 0));
            pnn.X = this.ClientSize.Width - 200; ;
            pnn.Y = this.ClientSize.Height - 200;
            land.Add(pnn);

        }

        void CreateBirdsLand()
        {
            int xBirds = 410;
            for (int j = 0; j < 4; j++)
            {
                tweet pnn = new tweet();
                pnn.imgs = new List<Bitmap>();

                for (int i = 0; i < 2; i++)
                {
                    Bitmap im = new Bitmap( (i + 3).ToString() + ".bmp");
                    im.MakeTransparent(im.GetPixel(0,0));
                    pnn.imgs.Add(im);

                }

                pnn.X = xBirds;
                pnn.Y = this.ClientSize.Height - 250;
                if (j == 0 || j == 1)
                {
                    pnn.dirX = -1;
                }
                else
                {
                    pnn.dirX = 1;
                }
                pnn.pos = j;
                xBirds += 200;
                pnn.iFrame = 0;

                tweety.Add(pnn);
            }
        }

        void MoveLandsandBirds()
        {
            for (int i = 0; i < land2.Count; i++)
            {
                if (land2[i].dirX == 1)
                {
                    land2[i].X += 5;
                }
                else
                {
                    land2[i].X -= 5;
                }
            }

            for (int i = 0; i < tweety.Count; i++)
            {
                if (tweety[i].dirX == 1)
                {
                    tweety[i].X += 5;
                }
                else
                {
                    tweety[i].X -= 5;
                }
            }

            CollideLandsandBirds();
        }

        void CollideLandsandBirds()
        {
            for (int i = 0; i < land2.Count; i++)
            {
                if (i == 0)
                {
                    if (land2[i].X == 70)
                    {
                        land2[i].dirX = 1;
                        for (int k = 0; k < tweety.Count; k++)
                        {
                            if (tweety[k].pos == i)
                            {
                                tweety[k].dirX = land2[tweety[k].pos].dirX;

                            }
                        }
                    }
                    else if (land2[i].X + 70 == land2[i + 1].X)
                    {

                        land2[i].dirX = -1;
                        land2[i + 1].dirX = 1;

                        for (int k = 0; k < tweety.Count; k++)
                        {
                            if (tweety[k].pos == i)
                            {
                                tweety[k].dirX = land2[tweety[k].pos].dirX;

                            }

                        }

                    }
                }
                else if (i == land2.Count - 1)
                {
                    if (land2[i].X + 70 >= this.ClientSize.Width - 200)
                    {
                        land2[i].dirX = -1;
                        for (int k = 0; k < tweety.Count; k++)
                        {
                            if (tweety[k].pos == i)
                            {
                                tweety[k].dirX = land2[tweety[k].pos].dirX;
                            }
                        }
                    }
                    else if (land2[i].X == land2[i - 1].X + 70)
                    {
                        land2[i].dirX = 1;
                        land2[i - 1].dirX = -1;

                        for (int k = 0; k < tweety.Count; k++)
                        {
                            if (tweety[k].pos == i)
                            {
                                tweety[k].dirX = land2[tweety[k].pos].dirX;
                            }
                        }

                    }
                }
                else
                {
                    if (land2[i].X + 70 == land2[i + 1].X)
                    {
                        land2[i].dirX = -1;
                        land2[i + 1].dirX = 1;
                        for (int k = 0; k < tweety.Count; k++)
                        {
                            if (tweety[k].pos == i)
                            {
                                tweety[k].dirX = land2[tweety[k].pos].dirX;

                            }
                        }
                    }
                    else if (land2[i].X == land2[i - 1].X + 70)
                    {
                        land2[i].dirX = 1;
                        land2[i - 1].dirX = -1;
                        for (int k = 0; k < tweety.Count; k++)
                        {
                            if (tweety[k].pos == i)
                            {
                                tweety[k].dirX = land2[tweety[k].pos].dirX;

                            }
                        }
                    }

                }
            }

        }

        void drawTweet()
        {
            for (int i = 0; i < tweety.Count; i++)
            {
                int birdX = tweety[i].X + 25;

                if (birdX >= hel[0].X && birdX <= hel[0].X + 80 &&
                    tweety[i].Y >= hel[0].Y - 100 && tweety[i].Y <= hel[0].Y + 100)
                {
                    tweety[i].iFrame = 1;
                    near = i;
                }
                else
                {
                    tweety[i].iFrame = 0;
                }
            }
        }

        void drawScene(Graphics g)
        {
            g.Clear(Color.GreenYellow);

            for (int i = 0; i < hel.Count; i++)
            {
                g.DrawImage(hel[i].im, hel[i].X, hel[i].Y, 80, 80);
            }

            for (int i = 0; i < tweety.Count; i++)
            {
                int index = tweety[i].iFrame % tweety[i].imgs.Count;
                if (index == 0)
                    g.DrawImage(tweety[i].imgs[index], tweety[i].X, tweety[i].Y, 50, 50);
                else
                    g.DrawImage(tweety[i].imgs[index], tweety[i].X, tweety[i].Y, 40, 40);
            }

            for (int i = 0; i < tweety2.Count; i++)
            {
                int index = tweety2[i].iFrame % tweety2.Count;
                g.DrawImage(tweety2[i].imgs[index], tweety2[i].X, tweety2[i].Y, 40, 40);
            }

            for (int i = 0; i < land2.Count; i++)
            {
                g.DrawImage(land2[i].im, land2[i].X, land2[i].Y, 70, 50);
            }

            for (int i = 0; i < land.Count; i++)
            {
                g.DrawImage(land[i].im, land[i].X, land[i].Y, 70, 50);
            }

        }

        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            drawScene(g2);
            g.DrawImage(off, 0, 0);

        }

    }
}