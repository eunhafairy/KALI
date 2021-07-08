using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class freeTamaraw : MonoBehaviour
{
    WardScript ward;
    GameObject btn;
    int index;
    void Awake()
    {
        ward = GameObject.Find("Ward").GetComponent<WardScript>();
        btn = transform.GetChild(4).gameObject;
        index = transform.parent.GetSiblingIndex();
        btn.GetComponent<Button>().onClick.AddListener(delegate { ward.freeTamaraw(btn, index); });
    }

}
