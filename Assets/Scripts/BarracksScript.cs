using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
public class BarracksScript : MonoBehaviour
{
    [SerializeField] GameObject rangerField, rangerFieldPrefab;
    [SerializeField] GameObject progressPrefab, encounterPanel, audioManager;
    [SerializeField] Button upgradebutton;
    [SerializeField] Transform progressPanel;
    [SerializeField] SpriteRenderer spriteRenderer;
    public Sprite lvl1, lvl2, lvl3;
    public int level, noRangers, no_of_avail, maxRangers;
    public int[] rangerEnergy;
    public int rangerCost, availableRanger;
   
    public TextMeshProUGUI tmp_brk_lvl, tmp_no_of_ranger;
    public GameObject rangers, barracksUpgrade; //prefab reference
    public GameObject[] ranger, progressCard; //objects
    public PlayerData player;

    private void Start()
    {
    
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
       

        //set upgrade button interactible if minimum lvl requirement reached.

        if (level < 3 && level <= player.playerLevel)
        {
            upgradebutton.interactable = true;

        }
       
        else
        {
            upgradebutton.interactable = false;
        }

        //set text mesh pro value
        tmp_brk_lvl.SetText("Barracks (Level "+level+")");
        tmp_no_of_ranger.SetText(getAvailable() + "/" + noRangers);


        //set max number of ranger available
        switch (level) {

            case 1:
                maxRangers = 2;
                spriteRenderer.sprite = lvl1;
                break;
            case 2:
                maxRangers = 4;
                spriteRenderer.sprite = lvl2;

                break;
            case 3:
                maxRangers = 6;
                spriteRenderer.sprite = lvl3;

                break;


        }

        //set ranger cost based on no of rangers
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
                player.playerExp += 400;

                //instantiate ranger prefab as child
                GameObject newRang = Instantiate(rangers, transform);
                newRang.GetComponent<RangerScript>().energy = 100;
                Instantiate(progressPrefab, progressPanel);
                progressCard = new GameObject[6];
                for (int x = 0; x < noRangers; x++)
                {

                    progressCard[x] = progressPanel.GetChild(x).gameObject;

                }

                //add ranger on the field
                Instantiate(rangerFieldPrefab, rangerField.transform);

                audioManager.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
                Time.timeScale = 0f;
                encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
                encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("+1 Ranger!");
                encounterPanel.SetActive(true);

            }
            else {
                //create warning here
                audioManager.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();
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
            audioManager.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

            Time.timeScale = 0f;
            encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Warning");
            encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Max capacity of Barracks reached! Level up the barracks for increased max capacity.");
            encounterPanel.SetActive(true);
        }
    
    
    }

    public void upgradeMessage() {
        barracksUpgrade.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level "+level);
        barracksUpgrade.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level " + (level+1));
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
        barracksUpgrade.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText("Php "+ barrackUpgradeCost.ToString("N"));

    }

    public void upgradeBarracks() {

        switch (level) {

            case 1:

                
                if (player.playerFund >= 10000)
                {

                    level++;
                    player.playerExp += 200;
                    player.playerFund -= 10000;

                    GetComponent<SpriteRenderer>().sprite = lvl2;
                    audioManager.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
                    Time.timeScale = 0f;
                    encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
                    encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Barracks leveled up successfully.");
                    encounterPanel.SetActive(true);
                }
                else {
                    audioManager.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

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
                    GetComponent<SpriteRenderer>().sprite = lvl3;

                    level++;
                    player.playerExp += 300;
                    player.playerFund -= 30000;
                    audioManager.transform.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
                    Time.timeScale = 0f;
                    encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
                    encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Barracks leveled up successfully.");
                    encounterPanel.SetActive(true);
                }
                else
                {
                    audioManager.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

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
        no_of_avail = ctr;
        return ctr;


    }

    private void progressBar() {

        for (int x = 0; x < noRangers; x++)
        {

            progressCard[x].transform.GetChild(2).gameObject.GetComponent<Slider>().value = transform.GetChild(x).gameObject.GetComponent<RangerScript>().energy; 

        }

    }
}
