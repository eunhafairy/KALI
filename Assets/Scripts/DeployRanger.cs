using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeployRanger : MonoBehaviour
{
    [SerializeField] BarracksScript barracks;
    [SerializeField] PlayerData player;
    [SerializeField] TextMeshProUGUI val, successRate;
    [SerializeField] Slider slider;
    [SerializeField] GameObject notif, encounterPanel;
    int max;
    bool flag;

    private void Update()
    {
        max = (int)(100 + ((PlayerPrefs.GetInt("poachers") * 0.15f) * 100));
        int success = (int)((((slider.value * 0.11f) * 100) / max) * 100);
        slider.minValue = 1;
        slider.maxValue = PlayerPrefs.GetInt("avail");
        val.SetText(slider.value.ToString());
        successRate.SetText(success + "% Success Rate (The more you deploy, the chances of success will increase)");

    }


    public void confrontPoacher() {

        int no_ranger = (int)slider.value;
        int no_poacher = PlayerPrefs.GetInt("poachers");


        //per ranger, success rate is 11%
        int chance = (int)((no_ranger * 0.11f) * 100);



        int rand = (int)Random.Range(1, (100 + ((no_poacher * 0.15f) * 100)));
        if (rand <= chance)
        {

            //success
            Time.timeScale = 0f;
            encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Yeehaw!!");
            encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Your rangers were successful in defending the tamaraws. Cheers!");
            player.playerExp += 200; //player exp
            encounterPanel.SetActive(true);
           
            Debug.LogWarning("Success! Rolled: " + rand + " out of " + max + ". Roll should be less than or equal to: " + chance);
            //energy of ranger to 50%
            int flagg = 0;
            for (int x = 0; x < barracks.noRangers; x++) {
                if (barracks.transform.GetChild(x).gameObject.GetComponent<RangerScript>().isReady)
                {
                    barracks.transform.GetChild(x).gameObject.GetComponent<RangerScript>().energy -= 50;
                    flagg++;
                }

                if (flagg == no_ranger) {
                    return;
                }

            }
        }
        else
        {
            //fail

            Debug.LogWarning("Fail! Rolled: " + rand + " out of " + max + ". Roll should be less than or equal to: " + chance);
            //kill tamaraws
            flag = false;
            int toKill = Random.Range(1 * (no_poacher), 3 * no_poacher);
            player.tamarawNumber -= toKill;
            Time.timeScale = 0f;
            encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("No way!!");
            encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The poachers won this round... They killed "  + toKill+" tamaraws and drained your ranger's full energy.");
            player.playerExp += 50; //player exp
            encounterPanel.SetActive(true);

            int flagg = 0;
            for (int x = 0; x < barracks.noRangers; x++)
            {
                if (barracks.transform.GetChild(x).gameObject.GetComponent<RangerScript>().isReady)
                {
                    barracks.transform.GetChild(x).gameObject.GetComponent<RangerScript>().energy = 0;
                    flagg++;
                }

                if (flagg == no_ranger)
                {
                    return;
                }

            }


           
           

        }


    }
    

}
