using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LissajousCurve
{
    class Program
    {
        static void Main(string[] args)
        {
            var width = 500;
            var height = 500;
            int a = 4;
            int b = 5;
            var lc = new LissajousCurve(a, b, width, height);
            var drawer = new Drawer(width, height, $"LissajousCurve{a}_{b}.png");
            lc.Subscribe(drawer);
            lc.Start();
        }
    }
}
