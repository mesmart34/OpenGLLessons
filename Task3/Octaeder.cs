using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Graphics
{
    public static class Octaeder
    {
        public static void Draw(Vector3 center)
        {
            var color1 = new Vector3(1.0f, 1.0f, 0.0f);
            var color2 = new Vector3(1.0f, 0.0f, 0.0f);
            var color3 = new Vector3(0.2f, 0.9f, 1.0f);
            var color4 = new Vector3(0.4f, 0.2f, 1.0f);
            var color5 = new Vector3(0.7f, 0.4f, 1.0f);
            var mode = PrimitiveType.LineStrip;
            DrawBottom(mode, color1, center);
            DrawFirst(mode, color2, center);
            DrawSecond(mode, color3, center);
            DrawThird(mode, color4, center);
            DrawFourth(mode, color5, center);
            DrawFirst2(mode, color2, center);
            DrawSecond2(mode, color3, center);
            DrawThird2(mode, color4, center);
            DrawFourth2(mode, color5, center);
        }

        private static void DrawBottom(PrimitiveType mode, Vector3 color3, Vector3 point)
        {
            GL.Begin(mode);
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
        private static void DrawFirst(PrimitiveType mode, Vector3 color3, Vector3 point)
        {
            GL.Begin(mode);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(0.0f + point.X, 0.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }
        private static void DrawSecond(PrimitiveType mode, Vector3 color3, Vector3 point)
        {
            GL.Begin(mode);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(0.0f + point.X, 0.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }

        private static void DrawThird(PrimitiveType mode, Vector3 color3, Vector3 point)
        {
            GL.Begin(mode);

            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.Color3(color3);
            GL.Vertex3(0.0f + point.X, 0.0f + point.Y, 0.0f + point.Z);

            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);

            GL.End();
        }
        private static void DrawFourth(PrimitiveType mode, Vector3 color3, Vector3 point)
        {
            GL.Begin(mode);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(0.0f + point.X, 0.0f + point.Y, 0.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }
        private static void DrawFirst2(PrimitiveType mode, Vector3 color3, Vector3 point)
        {
            GL.Begin(mode);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(0.0f + point.X, 0.0f + point.Y, -150.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }
        private static void DrawSecond2(PrimitiveType mode, Vector3 color3, Vector3 point)
        {
            GL.Begin(mode);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(0.0f + point.X, 0.0f + point.Y, -150.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }
        private static void DrawThird2(PrimitiveType mode, Vector3 color3, Vector3 point)
        {
            GL.Begin(mode);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(0.0f + point.X, 0.0f + point.Y, -150.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }
        private static void DrawFourth2(PrimitiveType mode, Vector3 color3, Vector3 point)
        {
            GL.Begin(mode);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, -100.0f + point.Y, -100.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(0.0f + point.X, 0.0f + point.Y, -150.0f + point.Z);
            GL.Color3(color3);
            GL.Vertex3(-100.0f + point.X, 100.0f + point.Y, -100.0f + point.Z);
            GL.End();
        }
    }
}
