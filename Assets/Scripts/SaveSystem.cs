using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class SaveSystem
{
    public static void Save (UserDataManager userDataManager)
    {
        BinaryFormatter formatter = new BinaryFormatter ();
        string path = Application.persistentDataPath + "/example.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        Data data = new Data(userDataManager);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static Data Load()
    {
        string path = Application.persistentDataPath + "/example.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter ();
            FileStream stream = new FileStream(path, FileMode.Open);
            Data data = null;
            if (stream != null)
            {
               data = formatter.Deserialize(stream) as Data;
            }
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
