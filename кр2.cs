using System.IO;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
abstract class Task
{
    protected string text = "No text here yet";
    public string Text
    {
        get => text;
        protected set => text = value;
    }
    public Task(string text)
    {
        this.text = text;
    }
}
class Task1 : Task
{
    private const string RussianAlphabetLowercase = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
    private const string RussianAlphabetUppercase = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
    [JsonConstructor]
    public Task1(string text) : base(text)
    {
        this.text = text;
        ShiftText();
    }
    public void ShiftText()
    {
        string shiftedText = "";
        int alphabetLength = RussianAlphabetLowercase.Length;
        foreach (char c in text)
        {
            string charInString = c.ToString();
            if (RussianAlphabetLowercase.Contains(charInString))
            {
                int index = (RussianAlphabetLowercase.IndexOf(c) + 10) % alphabetLength;
                shiftedText += RussianAlphabetLowercase[index];
            }
            else if (RussianAlphabetUppercase.Contains(charInString))
            {
                int index = (RussianAlphabetUppercase.IndexOf(c) + 10) % alphabetLength;
                shiftedText += RussianAlphabetUppercase[index];
            }
            else
            {
                shiftedText += c;
            }
        }
        text = shiftedText.ToString();
    }
    public override string ToString()
    {
        return text;
    }
}
class Task2 : Task
{
    private int amount;
    public int Amount
    {
        get => amount;
        private set => amount = value;
    }
    [JsonConstructor]
    public Task2(string text) : base(text)
    {
        this.amount = countUniqueChars();
    }
    public int countUniqueChars()
    {
        bool[] foundLetters = new bool[char.MaxValue];
        int count = 0;
        foreach (char c in text.ToLower())
        {
            if (char.IsLetter(c) && !foundLetters[c])
            {
                foundLetters[c] = true;
                count++;
            }
        }
        return count;
    }
    public override string ToString()
    {
        return amount.ToString();
    }
}
class JsonIO
{
    public static void Write<T>(T obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, obj);
        }
    }
    public static T Read<T>(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<T>(fs);
        }
        return default(T);
    }
}
class Program
{
    static public void MakeJsons(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string firstFilePath = Path.Combine(path, "cw2_1.json");
        string secondFilePath = Path.Combine(path, "cw2_2.json");
        if (!File.Exists(firstFilePath))
        {
            File.WriteAllText(firstFilePath, "");
        }
        if (!File.Exists(secondFilePath))
        {
            File.WriteAllText(secondFilePath, "");
        }
    }
    static void Main()
    {
        Task[] tasks = { new Task1("нупагкгаиука"), new Task2("ывыыцццпишш") };
        Console.WriteLine(tasks[0]);
        Console.WriteLine(tasks[1]);
        string path = "C:\\Users\\Ястребова Александра\\Test";
        string firstFilePath = Path.Combine(path, "cw2_1.json");
        string secondFilePath = Path.Combine(path, "cw2_2.json");
        MakeJsons(path);
        Console.WriteLine(firstFilePath);
        if (!File.Exists(secondFilePath))
        {
            JsonIO.Write<Task1>((Task1)tasks[0], firstFilePath);
            JsonIO.Write<Task2>((Task2)tasks[1], secondFilePath);
        }
        else
        {
            var t1 = JsonIO.Read<Task1>(firstFilePath);
            var t2 = JsonIO.Read<Task2>(secondFilePath);
            Console.WriteLine(t1);
            Console.WriteLine(t2);
        }

    }

}