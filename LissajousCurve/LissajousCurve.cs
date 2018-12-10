using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LissajousCurve
{
    // 座標
    public class Point
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    // 通知データ  線分を表すクラス
    public class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }
    }

    // リサジュー曲線クラス
    public class LissajousCurve : IObservable<Line>
    {
        private int halfW;
        private int halfH;
        private int pa;
        private int pb;

        public LissajousCurve(int paramA, int paramB, int width, int height)
        {
            pa = paramA;
            pb = paramB;
            halfW = width / 2;
            halfH = height / 2;
        }

        public void Start()
        {
            int startx = (int)(halfW * Math.Sin(0));
            int starty = (int)(halfH * Math.Sin(0));
            double rad = 0.0;
            int prevx = startx;
            int prevy = starty;
            do
            {
                rad += 0.02;
                int x = (int)(halfW * Math.Sin(pa * rad));
                int y = (int)(halfH * Math.Sin(pb * rad));
                Line line = new Line
                {
                    Start = new Point(prevx, prevy),
                    End = new Point(x, y)
                };
                Publish(line);
                prevx = x;
                prevy = y;
            } while (prevx != startx || prevy != starty);
            Complete();
        }

        // 終了を通知する
        private void Complete()
        {
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
        }

        // 状況変化を知らせるために購読者に通知する
        private void Publish(Line state)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(state);
            }
        }

        private List<IObserver<Line>> _observers = new List<IObserver<Line>>();

        public IDisposable Subscribe(IObserver<Line> observer)
        {
            _observers.Add(observer);
            return observer as IDisposable;
        }

    }
}
