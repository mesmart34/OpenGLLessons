using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Linq;

namespace Graphics
{
    public static class Cylinder
    {
        private static int Radius = 100;

        public static void Draw(Vector3 center, int n)
        {
            Vector3 clr1 = new Vector3(1.0f, 1.0f, 0.0f);
            Vector3 clr2 = new Vector3(1.0f, 0.0f, 0.0f);
            Vector3 clr3 = new Vector3(0.2f, 0.9f, 1.0f);
            Vector3 clr4 = new Vector3(0.4f, 0.2f, 1.0f);
            Vector3 clr5 = new Vector3(0.7f, 0.4f, 1.0f);
            var angle = (float) Math.PI * 2 / n;
            var points = Enumerable.Range(0, n).Select(i => GetVector(center, i, angle)).ToArray();
            var drawMode = PrimitiveType.LineStrip;
            DrawBottom(drawMode, points, clr1);
            var upPoints = points.Select(x => Vector3.Add(x, new Vector3(0, 0, 100))).ToArray();
            DrawBottom(drawMode, upPoints, clr2);
            DrawSides(points, upPoints, drawMode, clr3);
        }

        private static void DrawBottom(PrimitiveType drawMode, Vector3[] points, Vector3 clr3)
        {
            GL.Begin(drawMode);
            foreach (var vec in points)
            {
                GL.Color3(clr3);
                GL.Vertex3(vec.X, vec.Y, vec.Z);
            }
            var first = points.First();
            GL.Color3(clr3);
            GL.Vertex3(first.X, first.Y, first.Z);
            GL.End();
        }

        private static void DrawSides(Vector3[] points, Vector3[] upPoints, PrimitiveType drawMode, Vector3 clr3)
        {
            for (var i = 0; i < points.Length - 1; i++)
                DrawSide(points[i], points[i + 1], upPoints[i], upPoints[i + 1], drawMode, clr3);
            DrawSide(points.Last(), points.First(), upPoints.Last(), upPoints.First(), drawMode, clr3);
        }

        private static void DrawSide(Vector3 first1, Vector3 second1, Vector3 first2, Vector3 second2, PrimitiveType drawMode, Vector3 clr3)
        {
            GL.Begin(drawMode);
            GL.Color3(clr3);
            GL.Vertex3(first2);
            GL.Color3(clr3);
            GL.Vertex3(first1);
            GL.Color3(clr3);
            GL.Vertex3(second1);
            GL.Color3(clr3);
            GL.Vertex3(second2);
            GL.Color3(clr3);
            GL.Vertex3(first2);
            GL.End();
        }

        private static Vector3 GetVector(Vector3 center, int i, float angle)
        {
            var vec = new Vector3(center.X, center.Y, center.Z);
            var inc = new Vector3((float) Math.Sin(i * angle) * Radius, (float) Math.Cos(i * angle) * Radius, 0);
            return Vector3.Add(vec, inc);
        }
    }
}