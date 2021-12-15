using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    internal interface IShape
    {
        Vector3 Position { get; set; }
        Vector3 Color { get; set; }
        Material Material { get; set; }
        bool Intersects(Ray ray, ref RayHit bestHit);
    }
}
