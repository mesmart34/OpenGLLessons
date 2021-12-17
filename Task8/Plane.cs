using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public class Plane : IShape
    {
        public Vector3 Position { get; set; }
        public Material Material { get; set; }

        public Plane(Vector3 position, Material material)
        {
            Position = position;
            Material = material;
        }

        public void Intersects(Vector3 rayPos, Vector3 rayDir, ref RayHit bestHit)
        {
            float t = -rayPos.Y / rayDir.Y;
            if (t > 0 && t < bestHit.Distance)
            {
                bestHit.Distance = t;
                bestHit.Position = rayPos + t * rayDir;
                bestHit.Normal = new Vector3(0.0f, 1.0f, 0.0f);
                bestHit.Color = Material.Albedo;
                bestHit.Specular = Material.Specular;
                bestHit.RefractiveIndex = Material.RefractiveIndex;
            }
        }
    }
}
