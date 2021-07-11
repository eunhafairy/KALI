using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class poachingAlert : MonoBehaviour
{

    [SerializeField] PlayerData player;
    [SerializeField] BarracksScript barracks;
    [SerializeField] int rate = 30, deployRate = 10, no_poachers;
    [SerializeField] GameObject gObj_poach_alert, deployWindow, poachWindow, poachWindowAvail, encounterPanel;
    [SerializeField] bool flag, flag2;
    [SerializeField] TextMeshProUGUI tmp_message;
    [SerializeField] Transform audioManager;

    void Start()
    {
        switch (player.playerLevel){

            case 1:
                rate =(int) (20 - (20 * 0.1f));
                break;
            case 2:
                rate = (int)(20 - (20 * 0.3f));
                break;
            case 3:
                rate = (int)(20 - (20 * 0.5f));
                break;
        }
      
        //disable alert windows at start
        gObj_poach_alert.SetActive(false);
        deployWindow.SetActive(false);
        poachWindow.SetActive(false);
        poachWindowAvail.SetActive(false);
        encounterPanel.SetActive(false);

        //time scale to 1, initialize bools
        Time.timeScale = 1f;
        flag2 = false;
        flag = false;
        
        //invoke poaching activities
        InvokeRepeating("poacherAlert", 10, rate);
    }
    private void Update()
    {
        switch (player.playerLevel)
        {

            case 1:
                rate = (int)(20 - (20 * 0.1f));
                break;
            case 2:
                rate = (int)(20 - (20 * 0.3f));
                break;
            case 3:
                rate = (int)(20 - (20 * 0.5f));
                break;
        }
    }

    void poacherAlert()
    {

        //only trigger if poacher warning is not active
        if (!gObj_poach_alert.activeSelf)
        {
            //50% chance to trigger
            int chance = Random.Range(1, 10);

            if (chance <= 5)
            {
                StartCoroutine(timer());


            }


        }

    }

    IEnumerator timer()
    {
        flag = false; 
        gObj_poach_alert.SetActive(true);
        audioManager.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(10);
        audioManager.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Stop();

        //after 10 seconds if not clicked, decrease tamaraw population and set game object to false
        gObj_poach_alert.SetActive(false);
        
        flag2 = false;
        if (!flag && !flag2) {

            //decrease tamaraw
            audioManager.GetChild(3).gameObject.GetComponent<AudioSource>().Play();

            int rand = Random.Range(1,5);
            player.tamarawNumber -= rand;
            Time.timeScale = 0f;
            encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Aww...");
            encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The poachers killed "+rand +" tamaraws.");
            encounterPanel.SetActive(true);



        }



    }

    public void isClickedConfirm() {
        
        audioManager.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Stop();

        int avail = 0;
        // Time.timeScale = 1f;
        //algorithm here
        for (int x= 0; x < barracks.noRangers; x++) {

            if (barracks.transform.GetChild(x).gameObject.GetComponent<RangerScript>().isReady) {
                avail++;
            
            }

        
        }
        if (avail > 0)
        {
            flag = true;
            PlayerPrefs.SetInt("avail", avail);
            PlayerPrefs.SetInt("poachers", no_poachers);
            poachWindow.SetActive(false);
            deployWindow.SetActive(true);
        }
        else {

            //warning here
            audioManager.transform.GetChild(3).gameObject.GetComponent<AudioSource>().Stop();

            poachWindowAvail.SetActive(true);
            poachWindow.SetActive(false);
            Debug.LogError("No available rangers!");
        
        }


    }

    public void isClickedCancel() {

        flag = false;
        Time.timeScale = 1f;
    }

    public void warningClicked() {
            audioManager.transform.GetChild(4).gameObject.GetComponent<AudioSource>().Stop();

        Time.timeScale = 0f;
            int rand = Random.Range(1,3);
            no_poachers = rand;
            tmp_message.SetText("There are "+ no_poachers + " poacher/s. Deploy rangers?");
            
    }

    public void globalCancel() {

        Time.timeScale = 1f;
    }


}
    

