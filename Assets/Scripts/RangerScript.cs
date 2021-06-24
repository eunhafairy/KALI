using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerScript : MonoBehaviour
{
    public bool isReady;
    public int energy;
    void Awake()
    {
       // isReady = true;
       // energy = 100;
        InvokeRepeating("Regenerate", 0, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (energy == 100)
        {
            isReady = true;
        }

    }

    void Regenerate() {
       
        
        if (!isReady)
        {
            energy++;
        }

    }
}
