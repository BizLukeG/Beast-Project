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



        
        rndPugba.CheckAllStats();

        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
