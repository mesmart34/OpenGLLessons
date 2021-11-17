using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fractal = new FractalRenderer();
            fractal.Run(60.0);
        }
    }
}
