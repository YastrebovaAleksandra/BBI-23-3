using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Variant_2
{
    public struct Point
    {
        private int x;
        private int y;
        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return $"x = {x}, y = {y}";
        }
        public double Length(Point other)
        {
            return Math.Round(Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2)), 2);
        }
        public static string GetDistanceInfo(Point point1, Point point2)
        {
            return $"Точка 1: {point1}\nТочка 2: {point2}\nРасстояние между точками: {point1.Length(point2)}";
        }
    }
    public class Task1
    {
        private Point[] _points;
        public Point[] Points
        {
            get { return _points; }
        }
        public Task1(Point[] points)
        {
            if (points.Length != 2)
            {
                throw new ArgumentException("Массив должен содержать две точки.");
            }
            _points = points;
        }
        public override string ToString()
        {
            string result = "";
            foreach (Point point in _points)
            {
                result += point.ToString() + "\n";
            }
            return result;
        }
        public void Sorting()
        {
            QuickSort(_points, 0, _points.Length - 1);
        }
        private void QuickSort(Point[] points, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(points, left, right);
                QuickSort(points, left, pivotIndex - 1);
                QuickSort(points, pivotIndex + 1, right);
            }
        }
        private int Partition(Point[] points, int left, int right)
        {
            Point pivot = points[right];
            int low = left - 1;

            for (int j = left; j < right; j++)
            {
                double dist1 = Math.Sqrt(Math.Pow(points[j].X, 2) + Math.Pow(points[j].Y, 2));
                double dist2 = Math.Sqrt(Math.Pow(pivot.X, 2) + Math.Pow(pivot.Y, 2));

                if (dist1 <= dist2)
                {
                    low++;
                    Swap(ref points[low], ref points[j]);
                }
            }

            Swap(ref points[low + 1], ref points[right]);
            return low + 1;
        }
        private void Swap(ref Point a, ref Point b)
        {
            Point temp = a;
            a = b;
            b = temp;
        }

        public class Point
        {
            private double[] doubles;

            public Point()
            {
            }

            public Point(double[] doubles)
            {
                this.doubles = doubles;
            }

            public decimal Length(Point zero)
            {
                throw new NotImplementedException();
            }
        }
    }
    public class Program1
    {
        public static void Main(string[] args)
        {
            Point point1 = new Point(1, 2);
            Point point2 = new Point(3, 4);
            Console.WriteLine(Point.GetDistanceInfo(point1, point2));

            Task1 task1 = new Task1(new Point[] { point1, point2 });
            Console.WriteLine(task1.ToString());

            task1.Sorting();
            Console.WriteLine("Отсортированный массив:\n" + task1.ToString());
        }
    }
}