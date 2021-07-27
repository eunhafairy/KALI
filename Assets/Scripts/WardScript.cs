using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class WardScript : MonoBehaviour
{
    AudioSource hover;
    public int maxWarden, upgradeCost, maxCapacity, wardenCost;
    public int noWardens;
    public int level, healRate;
    public int[] tamarawRecovery;
    public int tamarawRecovering;
    public GameObject warden, tamaraw, recoveryCard; //prefabs
    public GameObject wardPanel, scrollPanel, warningPanel, upgradePanel, buyPanel;
    public Transform audioManager;
    GameManager gm;
    public GameObject[] recoveryCardArr;
    public PlayerData player;
    [SerializeField] Button upgradeBtn;
    [SerializeField] GameObject wardenField, wardenFieldPrefab, tamPanel;
    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
        hover = GameObject.Find("AudioManager").transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        wardPanel.SetActive(false);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

        if (level < 3 && level <= player.playerLevel)
        {
            upgradeBtn.interactable = true;

        }
       
        else
        {
            upgradeBtn.interactable = false;
        }

        //cost of warden
        switch (noWardens) {

            case 1:
                wardenCost = 5000;
                break;
            case 2:
                wardenCost = 8000;
                break;
            case 3:
                wardenCost = 12000;
                break;
            case 4:
                wardenCost = 16000;
                break;
            case 5:
                wardenCost = 24000;
                break;
            case 6:
                wardenCost = 32000;
                break;
            
        }

        //get number of tamaraw based on the children
        tamarawRecovering = getTamarawNumber();

        switch (level) {

            case 1:
                maxWarden = 2;
                maxCapacity = 4;
                upgradeCost = 4000;
                break;
            case 2:
                maxWarden = 4;
                maxCapacity = 8;
                upgradeCost = 10000;

                break;
            case 3:
                maxWarden = 6;
                maxCapacity = 12;
                upgradeCost = 20000;
                break;


        }
        healRate = (int)((noWardens * 0.1f) * 100);
        wardPanel.transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>().SetText((maxCapacity - tamarawRecovering) + "/"+maxCapacity);
        wardPanel.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText(noWardens.ToString());
        wardPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Ward (Level " + level + ")");
        wardPanel.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText("Recovery rate: +" + healRate + "%");

        recoverySlider();
        for (int x = 0; x < tamarawRecovering; x++) {
            tamarawRecovery[x] = transform.GetChild(1).GetChild(x).gameObject.GetComponent<TamarawScript>().recovery;
        }

    }

    private void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Clicked on UI");
        }
        else {
           
            if (gm.windowClear()) wardPanel.SetActive(true);
        }
        

    }

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        else if (gm.windowClear()) {
            
            transform.localScale = new Vector3(scale.x +0.3f, scale.y + 0.3f, 1);
            if (hover.isPlaying) hover.Stop();
            hover.Play();

        } 


    }
    private void OnMouseExit()
    {

        if (gm.windowClear()) transform.localScale = scale;
    }

    private int getTamarawNumber(){


        int ctr = transform.GetChild(1).childCount;
        return ctr;
    }

    void recoverySlider() {

        for (int x = 0; x < maxCapacity; x++) {

            if (x < tamarawRecovering)
            {
                scrollPanel.transform.GetChild(x).gameObject.SetActive(true);
                scrollPanel.transform.GetChild(x).GetChild(2).GetComponent<Slider>().value = transform.GetChild(1).GetChild(x).gameObject.GetComponent<TamarawScript>().recovery;


                if (scrollPanel.transform.GetChild(x).GetChild(2).gameObject.GetComponent<Slider>().value >= 100)
                {

                    scrollPanel.transform.GetChild(x).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Fully recovered!");
                    scrollPanel.transform.GetChild(x).GetChild(2).gameObject.SetActive(false);
                    scrollPanel.transform.GetChild(x).GetChild(4).gameObject.SetActive(true);


                }
                else {
                    scrollPanel.transform.GetChild(x).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Recovering");
                    scrollPanel.transform.GetChild(x).GetChild(2).gameObject.SetActive(true);
                    scrollPanel.transform.GetChild(x).GetChild(4).gameObject.SetActive(false);
                 


                }

            }
            else {

                scrollPanel.transform.GetChild(x).gameObject.SetActive(false);
            }


        }

    }

    public void freeTamaraw(GameObject _button, int index) {

        GameObject _recoveryCard = _button.transform.parent.gameObject;
        player.playerExp += 150;
        audioManager.GetChild(2).gameObject.GetComponent<AudioSource>().Play();

        _recoveryCard.transform.GetChild(4).gameObject.SetActive(false);
        _recoveryCard.transform.GetChild(2).gameObject.GetComponent<Slider>().value = 0;
        _recoveryCard.transform.GetChild(2).gameObject.SetActive(true);
        _recoveryCard.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Recovering");
        Destroy(transform.GetChild(1).GetChild(index).gameObject);
        player.tamarawNumber++;
        Time.timeScale = 0f;
        warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Yehey!");
        warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The tamaraw has been freed.");
        warningPanel.SetActive(true);
        Destroy(tamPanel.transform.GetChild(0).gameObject);
    }

    public void upgradeWard()
    {
        
        if (player.playerFund >= upgradeCost)
        {
            player.playerExp += 150;
            audioManager.GetChild(2).gameObject.GetComponent<AudioSource>().Play();

            level++;
            switch (level) {
                case 2:
                    maxCapacity = 8;
                    break;
                case 3:
                    maxCapacity = 12;
                    break;
            }

            for (int x = 0; x < 4; x++) {
                Instantiate(recoveryCard, scrollPanel.transform);
            }

            //minus funds
            player.playerFund -= upgradeCost;
            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Ward leveled up from Level " + (level - 1) + " to Level " + level) ;
            warningPanel.SetActive(true);

        }
        else {
            //not enough funds
            audioManager.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Failed");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Not enough funds.");
            warningPanel.SetActive(true);
        }
    }

    public void upgradeWardMessage() {

        upgradePanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level " + level);
        upgradePanel.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level " + (level + 1));
        upgradePanel.transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>().SetText("Php " + upgradeCost.ToString("N"));
        upgradePanel.transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>().SetText("Maximum room capacity +4\nMaximum vet capacity +2");

    }

    public void addWardenMessage() {

        buyPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Buy additional warden for Php " + wardenCost.ToString("N") + "?");
    }

    public void addWarden() {

        if (noWardens < maxWarden)
        {
            if (player.playerFund >= wardenCost)
            {
                player.playerExp += 150;
                audioManager.GetChild(2).gameObject.GetComponent<AudioSource>().Play();

                noWardens++;
                Instantiate(warden, transform.GetChild(0));
                Instantiate(wardenFieldPrefab, wardenField.transform);
                Time.timeScale = 0f;
                warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
                warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("+1 Warden.");
                warningPanel.SetActive(true);
            }
            else {
                //not enough funds
                audioManager.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

                Time.timeScale = 0f;
                warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Failed");
                warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Not enough funds.");
                warningPanel.SetActive(true);

            }
        }
        else {
            //max capacity reach
            audioManager.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Failed");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Max capacity reached. Please upgrade the Ward.");
            warningPanel.SetActive(true);

        }

    
    }

}

