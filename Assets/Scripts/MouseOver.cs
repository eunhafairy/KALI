using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseOver : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject g_obj_barracks_panel;

    private void Start()
    {

        animator = GetComponent<Animator>();
    }
    void OnMouseOver()
    {
        animator.SetBool("isHover",true);
   
        //If your mouse hovers over the GameObject with the script attached, output this message
   

        if (Input.GetMouseButtonDown(0)) { 
            g_obj_barracks_panel.SetActive(true);
            Debug.Log("Wnt here");
        }
    }
    
    void OnMouseExit()
    { 
        animator.SetBool("isHover", false);
        //The mouse is no longer hovering over the GameObject so output this message each frame

    }


  
}
