using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace grafica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            center = new Vector3(0, 0, 0);
        }


        double angle = 0;
        double dangle = 0;
        private Vector3 center;

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {

            if (!loaded)
                return;

            //onload
            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);//цвет фона 
            GL.Enable(EnableCap.DepthTest);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //выбор вида
            Matrix4 modelview = Matrix4.LookAt
              (new Vector3(-300, 300, 200), new Vector3(0, 0, 0), new Vector3(0, 0, 1));

            //Vid(comboBox1.SelectedIndex.ToString());

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            //выбор оси вращения
            switch (comboBox1.Text)
            {
                case "X": GL.Rotate(angle, 1, 0, 0); break;
                case "Y": GL.Rotate(angle, 0, 1, 0); break;
                case "Z": GL.Rotate(angle, 0, 0, 1); break;
            }

            Vector3 clr1 = new Vector3(1.0f, 1.0f, 0.0f);
            Vector3 clr2 = new Vector3(1.0f, 0.0f, 0.0f);
            Vector3 clr3 = new Vector3(0.2f, 0.9f, 1.0f);
            Vector3 clr4 = new Vector3(0.3f, 0.9f, 1.0f);

            //Рисуем оси
            GL.Begin(BeginMode.Lines);
            GL.Color3(clr1); //X-желтая
            GL.Vertex3(-300.0f, 0.0f, 0.0f);
            GL.Vertex3(300.0f, 0.0f, 0.0f);
            GL.End();

            GL.Begin(BeginMode.Lines);
            GL.Color3(clr2); //Y-красная
            GL.Vertex3(0.0f, -300.0f, 0.0f);
            GL.Vertex3(0.0f, 300.0f, 0.0f);
            GL.End();

            GL.Begin(BeginMode.Lines);
            GL.Color3(clr3); //Z-голубая
            GL.Vertex3(0.0f, 0.0f, -300f);
            GL.Vertex3(0.0f, 0.0f, 300.0f);
            GL.End();

            //Rectangle.Draw(center);
            //Pyramid.Draw(center);
            //Trapezoid.Draw(center);
            //Octaeder.Draw(center);
            //Mnogougol.Draw(center, 50);
            //Cylindre.Draw(center, 50);
            //LinesSphere.Draw(100, 30, 30);
            //Torus.Draw();
            Spiral.Draw();
            //TriangleSphere.Draw();

           
            glControl1.SwapBuffers();

        }//paint
       

        //вращать
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
            float aspectRatio = (float)glControl.Width / (float)glControl.Height;
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                                       aspectRatio,
                                                                       1f,
                                                                       5000000000);
            GL.MultMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        bool loaded = false;


        private void Form1_Load(object sender, EventArgs e)
        {
            loaded = true;
            SetupViewport(glControl1);
            comboBox1.SelectedIndex = 0;
            //comboBox2.SelectedIndex = 0;
            //glControl1.Visible = false;
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            if (!loaded) return;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

/* ПРОЧЕЕ
 //примеры прочих видов
 Matrix4 Vid(String pos)
        {
            switch (pos)
            {
                case "1": //X (вид сбоку сверху)": 
                  return Matrix4.LookAt(new Vector3(300, 230, 0), new Vector3(0, 50, 0),new Vector3(0, 1, 0));
                case "2": //Y (вид сверху)": 
                  return Matrix4.LookAt(new Vector3(0, 300, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0));
                case "3": //Z (вид сбоку прямо)": 
                  return Matrix4.LookAt(new Vector3(0, 150, 400), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
                case "4": //-X (вид сбоку прямо)": 
                  return Matrix4.LookAt(new Vector3(-300, 300, 200), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
                case "5": //-Y (вид снизу)": 
                  return Matrix4.LookAt(new Vector3(0, -100, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0));
                case "6": //-Z (вид снизу сбоку)": 
                  return Matrix4.LookAt(new Vector3(0, 100, -400), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            default: 
                return Matrix4.LookAt(new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            }
        }

    //Выбор вида через comboBox
    Matrix4 modelview = Vid(comboBox2.SelectedIndex.ToString());
*/
