using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class releaseTamaraw : MonoBehaviour
{
    [SerializeField] GameObject warning;
    private void Awake()
    {
        warning = GameObject.Find("EncounterPanel");
    }
    public void showWarning() {

        Time.timeScale = 0f;
        warning.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success");
        warning.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The tamaraw has been released.");

    }
}
