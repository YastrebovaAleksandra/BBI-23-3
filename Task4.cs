using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Variant_1
{
    public abstract class AbstractSerializer
    {
        public abstract void Write<T>(T obj, string path);
        public abstract T Read<T>(string path);

        public void CreateFolder(string path, string folderName)
        {
            Directory.CreateDirectory(Path.Combine(path, folderName));
        }

        public void CreateFile(string path, string fileName)
        {
            File.Create(Path.Combine(path, fileName)).Close();
        }

        public void CreateFolders(string path, string[] folderNames)
        {
            foreach (var folderName in folderNames)
            {
                CreateFolder(path, folderName);
            }
        }

        public void CreateFiles(string path, string[] fileNames)
        {
            foreach (var fileName in fileNames)
            {
                CreateFile(path, fileName);
            }
        }
    }

    public class Task4
    {
    
    }

    public class Searcher
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }

    public class SearcherSerializer : AbstractSerializer
    {
        public override void Write<T>(T obj, string path)
        {
            string json = JsonSerializer.Serialize(obj);
            File.WriteAllText(path, json);
        }

        public override T Read<T>(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}