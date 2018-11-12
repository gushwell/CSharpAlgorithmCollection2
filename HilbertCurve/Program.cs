using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HilbertCurve
{
    class Program
    {
        static void Main(string[] args)
        {
            int generation = 6;
            int w = 500;
            var hilbert = new HilbertCurve();
            var drawer = new Drawer(w, w, $"HilbertCurve{generation}.png");
            hilbert.Subscribe(drawer);
            hilbert.Start(w, generation);
        }
    }
}
