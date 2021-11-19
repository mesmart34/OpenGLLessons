using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Line line;
        private Graphics graphics;
        private Circle circle;
        private Polygon polygon;
        private RegularPolygon regularPolygon;

        public Form1()
        {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();
            line = new Line(graphics, Color.Brown);
            circle = new Circle(graphics);
            polygon = new Polygon(graphics);
            regularPolygon = new RegularPolygon(graphics);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            line.Clear(pictureBox1.BackColor);
        }

        private void drawLineButton(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
                line.X1 = int.Parse(textBox1.Text);
            if (textBox2.Text != "")
                line.Y1 = int.Parse(textBox2.Text);
            if (textBox3.Text != "")
                line.X2 = int.Parse(textBox3.Text);
            if (textBox4.Text != "")
                line.Y2 = int.Parse(textBox4.Text);
            if (textBox5.Text != "")
                line.W = int.Parse(textBox5.Text);
            line.Draw();
        }

        private void CancelLine(object sender, EventArgs e)
        {
            line.Undo(pictureBox1.BackColor);
        }

        private void drawCircleButton(object sender, EventArgs e)
        {
            if (textBox10.Text != "")
                circle.X = int.Parse(textBox10.Text);
            if (textBox9.Text != "")
                circle.Y = int.Parse(textBox9.Text);
            if (textBox8.Text != "")
                circle.Width = int.Parse(textBox8.Text);
            if (textBox7.Text != "")
                circle.Height = int.Parse(textBox7.Text);
            if (textBox6.Text != "")
                circle.W = int.Parse(textBox6.Text);
            if (textBox11.Text != "")
                circle.Text = textBox11.Text;
            circle.Draw();
        }

        private void cancelCircleButton(object sender, EventArgs e)
        {
            circle.Undo(pictureBox1.BackColor);
        }

        private void inputPointButton(object sender, EventArgs e)
        {
            var text = textBox12.Text;
            polygon.AddPoint(text);
            listBox1.Items.Add(text);
        }

        private void drawPolygon(object sender, EventArgs e)
        {
            polygon.Draw();
        }

        private void cancelPolygon(object sender, EventArgs e)
        {
            polygon.Undo(pictureBox1.BackColor);
            listBox1.Items.Clear();
        }

        private void drawRegularPolygonButton(object sender, EventArgs e)
        {
            regularPolygon.X = int.Parse(textBox15.Text);
            regularPolygon.Y = int.Parse(textBox14.Text);
            regularPolygon.N = int.Parse(textBox13.Text);
            regularPolygon.Radius = int.Parse(textBox16.Text);
            regularPolygon.Draw();
        }

        private void cancelRegularPolygon(object sender, EventArgs e)
        {
            regularPolygon.Undo(pictureBox1.BackColor);
        }
    }
}