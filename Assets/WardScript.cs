using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WardScript : MonoBehaviour
{
    public int maxWarden, upgradeCost, maxCapacity;
    public int noWardens;
    public int level;
    public int[] tamarawRecovery;
    public int tamarawRecovering;
    public GameObject warden, tamaraw, recoveryCard; //prefabs
    public GameObject wardPanel, scrollPanel, warningPanel;
    GameManager gm;
    public GameObject[] recoveryCardArr;
    public PlayerData player;
    // Start is called before the first frame update
    void Start()
    {
        wardPanel.SetActive(false);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
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

        wardPanel.transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>().SetText(tamarawRecovering + "/"+maxCapacity);

        recoverySlider();
        for (int x = 0; x < tamarawRecovering; x++) {
            tamarawRecovery[x] = transform.GetChild(1).GetChild(x).gameObject.GetComponent<TamarawScript>().recovery;
        }

    }

    private void OnMouseDown()
    {

        if (gm.windowClear()) wardPanel.SetActive(true);

    }

    private void OnMouseOver()
    {

        if (gm.windowClear()) transform.localScale = new Vector3(0.5f, 0.5f, 1);


    }
    private void OnMouseExit()
    {

        if (gm.windowClear()) transform.localScale = new Vector3(0.4528f, 0.4744f, 1);
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
                scrollPanel.transform.GetChild(x).GetChild(2).gameObject.SetActive(true);
                scrollPanel.transform.GetChild(x).GetChild(4).gameObject.SetActive(false);
                scrollPanel.transform.GetChild(x).GetChild(2).GetComponent<Slider>().value = transform.GetChild(1).GetChild(x).gameObject.GetComponent<TamarawScript>().recovery;


                if (scrollPanel.transform.GetChild(x).GetChild(2).gameObject.GetComponent<Slider>().value >= 100) {

                    scrollPanel.transform.GetChild(x).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Fully recovered!");
                    scrollPanel.transform.GetChild(x).GetChild(2).gameObject.SetActive(false);
                    scrollPanel.transform.GetChild(x).GetChild(4).gameObject.SetActive(true);


                }

            }
            else {

                scrollPanel.transform.GetChild(x).gameObject.SetActive(false);
            }


        }

    }

    public void freeTamaraw(GameObject _button, int index) {

        GameObject _recoveryCard = _button.transform.parent.gameObject;

        _recoveryCard.transform.GetChild(4).gameObject.SetActive(false);
        _recoveryCard.transform.GetChild(2).gameObject.GetComponent<Slider>().value = 0;
        _recoveryCard.transform.GetChild(2).gameObject.SetActive(true);
        _recoveryCard.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Recovering");
        Destroy(transform.GetChild(1).GetChild(index).gameObject);
        Time.timeScale = 0f;
        warningPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Yehey!");
        warningPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The tamaraw has been freed.");
        warningPanel.SetActive(true);
    }

    public void upgradeWard()
    {

        if (player.playerFund >= upgradeCost)
        {


        }
        else { 
        //not enough funds
        
        }
    }

    public void upgradeWardMessage() { 
    
    
    }

}

