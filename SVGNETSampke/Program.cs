using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Svg;

namespace SVGNETSampke
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample01();
            Sample02();
        }

 
        private static void Sample01()
        {
            var svgDoc = new SvgDocument
            {
                Width = 500,
                Height = 500
            };

            svgDoc.ViewBox = new SvgViewBox(-250, -250, 500, 500);

            var group = new SvgGroup();
            svgDoc.Children.Add(group);

            group.Children.Add(new SvgCircle
            {
                Radius = 100,
                Fill = new SvgColourServer(Color.FromArgb(43, 138, 190)),
                Stroke = new SvgColourServer(Color.FromArgb(32, 101, 139)),
                StrokeWidth = 2
            });

            group.Children.Add(new SvgLine()
            {
                StartX = 0,
                StartY = -120,
                EndX = 0,
                EndY = 120,
                Stroke = new SvgColourServer(Color.LightGray),
                StrokeWidth = 3
            });

            group.Children.Add(new SvgRectangle()
            {
                X = -200,
                Y = -160,
                Width = 80,
                Height = 60,
                Fill = new SvgColourServer(Color.FromArgb(0, 170, 180))
            });

            var group2 = new SvgGroup();
            group.Children.Add(group2);
            for (int x = -100; x < 0; x += 5)
            {
                group2.Children.Add(new SvgRectangle()
                {
                    X = x,
                    Y = -210,
                    Width = 3,
                    Height = 5,
                    Fill = new SvgColourServer(Color.DarkGray)
                });
            }
            svgDoc.Write("sample.svg");
        }

        private static void Sample02()
        {
            var svgDoc = new SvgDocument
            {
                Width = 500,
                Height = 500
            };

            svgDoc.ViewBox = new SvgViewBox(0, 0, 250, 250);

            var group = new SvgGroup();
            svgDoc.Children.Add(group);

            var seglist = SvgPathBuilder.Parse("M 10,60 L 30,10 90,10 110,60 z");

            group.Children.Add(new SvgPath
            {
                StrokeWidth = 1,
                Fill = new SvgColourServer(Color.FromArgb(152, 225, 125)),
                Stroke = new SvgColourServer(Color.DarkGray),
                PathData = seglist
            });


            group.Children.Add(new SvgText
            {
                Nodes = { new SvgContentNode { Content = "Sample" } },
                X = { 30 },
                Y = { 50 },
                Fill = new SvgColourServer(Color.White),
                FontSize = 18,
                FontFamily = "sans-serif"
            });

            svgDoc.Write("sample2.svg");
        }

    }
}
