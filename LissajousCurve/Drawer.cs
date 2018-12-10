using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LissajousCurve
{
    class Drawer : IObserver<Line>
    {
        private EasyCanvas _canvas;

        private string _filepath;
        private int _width;
        private int _height;
        private int margin = 20;

        public Drawer(int w, int h, string filepath)
        {
            _canvas = new EasyCanvas(w + margin*2, h + margin * 2);
            _width = w;
            _height = h;
            _filepath = filepath;
        }
        public void OnCompleted()
        {
            _canvas.Write(_filepath);
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Line value)
        {
            var x1 = value.Start.X + _width / 2.0f + margin;
            var y1 = value.Start.Y + _height / 2.0f + margin;
            var x2 = value.End.X + _width / 2.0f + margin;
            var y2 = value.End.Y + _height / 2.0f + margin;
            _canvas.DrawLine(x1, y1, x2, y2, 
                new NGraphics.Color("#0022FF"));
        }
    }
}
