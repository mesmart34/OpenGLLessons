using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    internal class Plane : IShape
    {
        public float Size { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Color { get; set; }

        public bool Intersects(Vector3 start, Vector3 direction, out RayHit hit)
        {
            var denom = Vector3.Dot(start, direction);
            hit = new RayHit(); 
            if (Math.Abs(denom) <= 1e-4f)
                return false;
            var t = -(Vector3.Dot(start, direction) + Size) / Vector3.Dot(start, direction);
            if (t <= 1e-4)
                return false;
            hit.Point = start + t * direction;
            return true;
        }
    }
}
