using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //If your mouse hovers over the GameObject
        if (gm.windowClear())
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }



    }
    
    void OnMouseExit()
    {

        transform.localScale = new Vector3(0.4528f, 0.4744f, 1);

    }

    private void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject gameManager = GameObject.Find("GameManager");
            if (gameManager.GetComponent<GameManager>().windowClear()) {
                g_obj_barracks_panel.SetActive(true);
            }
            
            
        }
    }

}
