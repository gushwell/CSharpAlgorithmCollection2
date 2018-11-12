using NGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HilbertCurve
{
    class EasyCanvas
    {
        private IImageCanvas canvas;

        static EasyCanvas()
        {
        }

        public EasyCanvas(int width, int height)
        {
            canvas = Platforms.Current.CreateImageCanvas(new Size(width, height), scale: 2);
        }

        public void SetPixel(int x, int y, NGraphics.SolidBrush brush)
        {
            canvas.FillRectangle(x, y, 1, 1, brush);
        }

        public void Write(string path)
        {
            canvas.GetImage().SaveAsPng(path);
        }

        internal void DrawLine(float x1, float y1, float x2, float y2, Color color)
        {
            canvas.DrawLine(x1, y1, x2, y2, color);
        }
    }

}
