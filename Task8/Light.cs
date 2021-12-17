using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public class Light
    {
        public Vector3 Position { get; set; }
        public float Intensity { get; set; }

        public Light(Vector3 position, float intensivity)
        {
            Intensity = intensivity;
            Position = position;
        }
    }
}
