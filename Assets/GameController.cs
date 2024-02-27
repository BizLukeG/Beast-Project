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

        Beast getBeastPerRoute(AreaID Route){
            System.Random r = new System.Random();
            int rLevel = r.Next(AreaDB.Areas[Route].LevelRange[0], AreaDB.Areas[Route].LevelRange[1]);
            int rBeastIndex = r.Next(0, Enum.GetNames(typeof(BeastID)).Length);

            return AreaDB.Areas[Route].AvailableBeasts[rBeastIndex].Invoke(rLevel);
        }

        //Debug.Log(getBeastPerRoute(AreaID.Route101).Name);
        getBeastPerRoute(AreaID.Route101).CheckAllStats();
        getBeastPerRoute(AreaID.Route101).CheckAllStats();

        //AreaDB.Areas[AreaID.Route101].AvailableBeasts[0].Invoke(AreaDB.Areas[AreaID.Route101].LevelRange[0]).CheckAllStats();
        //AreaDB.Areas[AreaID.Route101].AvailableBeasts[0].Invoke(AreaDB.Areas[AreaID.Route101].LevelRange[0]).CheckAllStats();


        //foreach( var availableBeast in AreaDB.Areas[AreaID.Route101].AvailableBeasts)
        //{
        //    Debug.Log("Name: " + availableBeast.Invoke(AreaDB.Areas[AreaID.Route101].LevelRange[0]).Name);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
