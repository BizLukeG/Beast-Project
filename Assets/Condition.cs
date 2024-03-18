using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Condition 
{
    public string ActivationMessage { get; set; }
    public string CurrentlyConfusedMessage { get; set; }
    public string FullyConfusedMessage { get; set; }
    public string FullyFlinchedMessage { get; set; }
    public ConditionID Name { get; set; }
    public int Priority { get; set; }

    public Action<Beast> OnRemoveCondition { get; set; }

    public Action<Beast> OnConditionActivated { get; set; } = (Beast beast) => { };

    public Func<Beast, bool> OnBeforeMove { get; set; }
}
