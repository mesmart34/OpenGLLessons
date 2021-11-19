using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Linq;

namespace Graphics
{
    public static class Polygon
    {
        private const int Radius = 100;

        public static void Draw(Vector3 center, int n)
        {
            var color1 = new Vector3(1.0f, 1.0f, 0.0f);
            var drawMode = PrimitiveType.LineStrip;
            DrawBottom(drawMode,center, color1, n);
        }

        private static void DrawBottom(PrimitiveType drawMode, Vector3 center, Vector3 color3, int N)
        {
            var angle = (float)Math.PI * 2 / N;
            var points = Enumerable.Range(0, N).Select(i => GetVector(center, i, angle)).ToArray();
            GL.Begin(drawMode);
            foreach(var vec in points)
            {
                GL.Color3(color3);
                GL.Vertex3(vec.X, vec.Y, vec.Z);
            }
            var first = points.First();
            GL.Color3(color3);
            GL.Vertex3(first.X, first.Y, first.Z);
            GL.End();
            for(var i = 0; i < points.Length - 1; i++)
            {
                DrawSide(points[i], points[i + 1], drawMode, color3, center);
            }
            DrawSide(points.Last(), points.First(), drawMode, color3, center);
        }

        private static void DrawSide(Vector3 first, Vector3 second, PrimitiveType drawMode, Vector3 color3, Vector3 center)
        {
            GL.Begin(drawMode);
            GL.Color3(color3);
            GL.Vertex3(first);
            GL.Color3(color3);
            GL.Vertex3(second);
            GL.Color3(color3);
            GL.Vertex3(center.X, center.Y, 100);
            GL.Color3(color3);
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
