using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class tamarawGeneration : MonoBehaviour
{
  
    [SerializeField] PlayerData player;
    [SerializeField] TextMeshProUGUI tmp_tmrw; //reference to the text mesh pro tmrw
    [SerializeField] int rate = 10; //rate of generation
    void Start()
    {

        InvokeRepeating("generateTamaraw", 10, rate);

    }

    // Update is called once per frame
    void Update()
    {
        tmp_tmrw.SetText(player.tamarawNumber.ToString());

    }

    void generateTamaraw() {
        int rand = Random.Range(1, 3);
        player.tamarawNumber += rand;
    }
}
