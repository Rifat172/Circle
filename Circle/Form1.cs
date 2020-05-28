using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Circle
{
    public partial class Form1 : Form
    {
        //Пикселей в одном делении оси
        const int PIX_IN_ONE = 30;
        //Длина стрелки
        const int ARR_LEN = 10;

        List<CircleClass> Circles = new List<CircleClass>();
        
        int idotX = 0;
        int idotY = 0;

        bool isShown = false;

        Graphics graphics;

        public Form1()
        {
            InitializeComponent();
        }

        private void Paint_Map()
        {
            int w = pictureBox1.ClientSize.Width / 2;
            int h = pictureBox1.ClientSize.Height / 2;
            //Смещение начала координат в центр PictureBox
            graphics.TranslateTransform(w, h);
            DrawXAxis(new Point(-w, 0), new Point(w, 0), graphics);
            DrawYAxis(new Point(0, h), new Point(0, -h), graphics);
            //Центр координат
            graphics.FillEllipse(Brushes.Red, -2, -2, 4, 4);
            pictureBox1.Refresh();
        }
        //Рисование оси X
        private void DrawXAxis(Point start, Point end, Graphics g)
        {
            //Деления в положительном направлении оси
            for (int i = PIX_IN_ONE; i <= end.X - ARR_LEN; i += PIX_IN_ONE)
            {
                g.DrawLine(Pens.Black, i, -5, i, 5);
                DrawText(new Point(i, 5), (i / PIX_IN_ONE).ToString(), g);
            }
            //Деления в отрицательном направлении оси
            for (int i = -PIX_IN_ONE; i > start.X; i -= PIX_IN_ONE)
            {
                g.DrawLine(Pens.Black, i, -5, i, 5);
                DrawText(new Point(i, 5), (i / PIX_IN_ONE).ToString(), g);
            }
            //Ось
            g.DrawLine(Pens.Black, start, end);
            //Стрелка
            g.DrawLines(Pens.Black, GetArrow(start.X, start.Y, end.X, end.Y, ARR_LEN));
        }
        //Рисование оси Y
        private void DrawYAxis(Point start, Point end, Graphics g)
        {
            //Деления в отрицательном направлении оси
            for (int i = PIX_IN_ONE; i < start.Y; i += PIX_IN_ONE)
            {
                g.DrawLine(Pens.Black, -5, i, 5, i);
                DrawText(new Point(5, i), (-i / PIX_IN_ONE).ToString(), g, true);
            }
            //Деления в положительном направлении оси
            for (int i = -PIX_IN_ONE; i > end.Y + ARR_LEN; i -= PIX_IN_ONE)
            {
                g.DrawLine(Pens.Black, -5, i, 5, i);
                DrawText(new Point(5, i), (-i / PIX_IN_ONE).ToString(), g, true);
            }
            //Ось
            g.DrawLine(Pens.Black, start, end);
            //Стрелка
            g.DrawLines(Pens.Black, GetArrow(start.X, start.Y, end.X, end.Y, ARR_LEN));
        }
        //Рисование текста
        private void DrawText(Point point, string text, Graphics g, bool isYAxis = false)
        {
            var f = new Font(Font.FontFamily, 14);
            var size = g.MeasureString(text, f);
            var pt = isYAxis
                ? new PointF(point.X + 1, point.Y - size.Height / 2)
                : new PointF(point.X - size.Width / 2, point.Y + 1);
            var rect = new RectangleF(pt, size);
            g.DrawString(text, f, Brushes.Black, rect);
        }
        //Вычисление стрелки оси
        private static PointF[] GetArrow(float x1, float y1, float x2, float y2, float len = 10, float width = 4)
        {
            PointF[] result = new PointF[3];
            //направляющий вектор отрезка
            var n = new PointF(x2 - x1, y2 - y1);
            //Длина отрезка
            var l = (float)Math.Sqrt(n.X * n.X + n.Y * n.Y);
            //Единичный вектор
            var v1 = new PointF(n.X / l, n.Y / l);
            //Длина стрелки
            n.X = x2 - v1.X * len;
            n.Y = y2 - v1.Y * len;
            result[0] = new PointF(n.X + v1.Y * width, n.Y - v1.X * width);
            result[1] = new PointF(x2, y2);
            result[2] = new PointF(n.X - v1.Y * width, n.Y + v1.X * width);
            return result;
        }

        private void Paint_Circle()
        {
            foreach(var C in Circles)
            {
                graphics.DrawEllipse(new Pen(Brushes.Black, 2), C.X, C.Y, C.diameter, C.diameter);
            }
            pictureBox1.Refresh();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (isShown == true)
            {
                LoadMap();
            }
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            isShown = true;
            LoadMap();
        }
        private void LoadMap()
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            Paint_Map();
            if (Circles.Count != 0)
                Paint_Circle();
        }

        private void AddB_Click(object sender, EventArgs e)
        {
            int radius_ = (int)radius.Value * PIX_IN_ONE;

            int diameter = radius_ * 2;

            int tempX = (int)centerX.Value;
            int tempY = -(int)centerY.Value;

            int X = tempX != 0 ? radius_ * tempX - 30 : tempX - radius_;
            int Y = tempY != 0 ? radius_ * tempY - 30 : tempY - radius_;

            Circles.Add(new CircleClass(X, Y, radius_, diameter));
            Paint_Circle();
        }

        private void CheckB_Click(object sender, EventArgs e)
        {

        }
    }
}
