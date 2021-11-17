using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafica
{
    class Trapezoid
    {
        public static void Draw(Vector3 center)
        {
            Vector3 clr1 = new Vector3(1.0f, 1.0f, 0.0f);
            Vector3 clr2 = new Vector3(1.0f, 0.0f, 0.0f);
            Vector3 clr3 = new Vector3(0.2f, 0.9f, 1.0f);
            Vector3 clr4 = new Vector3(0.4f, 0.2f, 1.0f);
            Vector3 clr5 = new Vector3(0.7f, 0.4f, 1.0f);
            var drawMode = BeginMode.LineStrip;
            DrawBottom(drawMode, clr1, center);
            DrawTop(drawMode, clr1, center);
            DrawFirst(drawMode, clr2, center);
            DrawSecond(drawMode, clr3, center);
            DrawThird(drawMode, clr4, center);
            DrawFourth(drawMode, clr5, center);
        }
        private static void DrawBottom(BeginMode drawMode, Vector3 clr3, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(clr3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }
        private static void DrawTop(BeginMode drawMode, Vector3 clr3, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(clr3);
            GL.Vertex3(-50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(50.0f + point.X, 50.0f + point.Y, 0.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-50.0f + point.X, 50.0f + point.Y, 0.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);
            GL.End();
        }
        private static void DrawFirst(BeginMode drawMode, Vector3 clr3, Vector3 point)
        {
            GL.Begin(drawMode);

            GL.Color3(clr3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);

            GL.End();
        }
        private static void DrawSecond(BeginMode drawMode, Vector3 clr3, Vector3 point)
        {
            GL.Begin(drawMode);

            GL.Color3(clr3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(50.0f + point.X, 50.0f + point.Y, 0.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);

            GL.End();
        }
        private static void DrawThird(BeginMode drawMode, Vector3 clr3, Vector3 point)
        {
            GL.Begin(drawMode);

            GL.Color3(clr3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-50.0f + point.X, 50.0f + point.Y, 0.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(50.0f + point.X, 50.0f + point.Y, 0.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.End();
        }
        private static void DrawFourth(BeginMode drawMode, Vector3 clr3, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(clr3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-50.0f + point.X, 50.0f + point.Y, 0.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.End();
        }
    }
}
