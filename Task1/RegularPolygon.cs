using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WindowsFormsApp1
{
    public class RegularPolygon
    {
        private readonly SolidBrush Brush;
        private readonly Pen Pen;
        private Graphics g;
        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }
        public int N { get; set; }

        public RegularPolygon(Graphics g)
        {
            Brush = new SolidBrush(Color.Blue);
            Pen = new Pen(Color.Brown, 5);
            this.g = g;
        }

        public void Draw()
        {
            var angle = Math.PI * 2 / N;
            var points = Enumerable.Range(0, N)
                .Select(i => PointF.Add(new Point(X, Y),
                    new SizeF((float) Math.Sin(i * angle) * Radius, (float) Math.Cos(i * angle) * Radius)))
                .ToArray();
            g.FillPolygon(Brush, points);
            g.DrawPolygon(Pen, points);
           
        }

        public void Undo(Color backColor)
        {
            var userInput = "";
            var digit = userInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var brush = new SolidBrush(backColor);
            var pen = new Pen(backColor, 5);
            var angle = Math.PI * 2 / N;
            var points = Enumerable.Range(0, N)
                .Select(i => PointF.Add(new Point(X, Y),
                    new SizeF((float) Math.Sin(i * angle) * Radius, (float) Math.Cos(i * angle) * Radius)))
                .ToArray();
            g.FillPolygon(brush, points);
            g.DrawPolygon(pen, points);
        }
    }
}