using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Line line;
        private Graphics g;
        private Circle circle;
        private Mnogougol Mnogougol;
        private PravMnogougol PravMnogougol;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            line = new Line(g, Color.Brown);
            circle = new Circle(g);
            Mnogougol = new Mnogougol(g);
            PravMnogougol = new PravMnogougol(g);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            line.Undo(pictureBox1.BackColor);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            line.Clear(pictureBox1.BackColor);
        }

        private void button5_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            circle.Undo(pictureBox1.BackColor);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var text = textBox12.Text;
            Mnogougol.AddPoint(text);
            listBox1.Items.Add(text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Mnogougol.Draw();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Mnogougol.Undo(pictureBox1.BackColor);
            listBox1.Items.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            PravMnogougol.X = int.Parse(textBox15.Text);
            PravMnogougol.Y = int.Parse(textBox14.Text);
            PravMnogougol.N = int.Parse(textBox13.Text);
            PravMnogougol.Radius = int.Parse(textBox16.Text);
            PravMnogougol.Draw();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            PravMnogougol.Undo(pictureBox1.BackColor);
        }
    }
}