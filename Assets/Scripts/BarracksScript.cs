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
    [SerializeField] Button upgradebutton;
    [SerializeField] Transform progressPanel;
    public int level, noRangers, no_of_avail, maxRangers;
    public int[] rangerEnergy;
    public int rangerCost, availableRanger;
    [SerializeField] TextMeshProUGUI tmp_brk_lvl, tmp_no_of_ranger;
    [SerializeField] GameObject rangers, barracksUpgrade; //prefab reference
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

        

        if (level <= player.playerLevel)
        {
            upgradebutton.interactable = true;

        }
        else {
            upgradebutton.interactable = false;
        }
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
            if (rangerCost <= player.playerFund)
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
            encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Max capacity of Barracks reached! Level up the barracks for increased max capacity.");
            encounterPanel.SetActive(true);
        }
    
    
    }

    public void upgradeMessage() {
        barracksUpgrade.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level "+level);
        barracksUpgrade.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level " + (level+1));
        int barrackUpgradeCost = 0;
        switch (level) {
            case 1:
                barrackUpgradeCost = 10000;
                break;
            case 2:
                barrackUpgradeCost = 30000;
                break;
            case 3:
                barrackUpgradeCost = 60000;
                break;


        }
        barracksUpgrade.transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>().SetText("Php "+ barrackUpgradeCost.ToString("N"));



    }
    public void upgradeBarracks() {

        switch (level) {

            case 1:

                
                if (player.playerFund >= 10000)
                {
                    level++;
                    player.playerExp += 200;
                    player.playerFund -= 1000;
                }
                else {
                    Debug.LogWarning("insufficient funds");
                    Time.timeScale = 0f;
                    encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Warning");
                    encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Insufficient Funds!");
                    encounterPanel.SetActive(true);
                }
                break;
            case 2:
                if (player.playerFund >= 30000)
                {
                    level++;
                    player.playerExp += 300;
                    player.playerFund -= 30000;
                }
                else
                {
                    Debug.LogWarning("insufficient funds");
                    Time.timeScale = 0f;
                    encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Warning");
                    encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Insufficient Funds!");
                    encounterPanel.SetActive(true);
                }
                break;
            case 3:
                if (player.playerFund >= 60000)
                {
                    level++;
                    player.playerExp += 500;
                    player.playerFund -= 60000;
                }
                else
                {
                    Debug.LogWarning("insufficient funds");
                    Time.timeScale = 0f;
                    encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Warning");
                    encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Insufficient Funds!");
                    encounterPanel.SetActive(true);
                }
                break;
        
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
