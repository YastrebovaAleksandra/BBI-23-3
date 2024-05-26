using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

class Program
{

    abstract class StringTask
    {
        protected string text;

        protected StringTask(string text)
        {
            this.text = text;
        }

        protected IEnumerable<string> getWords()
        {
            var punctuation = text.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = text.Split().Select(x => x.Trim(punctuation));
            return words;
        }
    }

    class Task1 : StringTask
    {
        private int[] data = new int[256 * 256];
        public Task1(string text) : base(text)
        {
            string new_text = text.ToLower();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 0;
            }
            for (int i = 0; i < text.Length; i++)
            {
                data[new_text[i]] += 1;
            }
        }
        public override string ToString()
        {
            string res = "номер 1:\n";
            for (char c = 'а'; c <= 'я'; c++)
            {
                if (data[(int)c] == 0)
                {
                    res += c + ": Нет\n";
                }
                else
                {
                    res += c + ": " + data[(int)c] + " раз(а) - " + ((float)data[(int)c] / text.Length * 100).ToString() + "%\n";
                }

            }
            return res;
        }
    }

    class Task2 : StringTask
    {
        private string res;
        public Task2(string text) : base(text)
        {
            res = "";
            var words = getWords();

            int free_space = 50;
            bool is_new_row = true;
            foreach (string word in words)
            {
                if (!is_new_row)
                {
                    if (free_space - word.Length - 1 >= 0)
                    {
                        res += " " + word;
                        free_space -= word.Length - 1;
                    }
                    else
                    {
                        res += "\n" + word;
                        free_space = 50 - word.Length;
                    }
                }
                else
                {
                    res = word;
                    free_space -= word.Length;
                    is_new_row = false;
                }
            }

        }

        public override string ToString()
        {
            return res;
        }
    }

    class Task3 : StringTask
    {
        private int[] data;
        public Task3(string text) : base(text)
        {
            data = new int[5000];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 0;
            }
            var words = getWords();
            foreach (string word in words)
            {
                if (word.Length == 0)
                {
                    continue;
                }
                data[(int)word[0]]++;
            }
        }

        public override string ToString()
        {
            string res = "номер 3:\n";
            string used = "";
            int max_val = 0;
            char max_c = ' ';

            var words = getWords();
            while (max_val != -1)
            {
                max_val = -1;
                max_c = ' ';
                foreach (string word in words)
                {
                    if (word.Length == 0)
                    {
                        continue;
                    }
                    char symbol = word[0];
                    if ((!used.Contains(symbol)) && (max_val < data[(int)symbol]))
                    {
                        max_val = data[(int)symbol];
                        max_c = symbol;
                    }
                }
                if (max_val != -1)
                {
                    used += max_c;
                    res += $"-> {max_c} - {max_val} раз(а)\n";
                }
            }
            return res;
        }

    }

    class Task4 : StringTask
    {
        private string res;

        public Task4(string text) : base(text)
        {
        }

        public Task4(string text, string sub) : base(text)
        {
            res = "";
            var words = getWords();
            foreach (var word in words)
            {
                if (words.Contains(sub))
                {
                    res += " " + word;
                }
            }
        }
        public override string ToString()
        {
            return res;
        }
    }

    class Task5 : StringTask
    {
        public override string Process(string input)
        {
            string[] surnames = input.Split(',').Select(s => s.Trim()).OrderBy(s => s).ToArray();
            return string.Join(", ", surnames);
        }
    }


    class Task6 : StringTask
    {
        private int cnt;
        public Task6(string text) : base(text)
        {
            var words = getWords();
            foreach (var word in words)
            {
                int n;
                bool isNumeric = int.TryParse(word, out n);
                if (isNumeric)
                {
                    cnt += n;
                }
            }
        }
        public override string ToString()
        {
            string res = "Сумма чисел: " + cnt.ToString();
            return res;
        }
    }

    static void Main(string[] args)
    {
        string s = "Дружба - это важно. Настоящий друг всегда рядом, поддержит и обрадует. В дружбе главное - искренность и уважение. Не забывайте ценить и беречь друзей, они делают жизнь лучше.";
        string surnames = "Иванов\nПетров\nСмирнов\nСоколов\nКузнецов\nПопов\nЛебедев\nВолков\nКозлов\nНовиков\nИванова\nПетрова\nСмирнова";

        Task1 a = new Task1(s);
        Console.WriteLine(a.ToString());

        Task2 b = new Task2(s);
        Console.WriteLine(b.ToString());

        Task3 c = new Task3(s);
        Console.WriteLine(c.ToString());

        Task4 d = new Task4(s);
        Console.WriteLine(d.ToString());

        Task5 e = new Task5(surnames);
        Console.WriteLine(e.ToString());

        Task6 f = new Task6(s);
        Console.WriteLine(f.ToString());
    }
}