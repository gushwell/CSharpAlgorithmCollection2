using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonCurve
{
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

    public class LineInfo
    {
        public Point Start { get; set; }
        public Point End { get; set; }
    }

    public class DragonCurve : IObservable<LineInfo>
    {

        public void Start(LineInfo line, int genelation)
        {
            Draw(line.Start, line.End, genelation);
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
        }

        private void Draw(Point p1, Point p2, int generation)
        {
            if (generation == 0)
            {
                var info = new LineInfo
                {
                    Start = p1,
                    End = p2
                };
                Publish(info);
            }
            else
            {
                Point c = NextPoint(p1, p2);
                Draw(p1, c, generation - 1);
                Draw(p2, c, generation - 1);
            }
        }

        private Point NextPoint(Point p1, Point p2)
        {
            var direction = ToDegree(GetSlope(p1, p2));
            var p3 = new Point(p2.X - p1.X, p2.Y - p1.Y);
            var len = LineLength(p1, p2) / 2;
            var nlen = Math.Sqrt(len * len * 2);
            var nd = (direction + 315) % 360;
            var p4 = new Point(
                        (float)(Math.Cos(ToRadian(nd)) * nlen + p1.X),
                        (float)(Math.Sin(ToRadian(nd)) * nlen + p1.Y));
            return p4;
        }

        private double GetSlope(Point p1, Point p2)
        {
            var vx = p2.X - p1.X;
            var vy = p2.Y - p1.Y;
            var n = Math.Atan(vy / vx);
            if (vx < 0)
                return Math.PI + n;    // 180度を足す
            if (vy < 0)
                return (2 * Math.PI) + n;  // 360度を足す
            return n;
        }

        private double ToDegree(double rad)
        {
            return rad * 360 / (2 * Math.PI);
        }

        private double ToRadian(double deg)
        {
            return deg * (2 * Math.PI) / 360;
        }

        private double LineLength(Point p1, Point p2)
        {
            var w = (p2.X - p1.X);
            var h = (p2.Y - p1.Y);
            return Math.Sqrt(w * w + h * h);
        }

        // 状況変化を知らせるために購読者に通知する
        private void Publish(LineInfo state)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(state);
            }
        }

        private List<IObserver<LineInfo>> _observers = new List<IObserver<LineInfo>>();

        public IDisposable Subscribe(IObserver<LineInfo> observer)
        {
            _observers.Add(observer);
            return observer as IDisposable;
        }
    }
}
