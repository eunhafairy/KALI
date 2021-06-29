using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class rangerStatus : MonoBehaviour
{
    Slider slider;
    Image image;
    [SerializeField] Sprite rest, ready;
    private void Awake()
    {
        slider = transform.GetChild(2).gameObject.GetComponent<Slider>();
        image = transform.GetChild(3).gameObject.GetComponent<Image>();
    }
    private void Update()
    {
        if (slider.value < slider.maxValue)
        {
            image.sprite = rest;
        }
        else {
            image.sprite = ready;

        }
    }
}
