using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csharpwork7
{
    public partial class Form1 : Form
    {

        private Graphics graphics;
        public int Degree1 { get; set; }//分支角度
        public int Degree2 { get; set; }//分支角度
        public double per1 { get; set; }
        public double per2 { get; set; }
        public double Per1 { get; set; }//分支长度比1
        public double Per2 { get; set; }//分支长度比2
        public double Length { get; set; } //主干高度
        public int N { get; set; }//迭代次数
        public Pen DrawPen { get; set; }//画笔
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Degree1 = 30; Degree2 = 40; per1 = 6; per2 = 7; Length = 100; N = 10;
            R = 90;G = 20;B = 20;
            trackBar7.DataBindings.Add("Value", this, "R");
            trackBar8.DataBindings.Add("Value", this, "G");
            trackBar9.DataBindings.Add("Value", this, "B");

            trackBar1.DataBindings.Add("Value", this, "Degree1");
            trackBar2.DataBindings.Add("Value", this, "Degree2");
            trackBar3.DataBindings.Add("Value", this, "per1");

            trackBar4.DataBindings.Add("Value", this, "per2");

            trackBar5.DataBindings.Add("Value", this, "Length");
            trackBar6.DataBindings.Add("Value", this, "N");
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            Color c7 = Color.FromArgb(R, G, B);
            DrawPen = new Pen(c7);
            Per1 = per1 / 10;
            Per2 = per2 / 10;
            graphics = this.panel2.CreateGraphics();
            graphics.Clear(panel2.BackColor);
            DrawCayleyTree(this.N, panel2.Width / 2, panel2.Height - 20, this.Length, -Math.PI / 2);
        }


        void DrawCayleyTree(int n, double x0, double y0, double len, double th)
        {
            if (n == 0) return;
            double x1 = x0 + len * Math.Cos(th);
            double y1 = y0 + len * Math.Sin(th);
            graphics.DrawLine(DrawPen,
               (int)x0, (int)y0, (int)x1, (int)y1);
            DrawCayleyTree(n - 1, x1, y1, this.Per1 * len, th + this.Degree1 * Math.PI / 180);
            DrawCayleyTree(n - 1, x1, y1, this.Per2 * len, th - this.Degree2 * Math.PI / 180);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label10.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label11.Text = trackBar2.Value.ToString();
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            label12.Text = "0."+trackBar3.Value.ToString();
        }

        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            label13.Text = "0." + trackBar4.Value.ToString();
        }

        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            label14.Text =trackBar5.Value.ToString();
        }

        private void trackBar6_ValueChanged(object sender, EventArgs e)
        {
            label15.Text = trackBar6.Value.ToString();
        }

        private void trackBar7_ValueChanged(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(trackBar7.Value, trackBar8.Value, trackBar9.Value);
        }

        private void trackBar8_ValueChanged(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(trackBar7.Value, trackBar8.Value, trackBar9.Value);
        }

        private void trackBar9_ValueChanged(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(trackBar7.Value, trackBar8.Value, trackBar9.Value);
        }
    }
}

