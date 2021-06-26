using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class fundGeneration : MonoBehaviour
{

    [SerializeField] GameObject notif;
    [SerializeField] PlayerData player;
    [SerializeField] TextMeshProUGUI tmp_fund; //reference to the text mesh pro fund
    [SerializeField] int rate = 10; //rate of generation
    bool flag;
    void Start()
    {
        InvokeRepeating("generateFund", 10, rate);

    }

    // Update is called once per frame
    void Update()
    {
        tmp_fund.SetText(player.playerFund.ToString());


    }

    void generateFund() {
        int rand = Random.Range(1000,3000);
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
            notif.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("You've got: " + rand + " funds.");
            notif.GetComponent<Animator>().SetBool("isShow", true);
        }
        else {
            notif.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("You've got: " + rand + " funds.");
            notif.GetComponent<Animator>().SetBool("isShow", true);
        }
    
    yield return new WaitForSeconds(5);
    hideNotif();



    }

    void hideNotif()
    {

        notif.GetComponent<Animator>().SetBool("isShow", false);


    }
}
