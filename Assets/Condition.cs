using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Condition 
{
    public string ActivationMessage { get; set; }
    public string FullyConfusedMessage { get; set; }
    

    public Action<Beast> OnConditionActivated { set; get; }

    public Func<Beast, bool> OnBeforeMove { get; set; }
}
