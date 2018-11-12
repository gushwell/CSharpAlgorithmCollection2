using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HilbertCurve
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

    public class DrawInfo
    {
        public Point Start { get; set; }
        public Point End { get; set; }
    }

    public class HilbertCurve : IObservable<DrawInfo>
    {
        private float length;
        private float X;
        private float Y;

        public void Start(int size, int n)
        {
            length = (float)(size / Math.Pow(2, n));
            X = size - (float)(size / Math.Pow(2, n + 1));
            Y = (float)(size / Math.Pow(2, n + 1));
            Ldr(n);
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
        }

        //  ┏
        //  ┗
        void Ldr(int n)
        {
            if (n > 0)
            {
                Dlu(n - 1);
                GoLeft();
                Ldr(n - 1);
                GoDown();
                Ldr(n - 1);
                GoRight();
                Urd(n - 1);
            }
        }

        //  ┏┓
        void Urd(int n)
        {
            if (n > 0)
            {
                Rul(n - 1);
                GoUp();
                Urd(n - 1);
                GoRight();
                Urd(n - 1);
                GoDown();
                Ldr(n - 1);
            }
        }

        //  ┓
        //  ┛
        void Rul(int n)
        {
            if (n > 0)
            {
                Urd(n - 1);
                GoRight();
                Rul(n - 1);
                GoUp();
                Rul(n - 1);
                GoLeft();
                Dlu(n - 1);
            }
        }

        //  ┗┛
        void Dlu(int n)
        {
            if (n > 0)
            {
                Ldr(n - 1);
                GoDown();
                Dlu(n - 1);
                GoLeft();
                Dlu(n - 1);
                GoUp();
                Rul(n - 1);
            }
        }

        private void GoRight()
        {
            float newx = X;
            float newy = Y;
            newx = X + length;
            DrawLine(newx, newy);
        }

        private void GoUp()
        {
            float newx = X;
            float newy = Y;
            newy = Y - length;
            DrawLine(newx, newy);
        }

        private void GoDown()
        {
            float newx = X;
            float newy = Y;
            newy = Y + length;
            DrawLine(newx, newy);
        }

        private void GoLeft()
        {
            float newx = X;
            float newy = Y;
            newx = X - length;
            DrawLine(newx, newy);
        }

        private void DrawLine(float newx, float newy)
        {
            var state = new DrawInfo()
            {
                Start = new Point(X, Y),
                End = new Point(newx, newy)
            };
            Publish(state);
            X = newx;
            Y = newy;
        }

        // 状況変化を知らせるために購読者に通知する
        private void Publish(DrawInfo state)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(state);
            }
        }

        private List<IObserver<DrawInfo>> _observers = new List<IObserver<DrawInfo>>();

        public IDisposable Subscribe(IObserver<DrawInfo> observer)
        {
            _observers.Add(observer);
            return observer as IDisposable;
        }
    }
}
