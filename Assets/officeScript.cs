using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class officeScript : MonoBehaviour
{
    AudioSource hover;
    [SerializeField] Transform officeScrollPanel, audioManager;
    [SerializeField] GameObject officePanel, warningPanel, upgradePanel;
    [SerializeField] GameManager gm;
    [SerializeField] PlayerData player;
    [SerializeField] Button upgradeBtn;
    [SerializeField] Sprite lockSprite, socMedSprite, webSprite;
    public float fundAmountRate;
    public float fundRate;
    public bool webinar;
    public bool tarp;
    public bool socialMedia;
    public int level;
    int tarpCost, webinarCost, socialMediaCost, upgradeCost;
    Vector3 scale;
    // Update is called once per frame

    private void Start()
    {
        hover = GameObject.Find("AudioManager").transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        scale = transform.localScale;
        tarpCost = 5000;
        webinarCost = 10000;
        socialMediaCost = 20000;
        player = GameObject.Find("Player").GetComponent<PlayerData>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        //upgrade button interactable
        if (level < 3 && level <= player.playerLevel)
        {
            upgradeBtn.interactable = true;

        }
        
        else
        {
            upgradeBtn.interactable = false;
        }

        //invest buttons set if interactable based on level
        switch (level) {

            case 1:
                upgradeCost = 3000;
                officeScrollPanel.GetChild(1).GetChild(4).GetComponent<Button>().interactable = false;
                officeScrollPanel.GetChild(2).GetChild(4).GetComponent<Button>().interactable = false;
                
          

                officeScrollPanel.GetChild(1).GetChild(3).GetComponent<Image>().sprite = lockSprite;
                officeScrollPanel.GetChild(1).GetChild(4).gameObject.SetActive(false);
                officeScrollPanel.GetChild(1).GetChild(5).gameObject.SetActive(true);

                officeScrollPanel.GetChild(2).GetChild(3).GetComponent<Image>().sprite = lockSprite;
                officeScrollPanel.GetChild(2).GetChild(4).gameObject.SetActive(false);
                officeScrollPanel.GetChild(2).GetChild(5).gameObject.SetActive(true);



                break;
            case 2:
                upgradeCost = 6000;
                if(!webinar) officeScrollPanel.GetChild(1).GetChild(4).GetComponent<Button>().interactable = true;
                if(!socialMedia) officeScrollPanel.GetChild(2).GetChild(4).GetComponent<Button>().interactable = false;

                officeScrollPanel.GetChild(1).GetChild(4).gameObject.SetActive(true);
                officeScrollPanel.GetChild(1).GetChild(5).gameObject.SetActive(false);
                officeScrollPanel.GetChild(1).GetChild(3).GetComponent<Image>().sprite = webSprite;
                officeScrollPanel.GetChild(2).GetChild(3).GetComponent<Image>().sprite = lockSprite;
                officeScrollPanel.GetChild(2).GetChild(4).gameObject.SetActive(false);
                officeScrollPanel.GetChild(2).GetChild(5).gameObject.SetActive(true);
                break;
            case 3:
                upgradeCost = 8000;
                if (!webinar) officeScrollPanel.GetChild(1).GetChild(4).GetComponent<Button>().interactable = true;
                if (!socialMedia) officeScrollPanel.GetChild(2).GetChild(4).GetComponent<Button>().interactable = true;


                officeScrollPanel.GetChild(1).GetChild(3).GetComponent<Image>().sprite = webSprite;
                officeScrollPanel.GetChild(2).GetChild(3).GetComponent<Image>().sprite = socMedSprite;
                officeScrollPanel.GetChild(2).GetChild(4).gameObject.SetActive(true);
                officeScrollPanel.GetChild(2).GetChild(5).gameObject.SetActive(false);
                break;

        }

        //investments if already bought
        if (tarp) {
            officeScrollPanel.GetChild(0).GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Invested");
            officeScrollPanel.GetChild(0).GetChild(4).GetComponent<Button>().interactable = false;
          


        }
        if (webinar)
        {
            officeScrollPanel.GetChild(1).GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Invested");
            officeScrollPanel.GetChild(1).GetChild(4).GetComponent<Button>().interactable = false;
            officeScrollPanel.GetChild(1).GetChild(5).gameObject.SetActive(false);

            officeScrollPanel.GetChild(1).GetChild(4).gameObject.SetActive(true);
            officeScrollPanel.GetChild(1).GetChild(5).gameObject.SetActive(false);


        }
        if (socialMedia)
        {
            officeScrollPanel.GetChild(2).GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Invested");
            officeScrollPanel.GetChild(2).GetChild(4).GetComponent<Button>().interactable = false;
            officeScrollPanel.GetChild(2).GetChild(5).gameObject.SetActive(false);

            officeScrollPanel.GetChild(2).GetChild(4).gameObject.SetActive(true);
            officeScrollPanel.GetChild(2).GetChild(5).gameObject.SetActive(false);
        }

        //set texts
        officePanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Office (Level "+level+")");
        officePanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Fund Generation +"+((fundAmountRate + fundRate)*100) +"%");

        
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Clicked on the UI");
        }
        else {
            if (gm.windowClear()) officePanel.SetActive(true);
        }
        

    }

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        else if (gm.windowClear()) { 
            transform.localScale = new Vector3(scale.x + 0.05f, scale.y + 0.05f, 1);
            if (hover.isPlaying) hover.Stop();
            hover.Play();

        }


    }
    private void OnMouseExit()
    {

        if (gm.windowClear()) transform.localScale = scale;
    }

    public void investTarp() {


        if (player.playerFund >= tarpCost)
        {
            audioManager.GetChild(2).gameObject.GetComponent<AudioSource>().Play();

            player.playerExp += 100;
            player.playerFund -= tarpCost;
            tarp = true;
            fundRate += 0.1f;
            fundAmountRate += 0.3f;
            int rate = (int) (20 - (20 * fundRate));
            Debug.Log("Upgrade tarp success. rate now: "+rate);
            
            gm.gameObject.GetComponent<fundGeneration>().CancelInvoke("generateFund");
            gm.gameObject.GetComponent<fundGeneration>().InvokeRepeating("generateFund", 10, rate);
            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Successfully Invested in tarpaulin.");
            warningPanel.SetActive(true);

        }
        else {
            //not enough funds
            audioManager.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Failed");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Not enough funds");
            warningPanel.SetActive(true);
        }
        
    
    }

    public void investWebinar()
    {


        if (player.playerFund >= webinarCost)
        {
            audioManager.GetChild(2).gameObject.GetComponent<AudioSource>().Play();

            player.playerExp += 100;

            player.playerFund -= webinarCost;
            webinar = true;
            fundRate += 0.1f;
            fundAmountRate += 0.5f;
            int rate = (int)(20 - (20 * fundRate));
            Debug.Log("Upgrade webinar success. rate now: " + rate);
            gm.gameObject.GetComponent<fundGeneration>().CancelInvoke("generateFund");
            gm.gameObject.GetComponent<fundGeneration>().InvokeRepeating("generateFund", 10, rate);
            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Successfully Invested in webinar.");
            warningPanel.SetActive(true);

        }
        else
        {
            //not enough funds
            audioManager.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Failed");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Not enough funds");
            warningPanel.SetActive(true);
        }


    }

    public void investSocMed()
    {


        if (player.playerFund >= socialMediaCost)
        {
            player.playerExp += 100;
            audioManager.GetChild(2).gameObject.GetComponent<AudioSource>().Play();

            player.playerFund -= socialMediaCost;
            socialMedia = true;
            fundRate += 0.1f;
            fundAmountRate += 0.9f;
            int rate = (int)(20 - (20 * fundRate));
            Debug.Log("Upgrade socmed success. rate now: " + rate);
            gm.gameObject.GetComponent<fundGeneration>().CancelInvoke("generateFund");
            gm.gameObject.GetComponent<fundGeneration>().InvokeRepeating("generateFund", 10, rate);
            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Successfully Invested in Social Media ads.");
            warningPanel.SetActive(true);

        }
        else
        {
            //not enough funds
            audioManager.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Failed");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Not enough funds");
            warningPanel.SetActive(true);
        }


    }

    public void upgradeMessage() {

        upgradePanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level " + level);
        upgradePanel.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText("Level " + (level + 1));
        upgradePanel.transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>().SetText("Php " + upgradeCost.ToString("N"));
      
    }

    public void upgradeOffice()
    {

        if (player.playerFund >= upgradeCost)
        {
            audioManager.GetChild(2).gameObject.GetComponent<AudioSource>().Play();

            level++;
            //minus funds
            player.playerFund -= upgradeCost;
            player.playerExp += 100;
            Time.timeScale = 0f;
            warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
            warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Office leveled up from Level " + (level - 1) + " to Level " + level);
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
}
