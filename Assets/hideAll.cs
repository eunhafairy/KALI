using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideAll : MonoBehaviour
{
    [SerializeField] GameObject barracks; 
    private void Awake()
    {
        hideWindow();
    }
    void hideWindow() {

        barracks.SetActive(false);
    }
}
