using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public struct Material
    { 
        public Vector3 Albedo { get; set; }
        public Vector3 Specular { get; set; }
        public float RefractiveIndex { get; set; }
        public float Transparency { get; set; }

        public Material(Vector3 albedo, Vector3 specular, float refractiveIndex = 1)
        {
            Albedo = albedo;
            Specular = specular;
            RefractiveIndex = refractiveIndex;
            Transparency = 0.5f;
        }
    }
}
