using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    internal struct Material
    {
        public Vector3 Albedo;
        public float Transparency;
        public Vector3 Specular;

        public Material(Vector3 albedo, float transparency, Vector3 specular)
        {
            Albedo = albedo;
            Transparency = transparency;
            Specular = specular;    
        }

    }
}
