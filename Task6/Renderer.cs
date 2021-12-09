using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;


namespace Task6
{

    internal class Renderer : GameWindow
    {
        private Random _random;
        private Model _polygon;
        private Model _cube;
        private Model _triangle;
        private Model _torus;
        private Model _octaeder;

        public Renderer(int width, int height)
            : base(width, height, GraphicsMode.Default, "Task 6")
        {
            _random = new Random();
            GL.Enable(EnableCap.DepthTest);
            GL.CullFace(CullFaceMode.FrontAndBack);
        }

        private Model Create2DMultipolygon(int n)
        {
            var vertices = new List<Vector3>();
            var indices = new List<int>();
            var indx = 0;
            for (var angle = 0.0; angle < 360; angle += (360 / n))
            {
                indices.Add(indx++);
                var _angle = angle * Math.PI / 180.0;
                var x = (float)Math.Cos(_angle);
                var y = (float)Math.Sin(_angle);
                vertices.Add(new Vector3(x, y, 0));
            }
            return new Model(vertices, indices);
        }

        private Vector3 GenerateColor()
        {
            return new Vector3(
                (float)_random.NextDouble(),
                (float)_random.NextDouble(),
                (float)_random.NextDouble()
                );
        }

        private Model CreateCube()
        {
            var vertices = new List<Vector3>();
            var colors = new List<Vector3>();
            var indices = new List<int>
            {
                0, 1, 2,
                2, 3, 0,
		        1, 5, 6,
                6, 2, 1,
		        7, 6, 5,
                5, 4, 7,
		        4, 0, 3,
                3, 7, 4,
		        4, 5, 1,
                1, 0, 4,
		        3, 2, 6,
                6, 7, 3
            };
            vertices.Add(new Vector3(-1.0f, -1.0f, 1.0f));
            vertices.Add(new Vector3(1.0f, -1.0f, 1.0f));
            vertices.Add(new Vector3(1.0f, 1.0f, 1.0f));
            vertices.Add(new Vector3(-1.0f, 1.0f, 1.0f));

            vertices.Add(new Vector3(-1.0f, -1.0f, -1.0f));
            vertices.Add(new Vector3(1.0f, -1.0f, -1.0f));
            vertices.Add(new Vector3(1.0f, 1.0f, -1.0f));
            vertices.Add(new Vector3(-1.0f, 1.0f, -1.0f));

            for (var i = 0; i < vertices.Count; i++)
                colors.Add(GenerateColor());
            var model = new Model(vertices, indices);
            model.SetColors(colors);
            return model;
        }

        private Model Create3DTriangle()
        {
            var vertices = new List<Vector3>();
            var colors = new List<Vector3>();
            var indices = new List<int> {
                0, 1, 2,
                0, 2, 3,
                0, 1, 4,
                1, 2, 4,
                2, 3, 4,
                3, 0, 4
            };

            vertices.Add(new Vector3(-1, 1, 0));    //0
            vertices.Add(new Vector3(1, 1, 0));     //1
            vertices.Add(new Vector3(1, -1, 0));    //2
            vertices.Add(new Vector3(-1, -1, 0));   //3
            vertices.Add(new Vector3(0, 0, 3));     //4

            for (var i = 0; i < vertices.Count; i++)
                colors.Add(GenerateColor());
            var model = new Model(vertices, indices);
            model.SetColors(colors);
            return model;
        }

        private Model Create3DTorus()
        {
            var vertices = new List<Vector3>();
            var colors = new List<Vector3>();
            var indices = new List<int> {
                
            };
            var TAU = 2.0 * Math.PI;
            var r = 25.0;
            var c = 50.0;
            var rSeg = 50.0;
            var cSeg = 50.0;
            for (var i = 0; i < rSeg; i++)
            {
                for (var j = 0; j <= cSeg; j++)
                {
                    for (var k = 0; k <= 1; k++)
                    {
                        var s = (i + k) % rSeg + 0.5;
                        var t = j % (cSeg + 1);
                        var x = (c + r * Math.Cos(s * TAU / rSeg)) * Math.Cos(t * TAU / cSeg);
                        var y = (c + r * Math.Cos(s * TAU / rSeg)) * Math.Sin(t * TAU / cSeg);
                        var z = r * Math.Sin(s * TAU / rSeg);
                        vertices.Add(new Vector3((float)(2 * x), (float)(2 * y), (float)(2 * z)));
                    }
                }
            }

            for (var i = 0; i < vertices.Count; i++)
                colors.Add(GenerateColor());
            return new Model(vertices, new List<int>());
        }


        private Model CreateOctaeder()
        {
            var vertices = new List<Vector3>();
            var colors = new List<Vector3>();
            var indices = new List<int> {
                0, 2, 3,
                0, 1, 4,
                1, 2, 4,
                2, 3, 4,
                3, 0, 4,
                0, 1, 5,
                1, 2, 5,
                2, 3, 5,
                3, 0, 5
            };

            vertices.Add(new Vector3(-1, 1, 0));    //0
            vertices.Add(new Vector3(1, 1, 0));     //1
            vertices.Add(new Vector3(1, -1, 0));    //2
            vertices.Add(new Vector3(-1, -1, 0));   //3
            vertices.Add(new Vector3(0, 0, 1.5f));     //4
            vertices.Add(new Vector3(0, 0, -1.5f));     //5

            for (var i = 0; i < vertices.Count; i++)
                colors.Add(GenerateColor());
            var model = new Model(vertices, indices);
            model.SetColors(colors);
            return model;
        }

        protected override void OnResize(EventArgs e)
        {
            Resize(Width, Height);
            base.OnResize(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            //(квадрат, четырехугольник, круг, многоугольник и т.п.)
            _polygon = Create2DMultipolygon(4); //Сменить аргумент, чтобы изменить кол-во углов
            _polygon.SetPosition(new Vector3(0, 0, -5));
            _polygon.SetScale(new Vector3(0.3f, 0.3f, 1.0f));

            _cube = CreateCube();

            _triangle = Create3DTriangle();
            _triangle.SetPosition(new Vector3(0, 0, -5));
            _torus = Create3DTorus();

            _octaeder = CreateOctaeder();
            _octaeder.SetPosition(new Vector3(0, 0, -5));


        }

        private void Resize(int width, int height)
        {
            var aspectRatio = width / (float)height;
            GL.Viewport(0, 0, width, height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            var perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1f, 5000000000);
            GL.MultMatrix(ref perspective);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            _polygon.Rotate(new Vector3(1, 0, 0));
            _cube.SetPosition(new Vector3(0, 0, -5));
            _triangle.Rotate(new Vector3(0.1f, 0.1f, 0.1f));
            _cube.Rotate(new Vector3(0.1f, 0.1f, 0.1f));
            _octaeder.Rotate(new Vector3(0.1f, 0.1f, 0.1f));
            _torus.Rotate(new Vector3(0.1f, 0.1f, 0.1f));
            _torus.SetScale(new Vector3(0.1f, 0.1f, 0.1f));
            _torus.SetPosition(new Vector3(0, 0, -40));

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _polygon.Draw(BeginMode.Polygon);
            _cube.Draw(BeginMode.Triangles);
            _triangle.Draw(BeginMode.TriangleStrip);
            _torus.Draw(BeginMode.LineStrip);
            _octaeder.Draw(BeginMode.Triangles);
            SwapBuffers();
            base.OnRenderFrame(e);
        }
    }
}
