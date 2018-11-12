using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KochCurve
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

    // 通知データ 線分を表すクラス
    public class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }
    }

    // KochCurveクラス
    public class KochCurve : IObservable<Line>
    {


        // コッホ曲線を描く。
        public void Start(int width, int height, int genelation)
        {
            Point p1 = new Point(0, height * 0.7f);
            Point p2 = new Point(width, height * 0.7f);
            Draw(p1, p2, genelation);
            Complete();
        }



        // コッホ曲線を描く下請けメソッド （再帰呼び出しされる）
        // ただし、実際の描画は行わない。購読者側で行う
        private void Draw(Point p1, Point p2, int generation)
        {
            if (generation == 0)
            {
                var info = new Line() { Start = p1, End = p2 };
                Publish(info);
            }
            else
            {
                Point a = new Point((p1.X + (p2.X - p1.X) / 3),
                                    (p1.Y + (p2.Y - p1.Y) / 3));
                Point b = new Point((p2.X - (p2.X - p1.X) / 3),
                                    (p2.Y - (p2.Y - p1.Y) / 3));
                Point c = NextPoint(a, b);
                Draw(p1, a, generation - 1);
                Draw(a, c, generation - 1);
                Draw(c, b, generation - 1);
                Draw(b, p2, generation - 1);
            }
        }

        // 次の点を求める
        private Point NextPoint(Point p1, Point p2)
        {
            double direction = ToDegree(GetSlope(p1, p2));
            Point p3 = new Point(p2.X - p1.X, p2.Y - p1.Y);
            double len = LineLength(p1, p2);
            double nd = (direction + 300) % 360;
            Point p4 = new Point(
                        (float)(Math.Cos(ToRadian(nd)) * len + p1.X),
                        (float)(Math.Sin(ToRadian(nd)) * len + p1.Y));
            return p4;
        }

        // ２点の傾斜を求める （ラジアン）
        private double GetSlope(Point p1, Point p2)
        {
            double vx = p2.X - p1.X;
            double vy = p2.Y - p1.Y;
            double n = Math.Atan(vy / vx);
            if (vx < 0)
                return Math.PI + n;    // 180度を足す
            if (vy < 0)
                return (2 * Math.PI) + n;  // 360度を足す
            return n;

        }

        // ラジアンを度の変換
        private double ToDegree(double rad)
        {
            return rad * 360 / (2 * Math.PI);
        }

        // 度をラジアンに変換
        private double ToRadian(double deg)
        {
            return deg * (2 * Math.PI) / 360;
        }

        // ２点の線分の長さを求める
        private double LineLength(Point p1, Point p2)
        {
            double w = (p2.X - p1.X);
            double h = (p2.Y - p1.Y);
            return Math.Sqrt(w * w + h * h);
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
