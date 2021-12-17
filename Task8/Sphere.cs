using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public class Sphere : IShape
    {
        public float Radius;
        public Vector3 Position { get; set; }
        public Material Material { get; set; }

        public Sphere(Vector3 position, float radius, Material material)
        {
            Position = position;
            Material = material;
            Radius = radius;    
        }

        public void Intersects(Vector3 rayPos, Vector3 rayDir, ref RayHit bestHit)
        {
            var pos = Position;
            var d = rayPos - pos;
            float p1 = -Vector3.Dot(rayDir, d);
            float p2sqr = p1 * p1 - Vector3.Dot(d, d) + Radius * Radius;
            if (p2sqr < 0)
            {
                return;
            }
            float p2 = (float)Math.Sqrt(p2sqr);
            float t = p1 - p2;
            if (t <= 0)
            {
                t = p1 + p2;
            }
            if (t > 0 && t < bestHit.Distance)
            {
                bestHit.Distance = t;
                bestHit.Position = rayPos + t * rayDir;
                bestHit.Normal = Vector3.Normalize(bestHit.Position - pos);
                bestHit.Color = Material.Albedo;
                bestHit.Specular = Material.Specular;
                bestHit.RefractiveIndex = Material.RefractiveIndex;
            }
        }
    }
}
