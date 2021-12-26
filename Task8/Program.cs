using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Task8
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ray = new Raytracing();
            ray.Run(60.0);
        }
    }
}
