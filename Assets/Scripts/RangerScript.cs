using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerScript : MonoBehaviour
{
    BarracksScript barracks;
    public bool isReady;
    public int energy;
    int rate;
    void Awake()
    {
        rate = 1;
        barracks = GameObject.Find("Barracks").GetComponent<BarracksScript>();
       // isReady = true;
       // energy = 100;
        InvokeRepeating("Regenerate", 0, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (energy >= 100)
        {
            isReady = true;
            energy = 100;
            
        }
        else {
            isReady = false;
        }

    }

    void Regenerate() {
       
        
        if (!isReady && energy < 100)
        {
            rate = 1 + (barracks.level / 2);
            energy+= rate ;

        }

    }
}
