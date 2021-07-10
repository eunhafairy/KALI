using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragCamera : MonoBehaviour
{
    
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;
    private void LateUpdate()
    {
        if(Input.GetMouseButton(0)) {
            Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (Drag == false)
            {
                Drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        } else
        {
            Drag = false;
        }
        if (Drag == true && gameObject.GetComponent<GameManager>().windowClear())
        {
            Camera.main.transform.position = Origin - Diference;
        }
       


    }
}
