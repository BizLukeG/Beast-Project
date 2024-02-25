using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expurn : Beast
{
    public int Ability { get; set; } = 20;
    //public int Level { get; set; } = 20;
    public int NewAtt { get; set; }

    public Expurn(/*int level*/) : base()
    {
        maxBaseStats = 300;
        createAllStats(300);
        Ability = 30;
        NewAtt = Att + 5;
        //Beast.Level = 4;
    }
}
