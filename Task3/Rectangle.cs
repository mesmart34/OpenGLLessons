using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    class Rectangle
    {
        public static void Draw(Vector3 center)
        {
            var drawMode = BeginMode.Polygon;
            Vector3 clr1 = new Vector3(1.0f, 1.0f, 0.0f);
            Vector3 clr2 = new Vector3(1.0f, 0.0f, 0.0f);
            Vector3 clr3 = new Vector3(0.2f, 0.9f, 1.0f);

            DrawBottom(drawMode, clr1, center);
            DrawTop(drawMode, clr1, center);
            DrawLeft(drawMode, clr2, center);
            DrawRight(drawMode, clr2, center);
            DrawBack(drawMode, clr3, center);
            DrawFront(drawMode, clr3, center);
        }
        private static void DrawFront(BeginMode drawMode, Vector3 clr3, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(clr3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, 100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, 100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, 100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, 100.0f + point.Z);

            GL.Color3(clr3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, 100.0f + point.Z);
            GL.End();
        }
        private static void DrawBack(BeginMode drawMode, Vector3 clr3, Vector3 point)
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

        private static void DrawLeft(BeginMode drawMode, Vector3 clr2, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(clr2);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, 100.0f + point.Z);

            GL.Color3(clr2);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, 100.0f + point.Z);

            GL.Color3(clr2);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr2);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr2);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, 100.0f + point.Z);
            GL.End();

        }
        private static void DrawRight(BeginMode drawMode, Vector3 clr2, Vector3 point)
        {

            GL.Begin(drawMode);
            GL.Color3(clr2);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, 100.0f + point.Z);

            GL.Color3(clr2);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, 100.0f + point.Z);

            GL.Color3(clr2);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr2);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr2);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, 100.0f + point.Z);
            GL.End();

        }

        private static void DrawBottom(BeginMode drawMode, Vector3 clr1, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(clr1);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, 100.0f + point.Z);

            GL.Color3(clr1);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, 100.0f + point.Z);

            GL.Color3(clr1);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr1);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(clr1);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, 100.0f + point.Z);
            GL.End();
        }
        private static void DrawTop(BeginMode drawMode, Vector3 clr1, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(clr1);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, 100.0f + point.Z);

            GL.Color3(clr1);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, 100.0f + point.Z);

            GL.Color3(clr1);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(clr1);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(clr1);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, 100.0f + point.Z);
            GL.End();
        }
    }
}
