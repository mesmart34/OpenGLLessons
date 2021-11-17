using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafica
{
    public class Torus
    {
        public static void Draw()
        {
            double r = 25;
            double c = 50;
            var rSeg = 50;
            var cSeg = 50;
            const double TAU = 2 * Math.PI;

            for (var i = 0; i < rSeg; i++)
            {
                GL.Begin(BeginMode.LineStrip);
                for (var j = 0; j <= cSeg; j++)
                {
                    for (var k = 0; k <= 1; k++)
                    {
                        var s = (i + k) % rSeg + 0.5;
                        double t = j % (cSeg + 1);

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