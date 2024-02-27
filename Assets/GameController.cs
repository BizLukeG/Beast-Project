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
        //Pugba rndPugba = new Pugba();




        //rndPugba.CheckAllStats();
        //rndPugba.CheckAllStats();

        //Beast rndExp = BeastCreatorDB.BeastCreators[(BeastID)0].Invoke(7);
        //Beast rndExp2 = BeastCreatorDB.BeastCreators[(BeastID)0].Invoke(7);

        //Beast rndExp = new Expurn();
        //Beast rndExp2 = new Expurn();

        //rndExp.CheckAllStats();
        //rndExp2.CheckAllStats();

        Debug.Log("Hello " + MoveDB.Moves[MoveID.smack].power);

        AreaDB.Areas[AreaID.Route101].AvailableBeasts[0].Invoke(AreaDB.Areas[AreaID.Route101].LevelRange[0]).CheckAllStats();
        AreaDB.Areas[AreaID.Route101].AvailableBeasts[0].Invoke(AreaDB.Areas[AreaID.Route101].LevelRange[0]).CheckAllStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
