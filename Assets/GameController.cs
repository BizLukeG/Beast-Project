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
        Beast Roger = new Beast();
        Debug.Log(Roger.att);
        Debug.Log(Roger.spDef);
        Debug.Log(Roger.baseStats.baseAtt);
        Debug.Log("Howdy");
    }

    // Update is called once per frame
    void Update()
    {
        Console.WriteLine("Howdy");
    }
}
