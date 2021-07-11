using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class loadProgress : MonoBehaviour
{

    [SerializeField] researchScript research;
    [SerializeField] PlayerData player;
    [SerializeField] BarracksScript barracks;
    [SerializeField] ClinicScript clinic;
    [SerializeField] GameObject progressPrefab, roomPrefab;
    [SerializeField] Transform progressPanel;
    [SerializeField] WardScript ward;
    [SerializeField] officeScript office;
    [SerializeField] GameObject rangerField, rangerFieldPrefab, wardenField, wardenFieldPrefab, tamField, tamFieldPanel;

    void Start()
    {

        //barracks init
        barracks.ranger = new GameObject[6];
        barracks.rangerEnergy = new int[6];
        barracks.progressCard = new GameObject[6];

        //clinic
        clinic.tamarawHealth = new int[12];
        clinic.roomCard = new GameObject[12];


        //ward
        ward.tamarawRecovery = new int[12];
        ward.recoveryCardArr = new GameObject[12];

        string path = Application.persistentDataPath + "/playerData.ss";
        if (File.Exists(path))
        {
            //existing
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();

            //load player data
            player.playerFund = data.fund;
            player.tamarawNumber = data.tamarawNumber;
            player.playerLevel = data.playerLevel;
            player.playerExp = data.playerExp;
            player.levelFlag2 = data.levelFlag2;
            player.levelFlag3 = data.levelFlag3;


            //load barracks data
            barracks.level = data.barracksLevel;
            barracks.noRangers = data.noRangers;
            barracks.rangerEnergy = data.rangerEnergy;

            //instantiate rangers prefabs as child based on noRangers
            for (int x = 0; x < barracks.noRangers; x++)
            {
                Instantiate(barracks.rangers, barracks.transform);

            }
            for (int x = 0; x < barracks.noRangers; x++)
            {
                barracks.transform.GetChild(x).gameObject.GetComponent<RangerScript>().energy = barracks.rangerEnergy[x];

            }

            //load  clinic data
            clinic.level = data.clinicLevel;
            clinic.noVet = data.noVet;
            clinic.noRooms = data.noRooms;
            clinic.noAdmitted = data.noAdmitted;

            for (int x = 0; x < clinic.noVet; x++)
            {
                Instantiate(clinic.vet, clinic.transform.GetChild(0));

            }
            for (int x = 0; x < clinic.noRooms; x++)
            {
                Instantiate(clinic.room, clinic.transform.GetChild(1));

            }
            


            for (int x = 0; x < data.noAdmitted; x++)
            {
               
                clinic.tamarawHealth[x] = data.tamarawHealth[x];
            }

            //load ward data

            ward.level = data.wardLevel;
            ward.noWardens = data.noWardens;
            ward.tamarawRecovering = data.tamarawRecovering;

            for (int x=  0; x< ward.tamarawRecovering; x++) {
                ward.tamarawRecovery[x] = data.tamarawRecovery[x];
            }

            //load office data
            office.level = data.officeLevel;
            office.fundAmountRate = data.fundAmountRate;
            office.fundRate = data.fundRate;
            office.webinar = data.webinar;
            office.tarp = data.tarp;
            office.socialMedia = data.socialMedia;

            //load research data
            research.level = data.researchLevel;
            research.native = data.native;
            research.scout = data.scout;
            research.artificial = data.artificial;
        }

        else {

            //set default values
            //player
            player.playerFund = 10000;
            player.tamarawNumber = 100;
            player.playerLevel = 1;
            player.playerExp = 0;
            player.levelFlag2 = true;
            player.levelFlag3 = true;


            //barracks
            barracks.level = 1;
            barracks.noRangers = 1;
            Instantiate(barracks.rangers, barracks.transform);
            barracks.ranger[0] = barracks.transform.GetChild(0).gameObject;
            barracks.rangerEnergy[0] = 100;
            barracks.ranger[0].GetComponent<RangerScript>().energy = 100;

            //clinic

            clinic.level = 1;
            clinic.noVet = 1;
            clinic.noRooms = 4;
            clinic.noAdmitted = 0;
            Instantiate(clinic.vet, clinic.transform.GetChild(0));
            Instantiate(clinic.room, clinic.transform.GetChild(1));
            Instantiate(clinic.room, clinic.transform.GetChild(1));

            //warden
            ward.level = 1;
            ward.noWardens = 1;
            ward.maxCapacity = 4;
            ward.tamarawRecovering = 0;
            Instantiate(ward.warden, ward.transform.GetChild(0));

            //office
            office.level = 1;
            office.fundAmountRate = 0.1f;
            office.fundRate = 0.1f;
            office.webinar = false;
            office.tarp = false;
            office.socialMedia = false;

            //research
            research.level = 1;
            research.native = false;
            research.scout = false;
            research.artificial = false;

        }

        ward.maxCapacity = ward.level * 4;

        //barracks scrollview initialization
        for (int x = 0; x < barracks.noRangers; x++)
        {

            Instantiate(progressPrefab, progressPanel);


        }
        for (int x = 0; x < barracks.noRangers; x++)
        {

            barracks.progressCard[x] = progressPanel.GetChild(x).gameObject;

        }

        switch (barracks.level) {
            case 1:
                barracks.GetComponent<SpriteRenderer>().sprite = barracks.lvl1;
                break;
            case 2:
                barracks.GetComponent<SpriteRenderer>().sprite = barracks.lvl2;
                break;
            case 3:
                barracks.GetComponent<SpriteRenderer>().sprite = barracks.lvl3;
                break;
        }

        //rangers init

        for (int x = 0; x < barracks.noRangers;x++) {
            Instantiate(rangerFieldPrefab, rangerField.transform);
        }
        


        //clinic scrollview initialization

        for (int x = 0; x < clinic.noRooms; x++) {
            Instantiate(roomPrefab, clinic.roomPanel.transform);
        }
        for (int x = 0; x < clinic.noRooms; x++){
            clinic.roomCard[x] = clinic.roomPanel.transform.GetChild(x).gameObject;
        }
        for (int x = 0; x < clinic.noAdmitted; x++)
        {
            Instantiate(clinic.tamaraw, clinic.transform.GetChild(2));
            
        }
        for (int x = 0; x < clinic.noAdmitted; x++)
        {

         clinic.transform.GetChild(2).GetChild(x).gameObject.GetComponent<TamarawScript>().health = clinic.tamarawHealth[x];
        }

        //warden scrollview init
        for (int x = 0; x < ward.maxCapacity; x++) {
         
            Instantiate(ward.recoveryCard, ward.scrollPanel.transform);
        }

        for (int x = 0; x < ward.tamarawRecovering; x++) {
            GameObject newTam = Instantiate(ward.tamaraw, ward.transform.GetChild(1));
            newTam.GetComponent<TamarawScript>().recovery = ward.tamarawRecovery[x];
        }
        for (int x = 0; x < ward.noWardens; x++)
        {
            Instantiate(wardenFieldPrefab, wardenField.transform);
        }
        for (int x = 0; x < ward.tamarawRecovering; x++)
        {
            Instantiate(tamField, tamFieldPanel.transform);
           
        }

    }


    }
