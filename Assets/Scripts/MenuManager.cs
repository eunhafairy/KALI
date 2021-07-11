using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject continueMenu, newMenu, dialogBox;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        dialogBox.SetActive(false);
        continueMenu.SetActive(false);
        newMenu.SetActive(false);
        string path = Application.persistentDataPath + "/playerData.ss";
        if (File.Exists(path))
        {
            //file exists. show with continue
            continueMenu.SetActive(true);

        }

        else
        {
            //file doesn't exist, show only new game
            newMenu.SetActive(true);
        }
    }

    public void loadPlayer() {

        StartCoroutine(loadGameRoutine());

    }

    public void newGame() {

        if (continueMenu.activeSelf)
        {

            //show warning
            dialogBox.SetActive(true);


        }
        else {
            PlayerPrefs.SetInt("isLoad", 0);
            StartCoroutine(newGameRoutine());



        }


    }

    public void deleteOld() {
        StartCoroutine(deleteRoutine());
    }
    public IEnumerator deleteRoutine() {

        animator.SetTrigger("triggerStart");
        yield return new WaitForSeconds(2);
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (string filePath in filePaths)
        {
            File.Delete(filePath);

        }
       
        SceneManager.LoadScene(1);
        //go to scene 
    }

    IEnumerator newGameRoutine() {

        animator.SetTrigger("triggerStart");
        yield return new WaitForSeconds(2);
        //go to scene
        SceneManager.LoadScene(1);
    }
    IEnumerator loadGameRoutine() {
        animator.SetTrigger("triggerStart");
        yield return new WaitForSeconds(2);
        PlayerPrefs.SetInt("isLoad", 1);
        SceneManager.LoadScene(1);

    }

    public void exitGame() {
        Application.Quit();
    }

    

}
