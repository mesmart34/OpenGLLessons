using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    internal struct Ray
    {
        public Vector3 Origin;
        public Vector3 Direction;
        public Vector3 Energy;

        public static Ray Create(Vector3 origin, Vector3 direction)
        {
            Ray ray;
            ray.Origin = origin;
            ray.Energy = Vector3.One;
            ray.Direction = direction;
            return ray;
        }
    }
}
