using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Variant_2.Task3;
using System.Text.Json;
#region Выберите библиотеку(и) для сериализации
// using Newtonsoft;
//using System.Text.Json;
using System.Text.Json.Serialization;
#endregion
namespace Variant_2
{
    public interface IDataSerializer
    {
        void Write(string path, object obj);
        object Read(string path);
    }

    public interface ICreator
    {
        void CreateFolder(string path, string folderName);
        void CreateFolder(string path, string[] folderNames);
        void CreateFile(string path, string fileName);
        void CreateFile(string path, string[] fileNames);
    }

    public class DataSerializer : IDataSerializer, ICreator
    {
        public void Write(string path, object obj)
        {
            string json = JsonSerializer.Serialize(obj);
            File.WriteAllText(path, json);
        }

        public object Read(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Grep>(json);
        }

        public void CreateFolder(string path, string folderName)
        {
            Directory.CreateDirectory(Path.Combine(path, folderName));
        }

        public void CreateFolder(string path, string[] folderNames)
        {
            foreach (string folderName in folderNames)
            {
                CreateFolder(path, folderName);
            }
        }

        public void CreateFile(string path, string fileName)
        {
            File.Create(Path.Combine(path, fileName)).Close();
        }

        public void CreateFile(string path, string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                CreateFile(path, fileName);
            }
        }
    }
    public class Task4
    {
        private Grep _greper;
        public Grep Greper
        {
            get { return _greper; }
        }
        public Task4(string text)
        {
            _greper = new Grep(text);
        }

        public class DataSerializer
        {
            public DataSerializer()
            {
            }

            public void CreateFile(string path, string[] strings)
            {
                throw new NotImplementedException();
            }

            public void CreateFile(string path, string v)
            {
                throw new NotImplementedException();
            }

            public void CreateFolder(string path, string[] strings)
            {
                throw new NotImplementedException();
            }

            public void CreateFolder(string path, string v)
            {
                throw new NotImplementedException();
            }

            public object Read<T>(string path)
            {
                throw new NotImplementedException();
            }

            public void Write(Task3.Grep greper, string path)
            {
                throw new NotImplementedException();
            }
        }

        public class IDataSerializer : DataSerializer
        {
        }
    }
    public class Program4
    {
        public static void Main(string[] args)
        {
            string text = "This is a test string with some words. Let's see how it works!";
            Task4 task4 = new Task4(text);

            DataSerializer dataSerializer = new DataSerializer();

            // Сохраняем объект Grep в файл
            string filePath = "grep.json";
            dataSerializer.Write(filePath, task4.Greper);

            // Создаем папку и файл
            string folderPath = "data";
            string[] fileNames = { "file1.txt", "file2.txt" };
            dataSerializer.CreateFolder(folderPath, "subfolder");
            dataSerializer.CreateFile(folderPath, fileNames);

            // Чтение объекта Grep из файла
            Grep deserializedGrep = (Grep)dataSerializer.Read(filePath);

            Console.WriteLine("Измененный текст (из файла):\n" + deserializedGrep.ToString());
        }
    }
}