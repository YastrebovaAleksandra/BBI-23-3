using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Variant_2.Task2;

namespace Variant_2
{
    public struct Point1
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

        public Point1(int x, int y)
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
    }
    public abstract class Fourangle
    {
        protected Point[] points;
        public Fourangle(Point[] points)
        {
            if (points.Length != 4)
            {
                this.points = new Point[4];
            }
            else
            {
                this.points = points;
            }
        }
        public abstract double Length();
        public abstract double Area();
        public override string ToString()
        {
            return $"{GetType().Name} with P = {Length():F2}, S = {Area():F2}";
        }
    }
    public class Square : Fourangle
    {
        public Square(Point[] points) : base(points) { }

        public override double Length()
        {
            double side = points[0].Length(points[1]);
            return side * 4;
        }

        public override double Area()
        {
            double side = points[0].Length(points[1]);
            return side * side;
        }
    }
    public class Rectangle : Fourangle
    {
        public Rectangle(Point[] points) : base(points) { }

        public override double Length()
        {
            double side1 = points[0].Length(points[1]);
            double side2 = points[1].Length(points[2]);
            return 2 * (side1 + side2);
        }
        public override double Area()
        {
            double side1 = points[0].Length(points[1]);
            double side2 = points[1].Length(points[2]);
            return side1 * side2;
        }
    }
    public class Task2
    {
        private Fourangle[] _fourangles;

        public Fourangle[] Fourangles
        {
            get { return _fourangles; }
        }

        public Task2(Fourangle[] fourangles)
        {
            _fourangles = fourangles;
        }
        public void Sorting()
        {
            QuickSort(_fourangles, 0, _fourangles.Length - 1);
        }
        private void QuickSort(Fourangle[] array, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right);
                QuickSort(array, left, pivotIndex - 1);
                QuickSort(array, pivotIndex + 1, right);
            }
        }
        private int Partition(Fourangle[] array, int left, int right)
        {
            Fourangle pivot = array[right];
            int low = left - 1;

            for (int j = left; j < right; j++)
            {
                if (array[j].Area().CompareTo(pivot.Area()) >= 0)
                {
                    low++;
                    Swap(ref array[low], ref array[j]);
                }
            }

            Swap(ref array[low + 1], ref array[right]);
            return low + 1;
        }
        private void Swap(ref Fourangle a, ref Fourangle b)
        {
            Fourangle temp = a;
            a = b;
            b = temp;
        }
        public override string ToString()
        {
            string result = "";
            foreach (Fourangle fourangle in _fourangles)
            {
                result += fourangle.ToString() + "\n";
            }
            return result;
        }

        public class Point
        {
            private double[] doubles;

            public Point(double[] doubles)
            {
                this.doubles = doubles;
            }
        }

        public class Square
        {
            private Point[] points;

            public Square(Point[] points)
            {
                this.points = points;
            }

            public object Points { get; set; }
        }

        public class Fourangle
        {
        }

        public class Rectangle : Fourangle
        {
            private Point[] pointsReversed;

            public Rectangle(Point[] pointsReversed)
            {
                this.pointsReversed = pointsReversed;
            }
        }
    }    
    public class Program2
    {
        public static void Main(string[] args)
        {
            Point[] squarePoints = new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, 1) };
            Point[] rectanglePoints = new Point[] { new Point(0, 0), new Point(2, 0), new Point(2, 1), new Point(0, 1) };
            Fourangle[] fourangles = new Fourangle[] { new Square(squarePoints), new Rectangle(rectanglePoints) };

            Task2 task2 = new Task2(fourangles);

            Console.WriteLine(task2.ToString());

            task2.Sorting();

            Console.WriteLine("\nОтсортированный массив:\n" + task2.ToString());
        }
    }
}