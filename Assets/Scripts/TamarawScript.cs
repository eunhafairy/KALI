using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TamarawScript : MonoBehaviour
{
    public int health;
    public bool isHealthy;
    public int recovery;
    float vets, wardens;
    [SerializeField]  float rate, recoverRate;
    [SerializeField] ClinicScript clinic;
    [SerializeField] WardScript ward;
    [SerializeField] GameObject warningPanel;
    private void Awake()
    {
        clinic = GameObject.Find("Clinic").GetComponent<ClinicScript>();
        ward = GameObject.Find("Ward").GetComponent<WardScript>();

         vets = clinic.noVet;
        wardens = ward.noWardens;
        rate = 1 - (vets * 0.1f);
        recoverRate = 1 - (wardens * 0.1f);
        InvokeRepeating("regenerateHealth", 0, rate);
        InvokeRepeating("recover", 0, recoverRate);

    }
    void FixedUpdate()
    {
        rate = 1 - (vets * 0.1f);
        recoverRate = 1 - (wardens * 0.1f);
        if (health >= 100)
        {
            isHealthy = true;
            health = 100;
        }
        else
        {
            isHealthy = false;
        }

    }
    void regenerateHealth() {

            if (!isHealthy && health < 100)
            {
                health++;

                
            }

        
    }

    void recover() {

        if (gameObject.transform.parent.parent.name == "Ward") {

            if (recovery < 100)
            {
                recovery++;
            }
        }
        
    }
    

}
