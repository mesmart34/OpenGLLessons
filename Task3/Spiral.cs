using OpenTK.Graphics.OpenGL;
using System;

namespace Graphics
{
    public static class Spiral
    {
        public static void Draw()
        {
            var radius = 50.0f;
            var decrease = 0.0;
            GL.Begin(PrimitiveType.Points);
            for (float angle = 0; angle < 5000; angle += 1)
            {
                var x = Math.Cos(angle * Math.PI / 180) * radius;
                var y = Math.Sin(angle * Math.PI / 180) * radius;
                GL.Vertex3(x, y, decrease);
                decrease -= 0.05f;
            }
            GL.End();
        }
    }
}