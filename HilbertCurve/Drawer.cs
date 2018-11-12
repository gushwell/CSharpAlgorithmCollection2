using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HilbertCurve
{
    class Drawer : IObserver<DrawInfo>
    {
        private EasyCanvas _canvas;

        private string _filepath;

        public Drawer(int w, int h, string filepath)
        {
            _canvas = new EasyCanvas(w, h);
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

        public void OnNext(DrawInfo value)
        {
            _canvas.DrawLine(value.Start.X, value.Start.Y, value.End.X, value.End.Y, 
                new NGraphics.Color("#0022FF"));
        }
    }
}
