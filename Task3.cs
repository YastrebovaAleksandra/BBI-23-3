using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Variant_2.Task3;

namespace Variant_2
{
    public class Grep
    {
        private string _input;
        private string _output;

        public string Input
        {
            get { return _input; }
        }

        public string Output
        {
            get { return _output; }
        }

        public Grep(string text)
        {
            _input = text;
            _output = ProcessText();
        }

        private string ProcessText()
        {
            // Находим самую часто встречающуюся букву
            char mostFrequentChar = _input.ToLower()
              .GroupBy(c => c)
              .OrderByDescending(g => g.Count())
              .First()
              .Key;

            // Удаляем все слова с этой буквой
            return string.Join(" ", _input.Split(' ')
              .Where(word => !word.ToLower().Contains(mostFrequentChar)));
        }

        public override string ToString()
        {
            return Output;
        }
    }

    public class Task3
    {
        private Grep _greper;

        public Grep Greper
        {
            get { return _greper; }
        }

        public Task3(string text)
        {
            _greper = new Grep(text);
        }

        public override string ToString()
        {
            return Greper.ToString();
        }

        public class Grep
        {
            private string nullText;

            public Grep(string nullText)
            {
                this.nullText = nullText;
            }

            public string? Input { get; set; }
            public object Output { get; set; }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            string text = "This is a test string with some words. Let's see how it works!";
            Task3 task3 = new Task3(text);

            Console.WriteLine("Исходный текст:\n" + text);
            Console.WriteLine("\nИзмененный текст:\n" + task3.ToString());
        }
    }
}