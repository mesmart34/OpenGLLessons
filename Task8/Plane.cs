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
        public Material Material { get; set; }

        public bool Intersects(Ray ray, ref RayHit bestHit)
        {
            var groundAlbedo = Material.Albedo;
            var groundSpecular = Material.Specular;

            float t = -ray.Origin.Y / ray.Direction.Y;
            if (t > 0 && t < bestHit.Distance)
            {
                bestHit.Distance = t;
                bestHit.Position = ray.Origin + t * ray.Direction;
                bestHit.Normal = new Vector3(0.0f, 1.0f, 0.0f);
                bestHit.Albedo = groundAlbedo;
                bestHit.Specular = groundSpecular;
                return true;
            }
            return false;
        }
    }
}
