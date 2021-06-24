using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerData : MonoBehaviour
{

    //player data   
    public int playerFund;
    public int tamarawNumber;
    private void Start()
    {
        string path = Application.persistentDataPath + "/playerData.ss";
        if (File.Exists(path))
        {
            //existing
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();

            playerFund = data.fund;
            tamarawNumber = data.tamarawNumber;
      
        }
        else {
            //not existing, set funds and tamaraw number to default.
            playerFund = 10000;
            tamarawNumber = 100;
        
        }
    }

}
