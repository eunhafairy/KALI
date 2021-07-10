using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MouseOver : MonoBehaviour
{
    AudioSource hover;
    GameObject audioManager;
    Animator animator;
    [SerializeField] GameObject g_obj_barracks_panel;
    Vector3 scale;
    private void Start()
    {
        hover = GameObject.Find("AudioManager").transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        audioManager = GameObject.Find("AudioManager");
        scale = transform.localScale;
        animator = GetComponent<Animator>();
    }
    void OnMouseOver()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();


        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //If your mouse hovers over the GameObject
        else
        {
            
            transform.localScale = new Vector3(scale.x + 0.02f, scale.y + 0.02f, 1);
            if (hover.isPlaying) hover.Stop();
            hover.Play();
        }



    }
    
    void OnMouseExit()
    {

        transform.localScale = scale;

    }

    private void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Clicked on the UI");
        }

        else
        {
            GameObject gameManager = GameObject.Find("GameManager");
            if (gameManager.GetComponent<GameManager>().windowClear()) {
                g_obj_barracks_panel.SetActive(true);
                
            }
            
            
        }
    }

}
