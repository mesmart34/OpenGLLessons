using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    internal class Sphere : IShape
    {
        public Vector3 Position { get; set; }
        public float Radius { get; set; }
        public Vector3 Color { get; set; }

        public bool Intersects(Vector3 start, Vector3 direction, out RayHit hit)
        {
            hit = new RayHit();
            var d = direction;
            var oc = Position - start;
            var b = 2 * Vector3.Dot(oc, d);
            var c = Vector3.Dot(oc, oc) - Radius;
            var disc = b * b - 4 * c;
            if (disc >= 0)
            {
                disc = (float)Math.Sqrt(disc);
                hit.Distance = disc;
                var t0 = -b - disc;
                var t1 = -b + disc;
                //var t = (t0 < t1) ? t0 : t1;
                hit.Color = Color;
                return true;
            }
            return false;
        }
    }
}
