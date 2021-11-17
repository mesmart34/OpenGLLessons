using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;

namespace Task4
{

    class FractalRenderer : GameWindow
    {
        private List<Vector2> fractal;

        public FractalRenderer() : base(800, 600, GraphicsMode.Default, "Fractal")
        {

        }

        private void AddPoint(List<Vector2> points, Vector2 point)
        {
            var temp = points[points.Count - 1];
            points.Add((point + temp));
        }

        private void Hilbert(List<Vector2> points, int depth, float dx, float dy)
        {
            if (depth > 1)
            {
                Hilbert(points, depth - 1, dy, dx);
                AddPoint(points, new Vector2(dx, dy));
            }
            if (depth > 1)
            {
                Hilbert(points, depth - 1, dx, dy);
                AddPoint(points, new Vector2(dy, dx));
            }
            if (depth > 1)
            {
                Hilbert(points, depth - 1, dx, dy);
                AddPoint(points, -new Vector2(dx, dy));
            }
            if (depth > 1)
            {
                Hilbert(points, depth - 1, -dy, -dx);
                //AddPoint(points, -new Vector2(dy, dx));
            }

        }


        protected override void OnLoad(EventArgs e)
        {
            fractal = new List<Vector2>();
            var totalLength = 0.0f;
            if (Height < Width)
                totalLength = (float)(0.9 * Height);
            else
                totalLength = (float)(0.9 * Width);
            var first = new Vector2((Width - totalLength) / 2, (Height - totalLength) / 2);
            fractal.Add(first);
            var depth = 8;
            var startLength = totalLength / (Math.Pow(2, depth) - 1);
            Hilbert(fractal, depth, (float)startLength, 0);
            base.OnLoad(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.LoadIdentity();
            GL.Translate(new Vector3(-1.4f, -0.8f, 0));
            GL.Begin(BeginMode.Lines);
            float scale = 0.005f;
            for(var i = 0; i < fractal.Count - 1; i++)
            {
                GL.Vertex2(fractal[i] * scale);
                GL.Vertex2(fractal[i + 1] * scale);
            }
            GL.End();


            /*GL.Begin(BeginMode.Triangles);
            GL.Vertex2(-1, -1);
            GL.Vertex2(1, -1);
            GL.Vertex2(1, 1);
            //GL.Vertex2(-1, 1);
            GL.End();*/

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
    }
}
