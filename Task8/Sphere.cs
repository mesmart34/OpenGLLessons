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
        public Material Material { get; set; }

        public bool Intersects(Ray ray, ref RayHit bestHit)
        {
            var d = ray.Origin - Position;
            d = ray.Origin - Position;
            float p1 = -Vector3.Dot(ray.Direction, d);
            float p2sqr = p1 * p1 - Vector3.Dot(d, d) + Radius * Radius;
            if (p2sqr < 0)
                return false;
            var p2 = (float)Math.Sqrt(p2sqr);
            var t = p1 - p2 > 0 ? p1 - p2 : p1 + p2;
            if (t > 0 && t < bestHit.Distance)
            {
                bestHit.Distance = t;
                bestHit.Position = ray.Origin + ray.Direction * t;
                bestHit.Normal = (bestHit.Position - Position).Normalized();
                bestHit.Albedo = Material.Albedo;
                bestHit.Specular = Material.Specular;
                return true;
            }
            return false;
        }
    }
}
