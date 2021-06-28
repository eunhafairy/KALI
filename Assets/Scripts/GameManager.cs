using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerData player;
    [SerializeField] BarracksScript barracks;
    [SerializeField] GameObject[] rangers;
    [SerializeField] GameObject notif, salaryPanel, barracksPanel, expSlider;
    bool flag;
    private void Start()
    {

        salaryPanel.SetActive(false);
        flag = false;
        notif.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Hello!");
   

        InvokeRepeating("salary",10,30);
    }
    public void SavePlayer() {

        SaveSystem.SavePlayer(player, barracks);

    
    }

    public void LoadPlayer() {

        Data data = SaveSystem.loadPlayer();

        player.tamarawNumber = data.tamarawNumber;
        player.playerFund = data.fund;
        
        barracks.level = data.barracksLevel;
        barracks.noRangers = data.noRangers;
        
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

        
        int no_emp = (barracks.noRangers)*2000;
        Debug.LogWarning("Payday! Your funds are deducted by: "+no_emp);
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
        salaryPanel.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>().SetText("Php " + (2000 * barracks.noRangers).ToString("N")); //total salary



    }

    private void Update()
    {
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
    }

    public void globalPause() {
        Time.timeScale = 0f;
    
    }


}
