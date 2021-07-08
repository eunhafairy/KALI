using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    //player
    public int fund;
    public int tamarawNumber;
    public int playerLevel;
    public int playerExp;

    //barracks
    public int barracksLevel;
    public int noRangers;
    public int[] rangerEnergy;

    //clinic
    public int clinicLevel;
    public int noVet;
    public int noRooms;
    public int[] tamarawHealth;
    public int noAdmitted;


    //ward
    public int wardLevel;
    public int noWardens;
    public int[] tamarawRecovery;
    public int tamarawRecovering;

    public Data(PlayerData player, BarracksScript barracks, ClinicScript clinic, WardScript ward) {
        
        rangerEnergy = new int[6];
        fund = player.playerFund;
        tamarawNumber = player.tamarawNumber;
        playerLevel = player.playerLevel;
        playerExp = player.playerExp;

        barracksLevel = barracks.level;
        noRangers = barracks.noRangers;
        for (int x = 0; x< noRangers; x++) {
            rangerEnergy[x] = barracks.rangerEnergy[x];
        }

        tamarawHealth = new int[12];
        clinicLevel = clinic.level;
        noVet = clinic.noVet;
        noRooms = clinic.noRooms;
        noAdmitted = clinic.noAdmitted;

        for (int x = 0; x < noAdmitted; x++) {
            tamarawHealth[x] = clinic.tamarawHealth[x];

        }

        tamarawRecovery = new int[12];
        wardLevel = ward.level;
        noWardens = ward.noWardens;
        tamarawRecovering = ward.tamarawRecovering;

        for (int x = 0; x < tamarawRecovering; x++) {
            tamarawRecovery[x] = ward.tamarawRecovery[x];
        }

    }


}
