using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public class RayHit
    {
        public Vector3 Position { get; set; }
        public float Distance { get; set; }
        public Vector3 Normal { get; set; }
        public Vector3 Color { get; set; }
        public Vector3 Specular { get; set; }
        public float RefractiveIndex { get; set; }
        public float Transparency { get; set; }

        public RayHit()
        {
            Position = new Vector3(0.0f, 0.0f, 0.0f);
            Distance = float.PositiveInfinity;
            RefractiveIndex = 1.0f;
            Normal = new Vector3(0.0f, 0.0f, 0.0f);
            Color = new Vector3(0.0f, 0.0f, 0.0f);
            Specular = new Vector3(0.0f, 0.0f, 0.0f);
            Transparency = 0.0f;
        }
    }
}
