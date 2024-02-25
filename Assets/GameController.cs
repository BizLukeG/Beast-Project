using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    //private void Awake()
    //{
    //    Beast Roger = new Beast();
    //    Console.WriteLine(Roger.stats);
    //}

        // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log("Howdy");

 
        //Expurn rndExp = (Expurn)BeastDB.Beasts[BeastID.Expurn];
        //Expurn rndExp = new Expurn();
        Pugba rndPugba = new Pugba();

        void CheckAllStats(Beast beast)
        {
            Debug.Log("Stats: ");
            foreach (var stat in beast.Stats)
            {
                Debug.Log(stat);
            }


            Debug.Log("BaseStats: ");
            foreach (var baseStat in beast.BaseStats.ActualBaseStats)
            {
                Debug.Log(baseStat);
            }


            Debug.Log("BaseStatsLimits: ");
            foreach (var baseStatLimit in beast.BaseStats.BaseStatsLimits)
            {
                Debug.Log(baseStatLimit);
            }
        }

        //CheckAllStats(rndExp);
        CheckAllStats(rndPugba);

        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
