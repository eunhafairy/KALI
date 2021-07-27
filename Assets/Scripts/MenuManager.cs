using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    
    [SerializeField] GameObject continueMenu, newMenu, dialogBox, loadScreen;
    // Start is called before the first frame update
    void Start()
    {
        loadScreen.SetActive(false);

        Time.timeScale = 1f;
        dialogBox.SetActive(false);
        continueMenu.SetActive(false);
        newMenu.SetActive(false);
        string path = Path.Combine(Application.persistentDataPath, "playerData.ss");
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
        loadScreen.SetActive(true);
        yield return new WaitForSeconds(3);
        
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (string filePath in filePaths)
        {

            if (filePath == Path.Combine(Application.persistentDataPath, "playerData.ss"))
            {
                Debug.Log("To delete: "+filePath);
                File.Delete(filePath);
            }
            else { 
                Debug.Log("Not to: "+filePath);

            }


        }

        yield return new WaitForSeconds(1);
        Debug.Log("Going to next scene...");
        SceneManager.LoadScene(1);
        //go to scene 
    }

    IEnumerator newGameRoutine() {

        animator.SetTrigger("triggerStart");
        yield return new WaitForSeconds(2);
        loadScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        //go to scene
        SceneManager.LoadScene(1);
    }
    IEnumerator loadGameRoutine() {
        animator.SetTrigger("triggerStart");
        yield return new WaitForSeconds(2);
        loadScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        PlayerPrefs.SetInt("isLoad", 1);
        SceneManager.LoadScene(1);

    }

    public void exitGame() {
        Application.Quit();
    }

    

}
