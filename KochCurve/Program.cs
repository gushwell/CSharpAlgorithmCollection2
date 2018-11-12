using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KochCurve
{
    class Program
    {
        static void Main(string[] args)
        {
            int generation = 6;
            int width = 800;
            int height = 400;
            var hilbert = new KochCurve();
            var drawer = new Drawer(width, height, $"KochCurve{generation}.png");
            hilbert.Subscribe(drawer);
            hilbert.Start(width, height, generation);
        }
    }
}
