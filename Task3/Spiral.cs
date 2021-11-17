using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace grafica
{
    public class Spiral
    {
        public static void Draw()
        {
            var radius = 50.0f;
            var decrease = 0.0;
            GL.Begin(BeginMode.Points);
            for (float angle = 0; angle < 5000; angle += 1)
            {
                var x = Math.Cos(angle * Math.PI / 180) * radius;
                var y = Math.Sin(angle * Math.PI / 180) * radius;
                //radius += 0.01f;
                GL.Vertex3(x, y, decrease);
                decrease -= 0.05f;
            }
            GL.End();
        }
    }
}