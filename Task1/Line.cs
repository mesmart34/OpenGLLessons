using System.Drawing;

namespace WindowsFormsApp1
{
    public class Line
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int W { get; set; }
        private Graphics g;
        private Color color;
        private Pen p;

        public Line(Graphics g, Color color)
        {
            this.g = g;
            this.color = color;
        }

        public void Draw()
        {
            p = new Pen(color, W);
            g.DrawLine(p, X1, Y1, X2, Y2);
        }

        public void Undo(Color backColor)
        {
            p = new Pen(backColor, W);
            g.DrawLine(p, X1, Y1, X2, Y2);
        }

        public void Clear(Color backColor)
        {
            g.Clear(backColor);
        }
    }
}