using NGraphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonCurve
{
    class Drawer : IObserver<LineInfo>
    {
        private EasyCanvas canvas;
        private int generation;

        public Drawer(int w, int h, int generation)
        {
            canvas = new EasyCanvas(w, h);
            this.generation = generation;
        }

        public void OnCompleted()
        {
            canvas.Write($"DragonCurve{generation}.png");
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(LineInfo value)
        {
            canvas.DrawLine(value.Start.X, value.Start.Y, value.End.X, value.End.Y, new Color("#0022FF"));
        }
    }
}
