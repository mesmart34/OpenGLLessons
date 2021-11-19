using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Graphics
{
    public static class Trapezoid
    {
        public static void Draw(Vector3 center)
        {
            var color1 = new Vector3(1.0f, 1.0f, 0.0f);
            var color2 = new Vector3(1.0f, 0.0f, 0.0f);
            var color3 = new Vector3(0.2f, 0.9f, 1.0f);
            var color4 = new Vector3(0.4f, 0.2f, 1.0f);
            var color5 = new Vector3(0.7f, 0.4f, 1.0f);
            var drawMode = PrimitiveType.LineStrip;
            DrawBottom(drawMode, color1, center);
            DrawTop(drawMode, color1, center);
            DrawFirst(drawMode, color2, center);
            DrawSecond(drawMode, color3, center);
            DrawThird(drawMode, color4, center);
            DrawFourth(drawMode, color5, center);
        }

        private static void DrawBottom(PrimitiveType drawMode, Vector3 color3, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }

        private static void DrawTop(PrimitiveType drawMode, Vector3 color3, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(color3);
            GL.Vertex3(-50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(50.0f + point.X, 50.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-50.0f + point.X, 50.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);
            GL.End();
        }

        private static void DrawFirst(PrimitiveType drawMode, Vector3 color3, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }

        private static void DrawSecond(PrimitiveType drawMode, Vector3 color3, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(50.0f + point.X, 50.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }

        private static void DrawThird(PrimitiveType drawMode, Vector3 color3, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-50.0f + point.X, 50.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(50.0f + point.X, 50.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }

        private static void DrawFourth(PrimitiveType drawMode, Vector3 color3, Vector3 point)
        {
            GL.Begin(drawMode);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-50.0f + point.X, -50.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-50.0f + point.X, 50.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }
    }
}
