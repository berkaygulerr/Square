using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveManager
{
    public static string directory = "data";

    public static void Save<T> (T data, string fileName)
    {
        if (!DirectoryExists())
            Directory.CreateDirectory(Application.persistentDataPath + "/" + directory);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetFullPath(fileName));
        bf.Serialize(file, data);
        file.Close();
    }
    public static T Load<T> (T t, string fileName)
    {
        if (SaveExists(fileName))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(GetFullPath(fileName), FileMode.Open);
                T data = (T)bf.Deserialize(file);
                file.Close();

                return data;
            }
            catch (SerializationException)
            {
                Debug.Log("Failed to load file");
            }
        }

        return t;
    }
    public static bool SaveExists(string fileName)
    {
        return File.Exists(GetFullPath(fileName));
    }
    private static bool DirectoryExists()
    {
        return Directory.Exists(Application.persistentDataPath + "/" + directory);
    }
    private static string GetFullPath(string fileName)
    {
        return Application.persistentDataPath + "/" + directory + "/" + fileName;
    }
}
