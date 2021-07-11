using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class tamarawGeneration : MonoBehaviour
{
    [SerializeField] researchScript research;
    [SerializeField] PlayerData player;
    [SerializeField] TextMeshProUGUI tmp_tmrw; //reference to the text mesh pro tmrw
    [SerializeField] int rate; //rate of generation
    void Start()
    {
        research = GameObject.Find("Research").GetComponent<researchScript>();
        rate = 40;
        if (research.artificial) rate -= (int)(40*.15f);
        InvokeRepeating("generateTamaraw", 10, rate);

    }

    // Update is called once per frame
    void Update()
    {
        tmp_tmrw.SetText(player.tamarawNumber.ToString());

    }

    void generateTamaraw() {
        int rand = Random.Range(1, 2);
        player.tamarawNumber += rand;
    }
}
