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

    public Data(PlayerData player, BarracksScript barracks) {
        
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
    }


}
