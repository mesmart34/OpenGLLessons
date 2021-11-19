using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace Graphics
{
    public static class Torus
    {
        private readonly static  double TAU = 2.0 * Math.PI;

        public static void Draw()
        {
            var r = 50.0;
            var c = 50.0;
            var rSeg = 50.0;
            var cSeg = 50.0;
            for (var i = 0; i < rSeg; i++)
            {
                GL.Begin(PrimitiveType.LineStrip);
                for (var j = 0; j <= cSeg; j++)
                {
                    for (var k = 0; k <= 1; k++)
                    {
                        var s = (i + k) % rSeg + 0.5;
                        var t = j % (cSeg + 1);
                        var x = (c + r * Math.Cos(s * TAU / rSeg)) * Math.Cos(t * TAU / cSeg);
                        var y = (c + r * Math.Cos(s * TAU / rSeg)) * Math.Sin(t * TAU / cSeg);
                        var z = r * Math.Sin(s * TAU / rSeg);
                        GL.Normal3(2 * x, 2 * y, 2 * z);
                        GL.Vertex3(2 * x, 2 * y, 2 * z);
                    }
                }
                GL.End();
            }
        }
    }
}