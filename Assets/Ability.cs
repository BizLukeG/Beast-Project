using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ability
{
    
    //AbilityID Name { set; get; }
    public int Priority { get; set; }
    public AbilityID Name { set; get; }

    public Action<Beast> OnCheckAbility { get; set; }
    
    public int Lol;
    //public Action<Beast> OnAfterFullTurn { get; set; }

    //public Action<Beast, Beast, Move> OnSecondaryEffect { get; set; }

    //public Func<Beast, bool> OnBeforeMove { get; set; }
}

