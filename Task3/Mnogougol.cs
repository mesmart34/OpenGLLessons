using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafica
{
    class Mnogougol
    {
        private const int Radius = 100;

        public static void Draw(Vector3 center, int n)
        {
            Vector3 clr1 = new Vector3(1.0f, 1.0f, 0.0f);
            Vector3 clr2 = new Vector3(1.0f, 0.0f, 0.0f);
            Vector3 clr3 = new Vector3(0.2f, 0.9f, 1.0f);
            Vector3 clr4 = new Vector3(0.4f, 0.2f, 1.0f);
            Vector3 clr5 = new Vector3(0.7f, 0.4f, 1.0f);
            var drawMode = BeginMode.LineStrip;
            DrawBottom(drawMode,center, clr1, n);
        }
        private static void DrawBottom(BeginMode drawMode, Vector3 center, Vector3 clr3, int N)
        {
            var angle = (float)Math.PI * 2 / N;
            var points = Enumerable.Range(0, N)
                .Select(i => GetVector(center, i, angle))
                .ToArray();
            GL.Begin(drawMode);
            foreach(var vec in points)
            {
                GL.Color3(clr3);
                GL.Vertex3(vec.X, vec.Y, vec.Z);
            }
            var first = points.First();
            GL.Color3(clr3);
            GL.Vertex3(first.X, first.Y, first.Z);
            GL.End();
            for(var i = 0; i < points.Length - 1; i++)
            {
                DrawSide(points[i], points[i + 1], drawMode, clr3, center);
            }
            DrawSide(points.Last(), points.First(), drawMode, clr3, center);
        }
        private static void DrawSide(Vector3 first, Vector3 second, BeginMode drawMode, Vector3 clr3, Vector3 center)
        {
            GL.Begin(drawMode);

            GL.Color3(clr3);
            GL.Vertex3(first);

            GL.Color3(clr3);
            GL.Vertex3(second);

            GL.Color3(clr3);
            GL.Vertex3(center.X, center.Y, 100);

            GL.Color3(clr3);
            GL.Vertex3(first);

            GL.End();

        }
        private static Vector3 GetVector(Vector3 center, int i, float angle)
        {
           
            var vec = new Vector3(center.X, center.Y, center.Z);
            var inc = new Vector3((float)Math.Sin(i * angle) * Radius, (float)Math.Cos(i * angle) * Radius, 0);
            return Vector3.Add(vec, inc);
           
        }
    }
}
