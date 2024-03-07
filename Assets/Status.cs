using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Status
{
   int TurnCount { set; get; }
   StatusID Name { set; get; }
   string Abbreviation { set; get; }
   string ActivationMessage { set; get; } //Beast was burned
   

   public Action<Beast> OnStatusActivated { get; set; }

   public Action<Beast> OnAfterFullTurn { get; set; }

    public Func<Beast, bool> OnBeforeMove { get; set; }
}

