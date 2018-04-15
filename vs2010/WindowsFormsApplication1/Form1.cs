using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        public int score = 0;

        public Form1()
        {
            InitializeComponent();

            //连接服务器
            Control.CheckForIllegalCrossThreadCalls = false;

            Thread t1 = new Thread(new ThreadStart(tf));
            t1.IsBackground = true;
            t1.Start();

            Thread t2 = new Thread(new ThreadStart(tf2));
            t2.IsBackground = true;
            t2.Start();

          

        }


        private void tf2()
        {
          

            while (true)
            {
                Thread.Sleep(800); 
                
              
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new MethodInvoker(delegate { initone(); }));
                        
                        }
             

            }
        }



        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        } 

        private void tf()
        {  
         

            while (true)
            {
                Thread.Sleep(100);

                foreach (Control pic in this.panel1.Controls)
                {


                    if (pic.Name.Contains("mypic"))
                    {
                        MyPic c = (MyPic)pic;

                        if ((this.InvokeRequired) && (c.mylife > 160))
                        {
                            this.panel1.Controls.Remove(pic);
                        }
                    }
                }

                //控制大小
                foreach (Control pic in this.panel1.Controls)
                {


                    if (pic.Name.Contains("mypic"))
                    {
                        MyPic c = (MyPic)pic;
                        c.mylife++;
                      
                        if (c.Width > 80) { c.tobig = false; }
                        if (c.Width < 10) { c.tobig = true; }

                        if (c.tobig)
                        {
                            c.Size = new Size(c.Width + 1, c.Height + 1);
                   
                        }
                        else
                        {

                            c.Size = new Size(c.Width - 1, c.Height - 1);
                        }  
                      
                    }
                
                
                }


            
            
            }
        
        
        }



        private void initone()
        {    

                MyPic pic = new MyPic();
                pic.Name = "mypic";
                pic.Image =  Properties.Resources.yyyy;
                pic.BackColor = Color.Transparent;

                Random ran = new Random(GetRandomSeed());     
                int Randx = ran.Next(10, 900);
                int y = ran.Next(10, 500);
                pic.Top = y;
                pic.Left = Randx;

                pic.Width = 50;
                pic.Height = 50;

                pic.SizeMode = PictureBoxSizeMode.Zoom; 
            
                pic.Click += delegate(Object o, EventArgs e1) { cl(pic, e1); };                

                this.panel1.Controls.Add(pic);
         
        }

        private void cl(MyPic pic,EventArgs e)
        {


            this.panel1.Controls.Remove(pic);
            score = score + 100;

            toolStripLabel1.Text = "score: " + score.ToString(); 
        
        
        }




        public class MyPic : PictureBox
        {

           public  GraphicsPath gp = new GraphicsPath();

           public bool tobig = false;

           public int mylife = 0;


           protected override void OnPaint(PaintEventArgs e)
           {
               //// 定义颜色的透明度
               Color drawColor = Color.FromArgb(this._alpha, this.BackColor);
               //// 定义画笔
               Pen labelBorderPen = new Pen(drawColor, 0);
               SolidBrush labelBackColorBrush = new SolidBrush(drawColor);
               //// 绘制背景色
               e.Graphics.DrawRectangle(labelBorderPen, 0, 0, Size.Width, Size.Height);
               e.Graphics.FillRectangle(labelBackColorBrush, 0, 0, Size.Width, Size.Height);

               base.OnPaint(e);

           }
            
            /*
            protected override void OnCreateControl()
            {
                       
               // gp.AddRectangle(this.ClientRectangle);//追加一矩形

                gp.AddEllipse(this.ClientRectangle);
                Region region = new Region(gp);
                this.Region = region;
                gp.Dispose();
                region.Dispose();
                base.OnCreateControl();
            }

            public GraphicsPath getgp()
            {
                return gp;
            }
            */



           public int _alpha { get; set; }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
           // toolStripStatusLabel1.Text = MousePosition.X + " " + MousePosition.Y;
        }



    }
}
