using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class researchScript : MonoBehaviour
{
    [SerializeField] PlayerData player;
    [SerializeField] Button upgradeBtn;
    [SerializeField] Transform scrollPanel, researchMessagePanel;
    [SerializeField] Transform audioManager;
    [SerializeField] GameObject researchPanel, upgradePanel, warningPanel;
    [SerializeField] AudioSource hover;
    
    int upgradeCost;
    GameManager gm;
    Vector3 scale;
    public int level;
    public bool native, artificial, scout;

    private void Start()
    {
        scale = transform.localScale;
        audioManager = GameObject.Find("AudioManager").transform;
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


        if (artificial)
        {

            scrollPanel.GetChild(0).GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Completed");
            scrollPanel.GetChild(0).GetChild(4).gameObject.GetComponent<Button>().interactable = false;

        }

        if (native) { 
            scrollPanel.GetChild(2).GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Completed");
            scrollPanel.GetChild(2).GetChild(4).gameObject.GetComponent<Button>().interactable = false;
            scrollPanel.GetChild(2).GetChild(5).gameObject.SetActive(false);


        }
        if (scout) { 
            scrollPanel.GetChild(1).GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Completed");
            scrollPanel.GetChild(1).GetChild(4).gameObject.GetComponent<Button>().interactable = false;
            scrollPanel.GetChild(1).GetChild(5).gameObject.SetActive(false);



        }

        switch (level){
            case 1:
                upgradeCost = 12000;
                if (!scout){ 
                    scrollPanel.GetChild(1).GetChild(4).gameObject.SetActive(false);
                    scrollPanel.GetChild(1).GetChild(5).gameObject.SetActive(true);

                }
                if (!native)
                {
                    scrollPanel.GetChild(2).GetChild(4).gameObject.SetActive(false);
                    scrollPanel.GetChild(2).GetChild(5).gameObject.SetActive(true);

                }
                break;
            case 2:
                upgradeCost = 20000;

                if (!scout)
                {
                    scrollPanel.GetChild(1).GetChild(4).gameObject.SetActive(true);
                    scrollPanel.GetChild(1).GetChild(5).gameObject.SetActive(false);

                }
                if (!native)
                {
                    scrollPanel.GetChild(2).GetChild(4).gameObject.SetActive(false);
                    scrollPanel.GetChild(2).GetChild(5).gameObject.SetActive(true);

                }
                break;
            case 3:
                upgradeCost = 25000;

                if (!scout)
                {
                    scrollPanel.GetChild(1).GetChild(4).gameObject.SetActive(true);
                    scrollPanel.GetChild(1).GetChild(5).gameObject.SetActive(false);

                }
                if (!native)
                {
                    scrollPanel.GetChild(2).GetChild(4).gameObject.SetActive(true);
                    scrollPanel.GetChild(2).GetChild(5).gameObject.SetActive(false);

                }
                break;


        }
       researchPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Research Facility (Level " + level + ")");

    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Clicked on the UI");
        }
        else
        {
            if (gm.windowClear()) researchPanel.SetActive(true);
        }


    }

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        else if (gm.windowClear())
        {
            transform.localScale = new Vector3(scale.x + 0.1f, scale.y + 0.1f, 1);
            if (hover.isPlaying) hover.Stop();
            hover.Play();

        }


    }
    private void OnMouseExit()
    {

        if (gm.windowClear()) transform.localScale = scale;
    }

    public void upgradeMessage()
    {

        upgradePanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level " + level);
        upgradePanel.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level " + (level + 1));
        upgradePanel.transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>().SetText("Php " + upgradeCost.ToString("N"));

    }

    public void upgradResearch()
    {

        if (player.playerFund >= upgradeCost)
        {
            audioManager.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
            player.playerExp += 200;

            level++;
            //minus funds
            player.playerFund -= upgradeCost;
            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Research facility leveled up from Level " + (level - 1) + " to Level " + level);
            warningPanel.SetActive(true);

        }
        else
        {
            audioManager.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

            //not enough funds
            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Failed");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Not enough funds.");
            warningPanel.SetActive(true);
        }
    }

    public void researchMessage1() {
        researchMessagePanel.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Research artificial reproduction for Php 6,000.00?");
        researchMessagePanel.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText("+15% Tamaraw reproduction rate.");

        PlayerPrefs.SetInt("rs_index", 1);
    }
    public void researchMessage2()
    {
        researchMessagePanel.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Research new scouting technique for Php 9,000.00?");
        researchMessagePanel.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText("+15% Injured Tamaraw Acquisition");

        PlayerPrefs.SetInt("rs_index", 2);

    }
    public void researchMessage3()
    { 
        researchMessagePanel.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Research native language for Php 12,000.00?");
        researchMessagePanel.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText("+15% Ranger success rate.");

        PlayerPrefs.SetInt("rs_index", 3);

    }

    public void researchItem()
    {
        switch (PlayerPrefs.GetInt("rs_index")) {

            case 1:
                if (player.playerFund >= 6000)
                {
                    audioManager.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
                    player.playerFund -= 6000;

                    player.playerExp += 100;

                    artificial = true;
                    tamarawGeneration gm = GameObject.Find("GameManager").GetComponent<tamarawGeneration>();
                    gm.CancelInvoke();
                    gm.InvokeRepeating("generateTamaraw", 0, (40 - (40*0.15f)));
                    Time.timeScale = 0f;
                    warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
                    warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Artifical Reproduction researched.");
                    warningPanel.SetActive(true);
                }
                else
                {

                    audioManager.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

                    Time.timeScale = 0f;
                    warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Failed");
                    warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Not enough funds.");
                    warningPanel.SetActive(true);
                }
                break;
            case 2:
                if (player.playerFund >= 9000)
                {
                    audioManager.GetChild(2).gameObject.GetComponent<AudioSource>().Play();
                    player.playerFund -= 9000;
                    player.playerExp += 100;

                    scout = true;

                    GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
                    gm.CancelInvoke();
                    float injuredRate = (30 - (30 * 0.15f));
                    int rand = Random.Range(10, 20);
                    Debug.LogWarning(injuredRate);
                    gm.InvokeRepeating("findInjured", rand, injuredRate);
                    Time.timeScale = 0f;
                    warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
                    warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Modern scouting technique researched.");
                    warningPanel.SetActive(true);
                }
                else
                {
                    audioManager.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

                    Time.timeScale = 0f;
                    warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Failed");
                    warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Not enough funds.");
                    warningPanel.SetActive(true);
                }
                break;
            case 3:
                if (player.playerFund >= 12000)
                {
                    player.playerExp += 100;
                    player.playerFund -= 12000;

                    native = true;
                    Time.timeScale = 0f;
                    warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
                    warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Native language researched.");
                    warningPanel.SetActive(true);
                }
                else
                {
                    audioManager.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

                    Time.timeScale = 0f;
                    warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Failed");
                    warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Not enough funds.");
                    warningPanel.SetActive(true);
                }
                break;
        
        }
             
    }

    
}
