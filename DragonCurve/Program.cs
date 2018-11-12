using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonCurve
{
    class Program
    {
        static void Main(string[] args)
        {
            int generation = 5;
            int size = 500;
            var dragon = new DragonCurve();
            var drawer = new Drawer(size, size, generation);
            dragon.Subscribe(drawer);
            // 初期の線
            var line = new LineInfo
            {
                Start = new Point(size * 0.25f, size * 0.55f),
                End = new Point(size * 0.75f, size * 0.55f)
            };
            dragon.Start(line, generation);
        }
    }
}
