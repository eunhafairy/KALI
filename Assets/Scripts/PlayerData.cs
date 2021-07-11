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
    public int playerLevel;
    public int playerExp;
    public bool levelFlag2;
    public bool levelFlag3;

    private void Start()
    {
        /*
        string path = Application.persistentDataPath + "/playerData.ss";
        if (File.Exists(path))
        {
            //existing
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();

            playerFund = data.fund;
            tamarawNumber = data.tamarawNumber;
            playerLevel = data.playerLevel;
            playerExp = data.playerExp;
      
        }
        else {
            //not existing, set funds and tamaraw number to default.
            playerFund = 10000;
            tamarawNumber = 100;
            playerLevel = 1;
            playerExp = 0;
        
        }
        */
    }

    private void Update()
    {
        if (playerExp <= 1000) {
            playerLevel = 1;
        }
        else if (playerExp > 1000 && playerExp <= 4000) {
          
            playerLevel = 2;
            if (levelFlag2) levelUp2();
           


        }
        else if (playerExp > 4000 && playerExp <= 6000) { 
            playerLevel = 3;
            if (levelFlag3) levelUp3();


        }


    }

    private void levelUp2() {
        //do something
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.playerLevel();
        Debug.LogWarning("level up 2");
        levelFlag2 = false;
    }
    private void levelUp3()
    {
        //do something
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.playerLevel();
        Debug.LogWarning("level up 2");

        levelFlag3 = false;
    }

}
