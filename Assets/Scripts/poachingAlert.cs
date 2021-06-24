using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class poachingAlert : MonoBehaviour
{

    [SerializeField] PlayerData player;
    [SerializeField] int rate = 10, deployRate = 10, no_poachers;
    [SerializeField] GameObject gObj_poach_alert;
    [SerializeField] bool flag, flag2;
    [SerializeField] TextMeshProUGUI tmp_message;

    void Start()
    {
        Time.timeScale = 1f;
        flag2 = false;
        flag = false;
        gObj_poach_alert.SetActive(false);
        InvokeRepeating("poacherAlert", 10, rate);
    }

    void poacherAlert()
    {

        //only trigger if poacher warning is not active
        if (!gObj_poach_alert.activeSelf)
        {
            //50% chance to trigger
            int chance = Random.Range(1, 10);
            Debug.Log(chance);
            if (chance < 5)
            {
                StartCoroutine(timer());


            }


        }

    }

    IEnumerator timer()
    {
        flag = false;
        
        gObj_poach_alert.SetActive(true);
        yield return new WaitForSeconds(5);
        //after 5 seconds if not clicked, decrease tamaraw population and set game object to false
        gObj_poach_alert.SetActive(false);
        flag2 = false;

        if (!flag && !flag2) { 

            //decrease tamaraw
            int rand = Random.Range(1,5);
            player.tamarawNumber -= rand;
            
        }



    }

    public void isClickedConfirm() {
        
        flag = true;
        Time.timeScale = 1f;
        //algorithm here 



    }

    public void isClickedCancel() {

        flag = false;
        Time.timeScale = 1f;
    }

    public void warningClicked() {
        if (PlayerPrefs.GetInt("no_of_avail") > 0)
        {
            Time.timeScale = 0f;
            int rand = Random.Range(1,3);
            no_poachers = rand;
            tmp_message.SetText("There are "+ no_poachers + " poacher/s. Deploy rangers?");


        }
        else {

            Debug.Log("Rangers not available.");
        }
        
    
    }

    public void globalCancel() {

        Time.timeScale = 1f;
    }

    public IEnumerator deployRanger(int no_of_rangers, int no_of_poachers) {

        //disable no_of_rangers availability
        PlayerPrefs.SetInt("no_of_avail", PlayerPrefs.GetInt("no_of_avail") - no_of_rangers);
        //wait for n seconds
        yield return new WaitForSeconds(deployRate);

        //logic for rangers vs poachers
        PlayerPrefs.SetInt("no_of_avail", PlayerPrefs.GetInt("no_of_avail") + no_of_rangers);

        //per ranger, success rate is 11%
    
        int chance = (int) Random.Range(1, ((no_of_rangers * 0.11f)*100));

        int rand = (int) Random.Range(1,(100 + (no_of_poachers * 0.15f)*100));
        if (rand <= chance) {
            //success
            Debug.Log("Success");

        }
        else {
            //fail
            Debug.Log("Fail");

            //kill tamaraws
            int toKill = Random.Range(1 * (no_of_poachers), 3 * no_of_poachers);
            PlayerPrefs.SetInt("tmrw_number", PlayerPrefs.GetInt("tmrw_number") - toKill);
        }

    }

}
    

