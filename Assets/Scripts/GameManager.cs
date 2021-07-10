using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class GameManager : MonoBehaviour
{
    Transform audioManager;
    [SerializeField] PlayerData player;
    [SerializeField] BarracksScript barracks;
    [SerializeField] ClinicScript clinic;
    [SerializeField] officeScript office;
    [SerializeField] GameObject[] rangers;
    [SerializeField] GameObject notif, salaryPanel, barracksPanel, expSlider, encounterPanel, clinicPanel, pausePanel, poachingWindow, deployWindow, buyRanger, gameover, injuredPanel, canvasPanel;
    [SerializeField] TextMeshProUGUI tmp_plyerLvl;
    [SerializeField] WardScript ward;
    [SerializeField] DragCamera2D drag;
    [SerializeField] int injuredRate;
    bool flag;
    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").transform;
        injuredPanel.SetActive(false);
        salaryPanel.SetActive(false);
        flag = true;
        notif.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Hello!");
        injuredRate = 30;
        InvokeRepeating("findInjured", 10, injuredRate);
        InvokeRepeating("salary",20,50);
    }
    public void SavePlayer() {

        SaveSystem.SavePlayer(player, barracks, clinic, ward, office);

        
        Time.timeScale = 0f;
        encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Success!");
        encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The current game progress is saved.");
        encounterPanel.SetActive(true);

    }

    public void LoadPlayer() {

        Data data = SaveSystem.loadPlayer();

        player.tamarawNumber = data.tamarawNumber;
        player.playerFund = data.fund;
        
        barracks.level = data.barracksLevel;
        barracks.noRangers = data.noRangers;
        clinic.level = data.clinicLevel;
        
        for (int x = 0; x < barracks.noRangers; x++) {
            barracks.transform.GetChild(x).gameObject.GetComponent<RangerScript>().energy = data.rangerEnergy[x];
        
        }
    }

  

    public void deleteFiles() {

        string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (string filePath in filePaths) {
            File.Delete(filePath);
       
        }

    
    }

    public void salary() {

        
        int no_emp = (barracks.noRangers*2000) + (clinic.noVet * 3000) + (ward.noWardens * 2000);
        player.playerExp += 200;
        player.playerFund -= no_emp;
        salaryNotice();
       
     
    }

    void salaryNotice()
    {

        Time.timeScale = 0f;
        barracksPanel.SetActive(false);
        salaryPanel.SetActive(true);
        salaryPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Rangers x"+ barracks.noRangers); //ranger number
        salaryPanel.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().SetText("Php "+(2000 * barracks.noRangers).ToString("N")); //ranger total
        salaryPanel.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>().SetText("Wardens x" + ward.noWardens); //ranger number
        salaryPanel.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>().SetText("Php " + (2000 * ward.noWardens).ToString("N")); //ranger total
        salaryPanel.transform.GetChild(5).gameObject.GetComponent<TextMeshProUGUI>().SetText("Vets x" + clinic.noVet); //vet number
        salaryPanel.transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>().SetText("Php " + (3000 * clinic.noVet).ToString("N")); //vet total
        salaryPanel.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText("-Php " + ((2000 * barracks.noRangers) + (3000* clinic.noVet)).ToString("N")); //total salary




    }

    private void Update()
    {
        if (windowClear()) {
            drag.enabled = true;
        }
        else { 
            drag.enabled = false;

        }
        switch (player.playerLevel) {

            case 1:
                expSlider.GetComponent<Slider>().minValue = 0;
                expSlider.GetComponent<Slider>().maxValue = 1000;
                break;

            case 2:
                expSlider.GetComponent<Slider>().minValue = 1000;
                expSlider.GetComponent<Slider>().maxValue = 4000;
                break;

            case 3:
                expSlider.GetComponent<Slider>().minValue = 4000;
                expSlider.GetComponent<Slider>().maxValue = 9000;
                break;


        }

        expSlider.GetComponent<Slider>().value = player.playerExp;

        tmp_plyerLvl.SetText("Lvl."+player.playerLevel);

        if (player.playerFund < 0)
        {
            StartCoroutine(countdown());
        }
        else {
            flag = true;
            StopAllCoroutines();
        }

        Debug.Log(windowClear());

        if (player.tamarawNumber < 1) {
            gameOver();
        }


     
    }
    void findInjured()
    {
        int rand = Random.Range(1,100);
        if (rand <= 50 ) {
            Time.timeScale = 0f;
            injuredPanel.SetActive(true);
        }
       
        
    }
    public void killInjured()
    {
        player.tamarawNumber -= 1;
        Time.timeScale = 0f;
        encounterPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().SetText("Aww...");
        encounterPanel.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("The injured tamaraw died.");
        encounterPanel.SetActive(true);

    }


    void gameOver() {
        Time.timeScale = 0f;
        gameover.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("Tamaraw became extinct.");
        gameover.SetActive(true);
    }

    public void globalPause() {
        Time.timeScale = 0f;
    
    }

    IEnumerator countdown() {

        if (flag) {
            while (player.playerFund < 0) {
                flag = false;
               
                yield return new WaitForSeconds(5);
                Time.timeScale = 0f;
                gameover.SetActive(true);
            }
            
        }
      
    
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    
    }

    public void exitGame() {
        Application.Quit();
    
    }

    public bool noOtherWindow() {

        
        if (barracksPanel.activeSelf)
        {
            return false;


        }
        else if (clinicPanel.activeSelf)
        {
            return false;

        }
        else if (pausePanel.activeSelf)
        {
            return false;



        }
        else if (salaryPanel.activeSelf) {
            return false;


        }
        else if (poachingWindow.activeSelf)
        {
            return false;


        }
        else if (deployWindow.activeSelf) {
            return false;

        }
        else if (encounterPanel.activeSelf) {
            return false;
        }
        else if (gameover.activeSelf) {
            return false;

        }
        else if (buyRanger.activeSelf)
        {
            return false;

        }
        else if (injuredPanel.activeSelf) {
            return false;
        }

        else {
            return true;
        }



             
    }

    public bool windowClear() {


        
      for (int x = 0; x < canvasPanel.transform.GetChild(1).childCount; x++) {

                if (canvasPanel.transform.GetChild(1).GetChild(x).gameObject.activeSelf) {
                    return false;
                }
                
        }

        int flag = 0;
        for (int x = 0; x < canvasPanel.transform.childCount; x++ )
        {
            if (canvasPanel.transform.GetChild(x).gameObject.activeSelf) {
                flag++;
            }
            
        }

        if (flag <= 2)
        {
            return true;
        }
        else {
            return false;
        }

        

    
    }

    


}
