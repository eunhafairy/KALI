using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class rangerCost : MonoBehaviour
{
    TextMeshProUGUI message;
    [SerializeField] GameObject barracks;
    void Awake() {
        message = transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        message.SetText("Hire additional ranger for" +
            "\nPhp " + barracks.GetComponent<BarracksScript>().rangerCost.ToString("N")+"?");
    
    
    }
    private void Update()
    {
        message.SetText("Hire additional ranger for" +
           "\nPhp " + barracks.GetComponent<BarracksScript>().rangerCost.ToString("N") + "?");
    }

}
