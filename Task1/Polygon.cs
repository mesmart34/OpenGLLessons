using System.Collections.Generic;
using System.Drawing;

namespace WindowsFormsApp1
{
    public class Polygon
    {
        private List<PointF> Points;
        private readonly SolidBrush Brush;
        private readonly Pen Pen;
        private Graphics g;

        public Polygon(Graphics g)
        {
            this.g = g;
            Points = new List<PointF>();
            Brush = new SolidBrush(Color.Red);
            Pen = new Pen(Color.Blue);
        }

        public void AddPoint(string line)
        {
            var splittedLine = line.Split(';');
            var x = float.Parse(splittedLine[0]);
            var y = float.Parse(splittedLine[1]);
            var point = new PointF(x, y);
            Points.Add(point);
        }

        public void Draw()
        {
            g.FillPolygon(Brush, Points.ToArray());
            g.DrawPolygon(Pen, Points.ToArray());
        }

        public void Undo(Color backColor)
        {
            var brush = new SolidBrush(backColor);
            var pen = new Pen(backColor);
            g.FillPolygon(brush, Points.ToArray());
            g.DrawPolygon(pen, Points.ToArray());
        }
    }
}