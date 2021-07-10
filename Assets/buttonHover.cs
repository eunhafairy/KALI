using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buttonHover : MonoBehaviour
{

    GameObject img;
    // Update is called once per frame
    private void Start()
    {
        img = this.transform.GetChild(1).gameObject;
    }

    private void onHover()
    {
        Debug.Log("Went here");
        img.SetActive(true);
    }
    private void OnMouseExit()
    {
        Debug.Log("Went here out");

        img.SetActive(false);

    }
    
}
