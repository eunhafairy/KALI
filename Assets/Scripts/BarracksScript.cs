using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class BarracksScript : MonoBehaviour
{

    public int level, noRangers, no_of_avail, maxRangers;
    public int[] rangerEnergy;
    [SerializeField] int rangerCost;
    [SerializeField] TextMeshProUGUI tmp_brk_lvl, tmp_no_of_ranger;
    [SerializeField] GameObject rangers; //prefab reference
    GameObject[] ranger; //objects
    public PlayerData player;
    void Start()
    {

        ranger = new GameObject[6];
        rangerEnergy = new int[6];


        string path = Application.persistentDataPath + "/playerData.ss";

        if (File.Exists(path))
        {
            //existing
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();
            level = data.barracksLevel;
            noRangers = data.noRangers;
            rangerEnergy = data.rangerEnergy;

            for (int x = 0; x < rangerEnergy.Length; x++) {
                Debug.Log("Ranger Energy: "+rangerEnergy[x]);
            }
            //instantiate rangers prefabs as child based on noRangers
            for (int x = 0; x < noRangers; x++)
            {
                Instantiate(rangers, this.transform);
                
            }
            for (int x = 0; x < noRangers; x++) {
                transform.GetChild(x).gameObject.GetComponent<RangerScript>().energy = rangerEnergy[x];

            }


        }
        else
        {
            //not existing, set funds and tamaraw number to default.
            level = 1;
            noRangers = 1;
            Instantiate(rangers, this.transform);   
            ranger[0] = transform.GetChild(0).gameObject;
   
            rangerEnergy[0] = ranger[0].GetComponent<RangerScript>().energy;
           
        }

      





    }

    // Update is called once per frame
    void Update()
    {
        
        tmp_brk_lvl.SetText("Ranger's Barracks (Level "+level+")");
        tmp_no_of_ranger.SetText(noRangers + "");

        switch (level) {

            case 1:
                maxRangers = 2;
                break;
            case 2:
                maxRangers = 4;
                break;
            case 3:
                maxRangers = 6;
                break;


        }

        switch (noRangers) {

            case 1:
                rangerCost = 4000;
                break;
            case 2:
                rangerCost = 6000;
                break;
            case 3:
                rangerCost = 9000;
                break;
            case 4:
                rangerCost = 13500;
                break;
            case 5:
                rangerCost = 20250;
                break;
            case 6:
                rangerCost = 30750;
                break;


        }

        for (int x = 0; x < noRangers; x++) {

            rangerEnergy[x] = transform.GetChild(x).gameObject.GetComponent<RangerScript>().energy;

        }
       
      
    }

    public void addRangers() {

        if (noRangers < maxRangers)
        {
            //add one ranger
            if (rangerCost < player.playerFund)
            {
                player.playerFund -= rangerCost;
                noRangers++;

                //instantiate ranger prefab as child
                Instantiate(rangers, transform);

            }
            else {
                //create warning here
                Debug.LogError("Insufficient Funds");
            }
            
        }
        else {

            //warning here
            Debug.LogError("Max number of rangers achieved already!!!!");
        }
    
    
    }
}
