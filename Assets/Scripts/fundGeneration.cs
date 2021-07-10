using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class fundGeneration : MonoBehaviour
{
    Transform audioManager;
    public officeScript office;
    [SerializeField] GameObject notif;
    [SerializeField] PlayerData player;
    [SerializeField] TextMeshProUGUI tmp_fund; //reference to the text mesh pro fund
    [SerializeField] int rate = 10; //rate of generation
    bool flag;
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").transform;
        office = GameObject.Find("Office").GetComponent<officeScript>();
        rate = (int) (20 - (20 * office.fundRate));
        InvokeRepeating("generateFund", 10, rate);
    }

    // Update is called once per frame
    void Update()
    {

  
        string fund = string.Format("Php {0:N}", player.playerFund);
        tmp_fund.SetText(fund);


    }

    void generateFund() {
        int rand = (int) Random.Range(5000, 15000 + (15000*office.fundAmountRate));
        player.playerFund += rand;
        flag = false;
        if (notif.GetComponent<Animator>().GetBool("isShow"))
        {
            flag = true;
            StartCoroutine(notify(rand));

        }
        else
        {
            StartCoroutine(notify(rand));
        }


    }

    IEnumerator notify(int rand) {
        if (flag)
        {
            yield return new WaitForSeconds(5);
            notif.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("You've got: Php " + rand.ToString("N") + " additional funding.");
            notif.GetComponent<Animator>().SetBool("isShow", true);
        }
        else {
            notif.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("You've got: Php " + rand.ToString("N") + " additional funding.");
            notif.GetComponent<Animator>().SetBool("isShow", true);
        }
      audioManager.GetChild(5).gameObject.GetComponent<AudioSource>().PlayDelayed(1);
      yield return new WaitForSeconds(5);
    hideNotif();



    }

    void hideNotif()
    {

        notif.GetComponent<Animator>().SetBool("isShow", false);


    }
}
