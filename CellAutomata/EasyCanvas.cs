using NGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellAutomata
{
    class EasyCanvas
    {
        private IImageCanvas canvas;

        static EasyCanvas()
        {
            // .NET Coreのパッケージの場合は、このコードが必要？
            //Platforms.SetPlatform<SystemDrawingPlatform>();
        }
        public EasyCanvas(int width, int height)
        {
            canvas = Platforms.Current.CreateImageCanvas(new Size(width, height), scale: 2);
        }

        public void SetPixel(int x, int y, SolidBrush brush)
        {
            canvas.FillRectangle(x, y, 1, 1, brush);
        }

        public void Write(string path)
        {
            canvas.GetImage().SaveAsPng(path);
        }
    }
}