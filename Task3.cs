
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Variant_1
{
    public class Task3
    {
        public class Searcher
        {

            private string input;
            private string[] output;

            public string Input
            {
                get { return input; }
            }
            public string[] Output
            {
                get { return output; }
            }

            public Searcher(string text)
            {
                input = text;
                output = FindDuplicateWords(text);
            }

            public string[] FindDuplicateWords(string text)
            {
                var words = text.Split(new[] { ' ', '.', ',', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

                var repeatedWords = new List<string>();
                var checkedWords = new List<string>();

                for (int i = 0; i < words.Length; i++)
                {
                    if (!checkedWords.Contains(words[i]))
                    {
                        int count = 0;
                        for (int j = 0; j < words.Length; j++)
                        {
                            if (words[i] == words[j])
                            {
                                count++;
                            }
                        }

                        if (count > 1) 
                        {
                            repeatedWords.Add(words[i]);
                        }

                        checkedWords.Add(words[i]);
                    }
                }

                return repeatedWords.ToArray();
            }

            public override string ToString()
            {
                return string.Join(" ", output);
            }
        }
    }
}
