using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class FileManager
{
    static BinaryFormatter bf = null;
    public static void SaveText(string filePath, string content)
    {
        File.WriteAllText(filePath, content);
    }

    public static void AppendText(string filePath, string content)
    {
        List<string> list = new List<string>();
        list.Add(content);
        File.AppendAllLines(filePath, list);
    }

    public static string LoadText(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        return string.Empty;
    }

    public static string[] LoadAllLines(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllLines(filePath);
        }
        return null;
    }

    public static void SaveToBinary<T>(string filePath, T data)
    {
        if (bf == null) bf = new BinaryFormatter();
        using (FileStream fs = File.Create(filePath))
        {
            bf.Serialize(fs, data);
        }
        //스트림은 사용안하게 되면 꼭 닫아줘야함 : Close(), using 키워드문을 이용해서 블록이 끝날 시 자동으로 닫게 할 수 있음
        //fs.Close();

    }

    public static T LoadFromBinary<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            using(FileStream fs = File.Open(filePath, FileMode.Open))
            {
                if (bf == null) bf = new BinaryFormatter();
                return (T)bf.Deserialize(fs);
            }
        }
        return default;
    }
}
