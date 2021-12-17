using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public interface IShape
    {
        Vector3 Position { get; set; }
        Material Material { get; set; }
        void Intersects(Vector3 rayPos, Vector3 rayDir, ref RayHit bestHit);
    }
}
