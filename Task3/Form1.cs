using System;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Graphics
{
    public partial class Form1 : Form
    {
        private double angle = 0.0;
        private double dangle = 0.0;
        private Vector3 center;
        private bool loaded = false;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            center = new Vector3(0, 0, 0);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded)
                return;
            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            var modelview = Matrix4.LookAt(new Vector3(-300, 300, 200), new Vector3(0, 0, 0), new Vector3(0, 0, 1));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
            switch (comboBox1.Text)
            {
                case "X": GL.Rotate(angle, 1, 0, 0); break;
                case "Y": GL.Rotate(angle, 0, 1, 0); break;
                case "Z": GL.Rotate(angle, 0, 0, 1); break;
            }
            var color1 = new Vector3(1.0f, 1.0f, 0.0f);
            var color2 = new Vector3(1.0f, 0.0f, 0.0f);
            var color3 = new Vector3(0.2f, 0.9f, 1.0f);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(color1); 
            GL.Vertex3(-300.0f, 0.0f, 0.0f);
            GL.Vertex3(300.0f, 0.0f, 0.0f);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(color2); 
            GL.Vertex3(0.0f, -300.0f, 0.0f);
            GL.Vertex3(0.0f, 300.0f, 0.0f);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(color3);
            GL.Vertex3(0.0f, 0.0f, -300f);
            GL.Vertex3(0.0f, 0.0f, 300.0f);
            GL.End();
            
            //Torus.Draw();
            //Pyramid.Draw(center);
            //Octaeder.Draw(center);
            //Trapezoid.Draw(center);
            //Rectangle.Draw(center);
            //Polygon.Draw(center, 50);
            Spiral.Draw();
            //TriangleSphere.Draw();
            //Cylinder.Draw(center, 50);
            //LinesSphere.Draw(100, 30, 30);

            glControl1.SwapBuffers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dangle = (double)numericUpDown1.Value;
            angle += dangle;
            glControl1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            angle = 0;
            glControl1.Invalidate();
        }

        private void SetupViewport(GLControl glControl)
        {
            var aspectRatio = glControl.Width / (float)glControl.Height;
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            var perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1f, 5000000000);
            GL.MultMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loaded = true;
            SetupViewport(glControl1);
            comboBox1.SelectedIndex = 0;
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!loaded)
                return;
            SetupViewport(glControl1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var x = int.Parse(textBox1.Text);
            var y = int.Parse(textBox2.Text);
            var z = int.Parse(textBox3.Text);
            center = new Vector3(x, y, z);
            glControl1.Invalidate();
        }
    }
}