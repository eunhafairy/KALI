using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerData player;
    [SerializeField] BarracksScript barracks;
    [SerializeField] GameObject[] rangers;
    // Start is called before the first frame update
    public void SavePlayer() {

        SaveSystem.SavePlayer(player, barracks);

    
    }

    public void LoadPlayer() {

        Data data = SaveSystem.loadPlayer();

        player.tamarawNumber = data.tamarawNumber;
        player.playerFund = data.fund;
        
        barracks.level = data.barracksLevel;
        barracks.noRangers = data.noRangers;
        
        for (int x = 0; x < barracks.noRangers; x++) {
            barracks.transform.GetChild(x).gameObject.GetComponent<RangerScript>().energy = data.rangerEnergy[x];
        
        }
    }

    public void deleteFiles() {

        string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (string filePath in filePaths) {
            File.Delete(filePath);
       
        }

    
    }
}
