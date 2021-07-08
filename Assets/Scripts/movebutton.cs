using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class movebutton : MonoBehaviour
{

    ClinicScript clinic;
    GameObject btn;
    int index;
    void Awake()
    {
        clinic = GameObject.Find("Clinic").GetComponent<ClinicScript>();
        btn = transform.GetChild(4).gameObject;
        index = transform.parent.GetSiblingIndex();
        btn.GetComponent<Button>().onClick.AddListener(delegate{ clinic.releaseTamaraw(btn, index); });
    }

    
}
