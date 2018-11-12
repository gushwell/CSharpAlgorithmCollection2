using NGraphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CellAutomata
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 256; i++)
            {
                int rule = i;
                int generations = 180;
                int width = generations * 2;
                var ca = new CellularAutomaton(rule, width);
                var lines = ca.AllGenerations().Take(generations);
                MakeImage(rule, lines, width);
            }
        }

        static void MakeImage(int rule, IEnumerable<bool[]> lines, int width)
        {
            var canvas = new EasyCanvas(width, width / 2 + 10);
            int y = 10;
            foreach (var line in lines)
            {
                for (int x = 0; x < width; x++)
                {
                    if (line[x])
                        canvas.SetPixel(x, y, Brushes.DarkGray);
                }
                y++;
            }
            canvas.Write($"Rule{rule}.png");
        }
    }
}