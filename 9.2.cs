using System;
using System.IO;
using Newtonsoft.Json;

class Serializer
{
    public void SerializeToJson(object obj, string filePath)
    {
        string json = JsonConvert.SerializeObject(obj);
        File.WriteAllText(filePath, json);
    }

    public T DeserializeFromJson<T>(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<T>(json);
    }
}

class Human
{
    protected string _name;
    protected int _wincount;
    protected double _drawcount;
    protected int _loosecount;
    protected double _finalscore;

    public Human(string name, int wins, double draws, int looses)
    {
        _name = name;
        _wincount = wins;
        _drawcount = draws;
        _loosecount = looses;
        _finalscore = wins * 1 + draws / 2;
    }

    public double Finalscore => _finalscore;

    public virtual void Print()
    {
        Console.WriteLine("{0,-5} | {1,-3} | {2,-3} | {3,-7} | {4,-3}", _name, _wincount, _drawcount, _loosecount, _finalscore);
    }
}

class Athlete : Human
{
    static int _id = 0;
    private int ID;

    public Athlete(string name, int wins, double draws, int looses) : base(name, wins, draws, looses)
    {
        _id++;
        ID = _id;
    }

    public override void Print()
    {
        Console.WriteLine("{5,-3} | {0,-5} | {1,-3} | {2,-3} | {3,-7} | {4,-3}", _name, _wincount, _drawcount, _loosecount, _finalscore, ID);
    }
}

class Program
{
    static void EnsureDirectoryExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
    static void Main(string[] args)
    {

        string filePath = "participants.json";


        string directoryPath = Path.Combine(Environment.CurrentDirectory, "participants.json");
        EnsureDirectoryExists(directoryPath);

        Athlete[] participants = new Athlete[7]
        {
            new Athlete("Vlad", 1, 2, 7),
            new Athlete("Pavel", 4, 3, 2),
            new Athlete("Lera", 0, 2, 9),
            new Athlete("Diana", 2, 8, 1),
            new Athlete("Vera", 3, 3, 7),
            new Athlete("Denis", 3, 6, 3),
            new Athlete("Anton", 4, 6, 3)
        };

        Console.WriteLine("Список участников");
        Console.WriteLine("Имя   Победы Ничьи Поражения Результат");
        for (int i = 0; i < participants.Length; i++)
        {
            participants[i].Print();
        }

        Serializer serializer = new Serializer();


        try
        {
            serializer.SerializeToJson(participants, filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка сохранения данных: " + ex.Message);
        }


        try
        {
            Athlete[] loadedParticipants = serializer.DeserializeFromJson<Athlete[]>(filePath);
            Console.WriteLine("\nЗагруженные участники");
            Console.WriteLine("Имя   Победы Ничьи Поражения Результат");
            for (int i = 0; i < loadedParticipants.Length; i++)
            {
                loadedParticipants[i].Print();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка загрузки данных: " + ex.Message);
        }

        Console.ReadKey();
    }
}