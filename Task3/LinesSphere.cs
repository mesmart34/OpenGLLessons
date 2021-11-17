using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafica
{
    public class LinesSphere
    {
        public static void Draw(double r, int lats, int longs)
        {
            int i, j;
            for (i = 0; i <= lats; i++)
            {
                var lat0 = Math.PI * (-0.5 + (double) (i - 1) / lats);
                var z0 = Math.Sin(lat0);
                var zr0 = Math.Cos(lat0);

                var lat1 = Math.PI * (-0.5 + (double) i / lats);
                var z1 = Math.Sin(lat1);
                var zr1 = Math.Cos(lat1);

                GL.Begin(BeginMode.LineStrip);
                for (j = 0; j <= longs; j++)
                {
                    var lng = 2 * Math.PI * (j - 1) / longs;
                    var x = Math.Cos(lng);
                    var y = Math.Sin(lng);

                    GL.Normal3(x * zr0, y * zr0, z0);
                    GL.Vertex3(r * x * zr0, r * y * zr0, r * z0);
                    GL.Normal3(x * zr1, y * zr1, z1);
                    GL.Vertex3(r * x * zr1, r * y * zr1, r * z1);
                }

                GL.End();
            }
        }
    }
}