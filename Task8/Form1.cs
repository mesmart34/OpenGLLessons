using OpenTK;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Task8
{
    public partial class Form1 : Form
    {
        private Raytracing _raytracing;

        public Form1()
        {
            InitializeComponent();
            var shapes = new List<IShape>();
            var sphere = new Sphere();
            sphere.Position = new Vector3(-1, 0, -9);
            sphere.Radius = 1.0f;
            sphere.Material = new Material(new Vector3(245 / 250.0f, 144 / 250.0f, 66 / 250.0f), 1.0f, Vector3.One);
            shapes.Add(sphere);

            var sphere1 = new Sphere();
            sphere1.Position = new Vector3(1.5f, 1, -9);
            sphere1.Radius = 1.0f;
            sphere1.Material = new Material(new Vector3(0.4f, 0.7f, 0.7f), 1.0f, Vector3.One);
            shapes.Add(sphere1);
            var plane = new Plane();
            plane.Size = 10;
            //shapes.Add(plane);
            _raytracing = new Raytracing(canvas.Width, canvas.Height, shapes);
            var data = _raytracing.RenderToPlane();
            canvas.Image = GetImage(data);


        }

        private Image GetImage(uint[,] data)
        {
            var bitmap = new Bitmap(Width, Height);
            for (var x = 0; x < data.GetLength(0); x++)
            {
                for (var y = 0; y < data.GetLength(1); y++)
                {
                    var r = (data[x, y] & 0xFF000000) >> 24;
                    var g = (data[x, y] & 0x00FF0000) >> 16;
                    var b = (data[x, y] & 0x0000FF00) >> 8;
                    var color = Color.FromArgb((int)(r), (int)(g), (int)(b));
                    bitmap.SetPixel(x, y, color);
                }
            }
            return bitmap;
        }
    }
}
