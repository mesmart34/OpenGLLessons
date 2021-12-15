using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public struct RayHit
    {
        public Vector3 Position;
        public float Distance;
        public Vector3 Normal;
        public Vector3 Albedo;
        public Vector3 Specular;

        public static RayHit Create()
        {
            RayHit hit;
            hit.Position = new Vector3(0.0f, 0.0f, 0.0f);
            hit.Distance = float.MaxValue;
            hit.Normal = new Vector3(0.0f, 0.0f, 0.0f);
            hit.Albedo = new Vector3(0.0f, 0.0f, 0.0f);
            hit.Specular = new Vector3(0.0f, 0.0f, 0.0f);
            return hit;
        }
    }
}
