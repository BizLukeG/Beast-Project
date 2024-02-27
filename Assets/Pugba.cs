using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pugba : Beast
{
    public int Ability { get; set; } = 20;
    public int MaxBaseStats { get; } = 300;
    public int NewAtt { get; set; }

    public Pugba(int level) : base()
    {
        Name = "Pugba";
        createAllStats(MaxBaseStats, level);
        Ability = 30;
        NewAtt = Att + 5;
    }

    static public Beast CreateNewPugba(int level)
    {
        Beast rndPugba = new Pugba(level);
        return rndPugba;
    }
}
