using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pugba : Beast
{
    public int Ability { get; set; } = 20;
    //public int Level { get; set; } = 20;
    public int NewAtt { get; set; }

    public Pugba(int level) : base()
    {
        Ability = 30;
        NewAtt = Att + 5;
        maxBaseStats = 400;
        createAllStats(maxBaseStats, level);
        //maxBaseStats = 350;
        //Beast.Level = 4;
    }
}
