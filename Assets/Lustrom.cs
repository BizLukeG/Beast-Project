using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lustrom : Beast
{
    public int Ability { get; set; } = 20;
    public int MaxBaseStats { get; } = 300;
    public int NewAtt { get; set; }

    public Lustrom(int level) : base()
    {
        Name = "Lustrom";
        createAllStats(MaxBaseStats, level);
        Ability = 30;
        NewAtt = Att + 5;

    }

    static public Beast CreateNewLustrom(int level)
    {
        Beast rndLustrom = new Lustrom(level);
        return rndLustrom;
    }
}
