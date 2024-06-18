using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Variant_1
{
    public class Task1
    {
        public class Number
        {
            private double real;

            public double Real
            {
                get { return real; }
            }

            public Number(double realNum)
            {
                this.real = realNum;
            }

            public Number Add(Number num)
            {
                return new Number(this.real + num.real);
            }

            public Number Sub(Number other)
            {
                return new Number(this.real - other.real);
            }

            public Number Mul(Number other)
            {
                return new Number(this.real * other.real);
            }

            public Number Div(Number num)
            {
                if (num.real != 0)
                    return new Number(this.real / num.real);
                else
                    return new Number(0);
            }

            public override string ToString()
            {
                return $"Number = {this.real}";
            }
        }

        private Number[] numbers;

        public Number[] Numbers
        {
            get { return numbers; }
        }

        public Task1(Number[] number)
        {
            this.numbers = number;
        }

        public void Sorting() // insertion sort
        {
            for (int i = 1; i < numbers.Length; i++)
            {
                Number current = numbers[i];
                int j = i - 1;

                while (j >= 0 && numbers[j].Real > current.Real)
                {
                    numbers[j + 1] = numbers[j];
                    j--;
                }
                numbers[j + 1] = current;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (var number in numbers)
            {
                result.AppendLine(number.ToString());
            }

            return result.ToString();
        }
    }
}
