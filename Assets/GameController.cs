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
        BeastBaseDB.Init();
        MoveDB.Init();
        Debug.Log("Howdy");
;

        Beast getBeastPerRoute(AreaID Route) {
            System.Random r = new System.Random();
            int rLevel = r.Next(AreaDB.Areas[Route].LevelRange[0], AreaDB.Areas[Route].LevelRange[1]);
            int rBeastIndex = r.Next(0, Enum.GetNames(typeof(BeastID)).Length);
            Debug.Log("NameIDLength " + Enum.GetNames(typeof(BeastID)).Length);
            Debug.Log("rBeastIndex " + rBeastIndex);
            Debug.Log("BeastID2 " + (BeastID)rBeastIndex);
            Debug.Log("BeastName " + BeastBaseDB.BeastBases[(BeastID)rBeastIndex].Name);
            Beast beast = new Beast(BeastBaseDB.BeastBases[(BeastID)rBeastIndex].Name, rLevel, BeastBaseDB.BeastBases[(BeastID)rBeastIndex].MaxBaseStats);
            //beast.createMoveSet(beast.Name);

            return beast;
        }

    

        Beast currentBeast = getBeastPerRoute(AreaID.Route101);
        currentBeast.CheckAllStats();
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
