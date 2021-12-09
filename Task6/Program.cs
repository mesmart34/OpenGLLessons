using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var renderer = new Renderer(800, 600);
            renderer.Run(0.0);
        }
    }
}
