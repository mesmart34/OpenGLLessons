using System.Drawing;

namespace WindowsFormsApp1
{
    public class Circle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int W { get; set; }
        public string Text { get; set; }
        private Graphics g;

        public Circle(Graphics g)
        {
            this.g = g;
        }

        public void Draw()
        {
            var br = new SolidBrush(Color.Blue);
            var texbr = new SolidBrush(Color.Yellow);
            var p = new Pen(Color.Red, W);
            g.FillEllipse(br, X, Y, Width, Height);
            g.DrawEllipse(p, X, Y, Width, Height);
            g.DrawString(Text, new Font(FontFamily.GenericSerif, 30), texbr, X, (Y + Height)/ 2 );
        }

        public void Undo(Color backColor)
        {
            var br = new SolidBrush(backColor);
            var p = new Pen(backColor, W);
            g.FillEllipse(br, X, Y, Width, Height);
            g.DrawEllipse(p, X, Y, Width, Height);
        }
    }
}