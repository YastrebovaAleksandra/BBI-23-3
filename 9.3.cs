using System;
using System.IO;
using Newtonsoft.Json;

public class JSONSerializer
{
    public void Serialize<T>(T obj, string filePath)
    {
        string json = JsonConvert.SerializeObject(obj);
        File.WriteAllText(filePath, json);
    }

    public T Deserialize<T>(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<T>(json);
    }
}

public class MainProgram
{
    static void Main(string[] args)
    {
        string directoryPath = @"C:\Users\Yastr\OneDrive\Рабочий стол\Учёба\программирование\лаб9";

        string filePathA = Path.Combine(directoryPath, "dataA.json");
        Student[] studentsA = new Student[]
        {
            new Student("Mari", "Ivanova", new int[] { 87, 77, 65, 98 }),
            new Student("Misha", "Smirnov", new int[] { 89, 88, 76, 66 }),
        };
        JSONSerializer serializer = new JSONSerializer();
        serializer.Serialize(studentsA, filePathA);
        Student[] loadedStudentsA = serializer.Deserialize<Student[]>(filePathA);

        foreach (var student in loadedStudentsA)
        {
            student.Print();
        }


        string filePathB = Path.Combine(directoryPath, "dataB.json");
        Student[] studentsB = new Student[]
        {
            new Student("Anton", "Volkov", new int[] { 67, 65, 87, 93 }),
            new Student("Dasha", "Mishina", new int[] { 68, 59, 67, 88 }),
        };
        serializer.Serialize(studentsB, filePathB);
        Student[] loadedStudentsB = serializer.Deserialize<Student[]>(filePathB);

        foreach (var student in loadedStudentsB)
        {
            student.Print();
        }

        string filePathC = Path.Combine(directoryPath, "dataC.json");
        Student[] studentsC = new Student[]
        {
            new Student("Kate", "Savina", new int[] { 80, 79, 62, 59 }),
            new Student("Alex", "Lobov", new int[] { 65, 73, 84, 95 }),
        };
        serializer.Serialize(studentsC, filePathC);
        Student[] loadedStudentsC = serializer.Deserialize<Student[]>(filePathC);

        foreach (var student in loadedStudentsC)
        {
            student.Print();
        }

        foreach (var student in studentsA)
        {
            Console.WriteLine($"Before serialization - {student.FirstName} {student.LastName}: Exams - {string.Join(", ", student.Exams)}");
        }

        foreach (var student in loadedStudentsA)
        {
            Console.WriteLine($"After deserialization - {student.FirstName} {student.LastName}: Average Score - {student.AverageScore}");
        }
    }
}

public class Student
{
    private string _firstNameValue;
    private string _lastNameValue;
    private int[] _exams;
    private double _averageScore;

    public Student(string firstName, string lastName, int[] exams)
    {
        _firstNameValue = firstName;
        _lastNameValue = lastName;
        _exams = exams;
        _averageScore = CalculateAverageScore();
    }

    private double CalculateAverageScore()
    {
        if (_exams == null || _exams.Length == 0)
         {
            return 0.0;
        }

        int sum = 0;
        foreach (int exam in _exams)
        {
            sum += exam;
        }
        return (double)sum / _exams.Length;
    }

    public string FirstName
    {
        get { return _firstNameValue; }
    }

    public string LastName
    {
        get { return _lastNameValue; }
    }

    public int[] Exams
    {
        get { return _exams; }
    }

    public double AverageScore
    {
        get { return _averageScore; }
    }

    public void Print()
    {
        Console.WriteLine($"Student: {_lastNameValue} {_firstNameValue}, Average Score: {_averageScore}");
    }
}
