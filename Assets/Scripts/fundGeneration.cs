using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class fundGeneration : MonoBehaviour
{

    [SerializeField] PlayerData player;
    [SerializeField] TextMeshProUGUI tmp_fund; //reference to the text mesh pro fund
    [SerializeField] int rate = 10; //rate of generation
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
    }
}
