using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Status
{
   public int TurnCount { set; get; }
   StatusID Name { set; get; }
   public string Abbreviation { set; get; }
   public string ActivationMessage { set; get; } //Beast was burned
   

   public Action<Beast> OnStatusActivated { get; set; }

   public Action<Beast> OnAfterFullTurn { get; set; }

    public Func<Beast, bool> OnBeforeMove { get; set; }
}

