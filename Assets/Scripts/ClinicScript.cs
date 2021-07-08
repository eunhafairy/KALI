using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ClinicScript : MonoBehaviour
{

    public GameObject clinicPanel, barracksPanel, ward, moveToward; //reference to the clinic panel
    public int level, noVet, noRooms, noAdmitted, maxAddRoom, maxVet, vetCost, clinicUpgradeCost, healRate; //attributes of clinic
    public int[] tamarawHealth; //health of tamaraws admitted
    public GameObject tamaraw, vet, room, roomCardPrefab; //reference to prefab
    public GameObject[] roomCard, tamaraws;
    [SerializeField] GameObject warningPanel, upgradePanel;
    [SerializeField] PlayerData player;
    public Transform roomPanel, buyPanel;
    public Button releaseButton, upgradebutton;
    public bool deathFlag;
   
    private void Update()
    {
    

        if (level <= player.playerLevel)
        {
            upgradebutton.interactable = true;

        }
        else
        {
            upgradebutton.interactable = false;
        }



        //assign max no of room and employee
        switch (level) {
            case 1:
               noRooms = 4;
                maxVet = 2;
                clinicUpgradeCost = 10000;
                break;
            case 2:
                noRooms = 8;
                maxVet = 4;
                clinicUpgradeCost = 15000;

                break;
            case 3:
                clinicUpgradeCost = 20000;
                noRooms = 12;
                maxVet = 6;
                break;
        }

        switch (noVet) {
            case 1:
                vetCost = 5000;
                break;
            case 2:
                vetCost = 8000;
                break;
            case 3:
                vetCost = 12000;
                break;
            case 4:
                vetCost = 16000;
                break;
            case 5:
                vetCost = 24000;
                break;
            case 6:
                vetCost = 32000;
                break;
        }

        noAdmitted = getNumberOfAdmitted(); //get the number of admitted tamaraws
        //set  text
        clinicPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Clinic (Level " + level + ")");
        clinicPanel.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(noVet.ToString());
        clinicPanel.transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>().SetText(noAdmitted +"/"+ noRooms.ToString());

        healRate = (int)((noVet * 0.1f) * 100);
        clinicPanel.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText("Healing rate: +" + healRate + "%");

        for (int x = 0; x < noAdmitted; x++)
        {

            tamarawHealth[x] = transform.GetChild(2).GetChild(x).gameObject.GetComponent<TamarawScript>().health;

        }
        
        
        healthSlider();



    }


    int getNumberOfAdmitted()
    {
        int admitted = transform.GetChild(2).transform.childCount;
        return admitted;
    }

    public void addVet() {

        //check if max number of vet is reached
        if (noVet < maxVet)
        {
            //add one vet
            if (vetCost <= player.playerFund) {

                player.playerFund -= vetCost;
                player.playerExp += 200;
                noVet++;

                //instantiate vetPrefab
                Instantiate(vet, transform.GetChild(0).transform);

                Time.timeScale = 0f;
                warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Hired!");
                warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Successfully hired an additional veterinarian!");
                warningPanel.SetActive(true);

            }

        
        else {
            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Warning");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Insufficient funds!");
            warningPanel.SetActive(true);
             }

     }
        else {
            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Warning");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Maximum number of Vets reached! Upgrade the clinic for extra slot.");
            warningPanel.SetActive(true);
        }
    }
   
    //for hover
    void OnMouseOver()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //If your mouse hovers over the GameObject
        if (gm.windowClear()) {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        

  



    }
    void OnMouseExit()
    {

        transform.localScale = new Vector3(0.4528f, 0.4744f, 1);


    }

    //for click
    private void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {

            GameObject gameManager = GameObject.Find("GameManager");
            if (gameManager.GetComponent<GameManager>().windowClear()) clinicPanel.SetActive(true);
        }
    }

    public void upgradeMessage()
    {

        upgradePanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level " + level);
        upgradePanel.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level " + (level + 1));
        upgradePanel.transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>().SetText("Php " + clinicUpgradeCost.ToString("N"));
        upgradePanel.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText("Maximum room capacity +4\nMaximum vet capacity +2");


    }
    public void buyMessage() {
        buyPanel.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Buy additional veterinarian for Php "+ vetCost.ToString("N")+"?");
    }

    public void takeInjured() {

        //check if full
        if (noAdmitted < noRooms)
        {
           

            GameObject newTamaraw = Instantiate(tamaraw, transform.GetChild(2));
            newTamaraw.GetComponent<TamarawScript>().health = Random.Range(1,50);
            tamaraws = new GameObject[12];
            for (int x = 0; x < getNumberOfAdmitted(); x++) {

                tamaraws[x] = transform.GetChild(2).GetChild(x).gameObject;
            }

            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success!");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Successfully admitted. Visit the clinic for the tamaraw's healing progress!");
            warningPanel.SetActive(true);
        }
        else {
            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Warning");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Room is full. Upgrade the Clinic to acquire more rooms!");
            warningPanel.SetActive(true);

        }

    
    }

    public void upgradeClinic() {

        if (player.playerFund >= clinicUpgradeCost && level <=3)
        {
            //upgrade clinic
            level++;
            switch (level) {
                case 2:
                    noRooms = 8;
                    break;
                case 3:
                    noRooms = 12;
                    break;
            }
            for (int x = 0; x< 4; x++) {
                Instantiate(roomCardPrefab, roomPanel.transform);
            }

            //re initialize roomcards
            roomCard = new GameObject[12];
            for (int x = 0; x < noRooms; x++)
            {
                
                roomCard[x] = roomPanel.transform.GetChild(x).gameObject;
                
            }
            Debug.LogWarning("No. Rooms: "+noRooms + ", No. of roomCardPrefab in panel: " + roomPanel.transform.childCount);
            Time.timeScale = 0f;
            player.playerFund -= clinicUpgradeCost;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success!");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Clinic is now level "+level);
            warningPanel.SetActive(true);




            



        }
        else {
            //not enough funds
            if (player.playerFund < clinicUpgradeCost)
            {
                Time.timeScale = 0f;
                warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Failed!");
                warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Not enough funds.");
                warningPanel.SetActive(true);
            }
            else {
                Time.timeScale = 0f;
                warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Failed!");
                warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Error occured.");
                warningPanel.SetActive(true);
            }
            

        }
    
    
    }

    public void healthSlider() {


            for (int x = 0; x < noRooms; x++) {


                if (x < noAdmitted)
                {

                    roomCard[x].gameObject.SetActive(true);

                    roomCard[x].transform.GetChild(2).gameObject.GetComponent<Slider>().value = transform.GetChild(2).GetChild(x).gameObject.GetComponent<TamarawScript>().health;

                    if (roomCard[x].transform.GetChild(2).gameObject.GetComponent<Slider>().value == 0)
                    {
                        continue;
                    }

                    if (roomCard[x].transform.GetChild(2).gameObject.GetComponent<Slider>().value >= roomCard[x].transform.GetChild(2).gameObject.GetComponent<Slider>().maxValue)
                    {

                        //heal or dead


                        roomCard[x].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("surgery complete");
                        roomCard[x].transform.GetChild(2).gameObject.SetActive(false);
                        roomCard[x].transform.GetChild(4).gameObject.SetActive(true);
                        roomCard[x].transform.GetChild(4).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("See result");




                    }
                    else
                    {
                        roomCard[x].transform.GetChild(2).gameObject.SetActive(true);
                        roomCard[x].transform.GetChild(4).gameObject.SetActive(false);
                        roomCard[x].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Healing!");

                    }
                }

            else
            {

                roomCard[x].gameObject.SetActive(false);
            }
            
            }

    
        
         

    }

    public void releaseTamaraw(GameObject _button, int index) {

        GameObject _roomCard = _button.transform.parent.gameObject;
       
        int rand = Random.Range(1,100);
        int chance = (int) ((noVet * 0.1f) * 100);
        if (rand <= chance)
        {
            Time.timeScale = 0f;
            moveToward.SetActive(true);
            _roomCard.transform.GetChild(4).gameObject.SetActive(false);
            _roomCard.transform.GetChild(2).gameObject.GetComponent<Slider>().value = 0;
            _roomCard.transform.GetChild(2).gameObject.SetActive(false);
            _roomCard.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Room Available");
            Destroy(transform.GetChild(2).GetChild(index).gameObject);

        }

        
        else {

            player.tamarawNumber -= 1;
            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Oh no!");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The tamaraw died in your care.");
            warningPanel.SetActive(true);
            
            _roomCard.transform.GetChild(4).gameObject.SetActive(false);
            _roomCard.transform.GetChild(2).gameObject.GetComponent<Slider>().value = 0;
            _roomCard.transform.GetChild(2).gameObject.SetActive(false);
            _roomCard.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Room Available");
            Destroy(transform.GetChild(2).GetChild(index).gameObject);


        }
        Debug.Log("Tamaraw release!! Rolled "+ rand + ". Roll should be less than or equal "+chance);
    }



    public void releaseAgad() {
       
        int rand = Random.Range(1, 2);

        switch (rand)
        {
            case 1:
                Time.timeScale = 0f;
                warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Yehey!");
                warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The tamaraw is released and it managed to survived.");
                warningPanel.SetActive(true);
                break;
            case 2:
                player.tamarawNumber -= 1;
                Time.timeScale = 0f;
                warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Oh no!");
                warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The tamaraw is released but it did not survived.");
                warningPanel.SetActive(true);
                break;
        }

    }

    public void moveToWardBldg() {

        if (ward.GetComponent<WardScript>().tamarawRecovering < ward.GetComponent<WardScript>().maxCapacity)
        {
            GameObject newTam = Instantiate(tamaraw, ward.transform.GetChild(1));
            newTam.GetComponent<TamarawScript>().recovery = Random.Range(50, 100);
            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Moved");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The tamaraw is moved to the recovery ward.");
            warningPanel.SetActive(true);
        }
        else {

            int rand = Random.Range(1, 2);

            switch (rand) {
                case 1:
                    Time.timeScale = 0f;
                    warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Full!");
                    warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The ward is full. The tamaraw is released and it managed to survived.");
                    warningPanel.SetActive(true);
                    break;
                case 2:
                    player.tamarawNumber -= 1;
                    Time.timeScale = 0f;
                    warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Full!");
                    warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The ward is full. The tamaraw is released but it did not survived.");
                    warningPanel.SetActive(true);
                    break;
            }
            
        }


    }
}