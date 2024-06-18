using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Variant_1
{
    public class Task2
    {
        public class Number
        {
            private double real;
            public double Real
            {
                get
                {
                    return real;
                }
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
                return "Number = {this.real}";
            }
        }
        public class ComplexNumber : Number
        {
            public double Imagine { get; }

            public ComplexNumber(double real, double imagine) : base(real)
            {
                Imagine = imagine;
            }

            public ComplexNumber Add(ComplexNumber number)
            {
                return new ComplexNumber(this.Real + number.Real, this.Imagine + number.Imagine);
            }

            public ComplexNumber Sub(ComplexNumber number)
            {
                return new ComplexNumber(this.Real - number.Real, this.Imagine - number.Imagine);
            }
            public ComplexNumber Mul(ComplexNumber number)
            {
                double real = this.Real * number.Real - this.Imagine * number.Imagine;
                double imagine = this.Real * number.Imagine + this.Imagine * number.Real;

                return new ComplexNumber(real, imagine);
            }
            public ComplexNumber Div(ComplexNumber number)
            {
                double denominator = number.Real * number.Real + number.Imagine * number.Imagine;
                double real = (this.Real * number.Real + this.Imagine * number.Imagine) / denominator;
                double imagine = (this.Imagine * number.Real - this.Real * number.Imagine) / denominator;

                if (number.Real != 0)
                    return new ComplexNumber(real, imagine);
                else
                    return new ComplexNumber(0, 0);
            }
            public override string ToString()
            {
                return $"Number = {Real} {(Imagine >= 0 ? "+" : "-")} {Math.Abs(Imagine)}i"; //math.abs imagine part always positive
            }
        }
        private ComplexNumber[] numbers;
        public ComplexNumber[] Numbers
        {
            get { return numbers; }
        }
        public Task2(ComplexNumber[] number)
        {
            this.numbers = number;
        }
        public void Sorting()
        {
            for (int i = 1; i < numbers.Length; i++)
            {
                Number key = numbers[i];
                double keyModulus = 0;
                if (key is ComplexNumber complexKey)
                {
                    keyModulus = Math.Sqrt(complexKey.Real * complexKey.Real + complexKey.Imagine * complexKey.Imagine);
                }
                int j = i - 1;
                while (j >= 0)
                {
                    double currentModulus;
                    if (numbers[j] is ComplexNumber complexNumber)
                    {
                        currentModulus = Math.Sqrt(complexNumber.Real * complexNumber.Real + complexNumber.Imagine * complexNumber.Imagine);
                    }
                    else
                    {
                        currentModulus = Math.Abs(numbers[j].Real);
                    }
                    if (currentModulus > keyModulus)
                    {
                        numbers[j + 1] = numbers[j];
                        j--;
                    }
                }
                numbers[j + 1] = (ComplexNumber)key;
            }
        }
        public override string ToString()
        {
            string result = string.Empty;
            foreach (var number in numbers)
            {
                result += number.ToString() + Environment.NewLine;
            }
            return result;
        }
    }
}