using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
public class BarracksScript : MonoBehaviour
{
    
    [SerializeField] GameObject progressPrefab, encounterPanel;
    [SerializeField] Transform progressPanel;
    public int level, noRangers, no_of_avail, maxRangers;
    public int[] rangerEnergy;
    [SerializeField] int rangerCost, availableRanger;
    [SerializeField] TextMeshProUGUI tmp_brk_lvl, tmp_no_of_ranger;
    [SerializeField] GameObject rangers; //prefab reference
    GameObject[] ranger, progressCard; //objects
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

            rangerEnergy[0] = 100;
            ranger[0].GetComponent<RangerScript>().energy = 100;
            
        }



        //for ranger's energy progress
        progressCard = new GameObject[6];

        for (int x = 0; x < noRangers; x++) {

            Instantiate(progressPrefab, progressPanel);


        }
        for (int x = 0; x < noRangers; x++)
        {

            progressCard[x] = progressPanel.GetChild(x).gameObject;
            
        }


    }

    // Update is called once per frame
    void Update()
    {


        tmp_brk_lvl.SetText("Barracks (Level "+level+")");
        tmp_no_of_ranger.SetText(getAvailable() + "/" + noRangers);

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


        progressBar();

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
                Instantiate(progressPrefab, progressPanel);
                progressCard = new GameObject[6];
                for (int x = 0; x < noRangers; x++)
                {

                    progressCard[x] = progressPanel.GetChild(x).gameObject;

                }
                Time.timeScale = 0f;
                encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
                encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("+1 Ranger!");
                encounterPanel.SetActive(true);

            }
            else {
                //create warning here
                Debug.LogError("Insufficient Funds");
                Time.timeScale = 0f;
                encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Warning");
                encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Insufficient Funds.");
                encounterPanel.SetActive(true);
            }
            
        }
        else {

            //warning here
            Debug.LogError("Max number of rangers achieved already!!!!");

            Time.timeScale = 0f;
            encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Warning");
            encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Max capacity of Barracks reached! Level up the barrakcs for increased max capacity.");
            encounterPanel.SetActive(true);
        }
    
    
    }

    private int getAvailable() {

        int ctr = 0;
        
        
        for (int x = 0; x < noRangers; x++)
        {

            if (transform.GetChild(x).gameObject.GetComponent<RangerScript>().energy == 100) {
                ctr++;
            } 

        }
        return ctr;


    }

    private void progressBar() {

        for (int x = 0; x < noRangers; x++)
        {

            progressCard[x].transform.GetChild(2).gameObject.GetComponent<Slider>().value = transform.GetChild(x).gameObject.GetComponent<RangerScript>().energy; 

        }

    }
}
