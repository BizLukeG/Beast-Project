using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expurn : Beast
{
    public int Ability { get; set; } = 20;
    public int MaxBaseStats { get; } = 300;
    public int NewAtt { get; set; }

    public Expurn(int level) : base()
    {
        
        createAllStats(MaxBaseStats, level);
        Ability = 30;
        NewAtt = Att + 5;
        
    }

    static public Beast CreateNewExpurn(int level)
    {
        Beast rndExpern = new Expurn(level);
        return rndExpern;
    }
}

//Need a seperate class for each type of beast but not each type of move because each subtype of Beast can vary but each subtype of move doesn't