using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{

    public static void SavePlayer(PlayerData player, BarracksScript barracks, ClinicScript clinic, WardScript ward, officeScript office, researchScript research) {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "playerData.ss");
        FileStream stream = new FileStream(path, FileMode.Create);
        Data data = new Data(player, barracks, clinic, ward, office, research);

        formatter.Serialize(stream, data);
        stream.Close();



    }
    public static Data loadPlayer() {

        string path = Path.Combine(Application.persistentDataPath, "playerData.ss");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();
            return data;
        }
        else {
            Debug.LogError("Save file not found in: "+path);
            return null;
        }

    }

}
