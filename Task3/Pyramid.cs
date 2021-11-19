using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Graphics
{
    public static class Pyramid
    {
        public static void Draw(Vector3 center)
        {
            var color1 = new Vector3(0.0f, 0.0f, 0.0f);
            var color2 = new Vector3(0.0f, 0.0f, 1.0f);
            var color3 = new Vector3(0.0f, 1.0f, 1.0f);
            var color4 = new Vector3(1.0f, 0.0f, 1.0f);
            var color5 = new Vector3(1.0f, 0.0f, 0.0f);
            var drawMode = PrimitiveType.LineStrip;
            DrawFirstPolygon(drawMode, color2, center);
            DrawSecondPolygon(drawMode, color3, center);
            DrawThirdPolygon(drawMode, color4, center);
            DrawFourthPolygon(drawMode, color5, center);
            DrawBottomPolygon(drawMode, color1, center);
        }
        
        private static void DrawBottomPolygon(PrimitiveType drawMode, Vector3 color3, Vector3 point)
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

        private static void DrawFirstPolygon(PrimitiveType drawMode, Vector3 color3, Vector3 point)
        {
            GL.Begin(drawMode);
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

        private static void DrawSecondPolygon(PrimitiveType drawMode, Vector3 color3, Vector3 point)
        {
            GL.Begin(drawMode);
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

        private static void DrawThirdPolygon(PrimitiveType drawMode, Vector3 color3, Vector3 point)
        {
            GL.Begin(drawMode);
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

        private static void DrawFourthPolygon(PrimitiveType drawMode, Vector3 color3, Vector3 point)
        {
            GL.Begin(drawMode);
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
    }
}
