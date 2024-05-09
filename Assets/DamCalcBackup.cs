using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamCalcBackup : MonoBehaviour
{
    // public static float DamageCalc(Move moveUsed, Beast firstUnitToMove, Beast secondUnitToMove, bool firstMove)
    //{
    //    Beast attacker; Beast defender;
    //    if (firstMove)
    //    {
    //        attacker = firstUnitToMove;
    //        defender = secondUnitToMove;
    //    }
    //    else
    //    {
    //        attacker = secondUnitToMove;
    //        defender = firstUnitToMove;
    //    }

    //    int damage = 0;
    //    float effectiveness = 1;
    //    //int confusedNum = 0;
    //    bool IsBeforeMoveActivated = false;
    //    Debug.Log("attackerCondition " + attacker.Condition);
    //    ConditionID ActivatedCondition = ConditionID.None;
    //    StatusID ActivatedStatus = StatusID.None;
    //    List<ConditionID> sortedConditions = new List<ConditionID>();
    //    //ArrayList sortedConditions = new ArrayList();
    //    List<int> RandomNums = new List<int>();
    //    RandomNums.Add(4); RandomNums.Add(7); RandomNums.Add(2);

    //    int Response = RandomNums.Aggregate((smallest, next) =>
    //    next < smallest ? next : smallest
    //    );

    //    //loop to find smallest priority of condition/status and call its beforemove(). if its beforemove is activated to skip the turn then exit out of loop else find next smallest priority etc until 
    //    //no more conditions or statuses are left

    //    while (attacker.NewBeastConditions.Count > 0 || attacker.NewBeastStatuses.Count > 0 || !statusConditionActivated) {
    //        //check for lowest prio in attacker's BeastConditions
    //        ConditionID PrioResponse = attacker.NewBeastConditions.Aggregate((smallest, next) => ConditionDB.Conditions[next].Priority < ConditionDB.Conditions[smallest].Priority ? next : smallest );
    //        //need to check status for lowest prio also then compare the results PrioResponse of Condition and Statuses to see which is smaller and continue the process after
    //        StatusID PrioStatusResponse = attacker.NewBeastStatuses.Aggregate((smallest, next) => StatusDB.Statuses[next].Priority < StatusDB.Statuses[smallest].Priority ? next : smallest);

    //        if (StatusDB.Statuses[PrioStatusResponse].Priority < ConditionDB.Conditions[PrioResponse].Priority) {
    //            //see if condition fully activates
    //            StatusDB.Statuses[PrioStatusResponse].OnBeforeMove(attacker);

    //            //remove the condition so that the next loop can check the next lowest prio
    //            attacker.NewBeastStatuses.Remove(PrioStatusResponse);
    //        }
    //        else
    //        {
    //            //see if condition fully activates
    //            ConditionDB.Conditions[PrioResponse].OnBeforeMove(attacker);

    //            //remove the condition so that the next loop can check the next lowest prio
    //            attacker.NewBeastConditions.Remove(PrioResponse);
    //        }

    //        //if statusConditionActivated = true (set from condition/status database) then loop will exit
    //    }
        

    //    Debug.Log("Response " + Response);

    //    //check beast conditions and statuses to see if it gets to do its move or not

    //    //ConditionDB.Conditions[sortedCondition].OnBeforeMove(attacker);

       

    //    foreach (var beastCondition in attacker.BeastConditions)
    //    {
    //        Debug.Log("beastCondition " + beastCondition);
    //    }

    //    if (attacker.BeastConditions.Count > 0)
    //    {
            
    //        List<int> priorities = new List<int>();
    //        foreach (var condition in attacker.BeastConditions)
    //        {
    //            priorities.Add(ConditionDB.Conditions[condition].Priority);
    //        }
            

    //        List<int> sortedPriorities = new List<int>();
    //        for (int i = 1; i < 7; i++)
    //        {
    //            foreach (var priority in priorities)
    //            {
    //                Debug.Log("conditionPriority " + priority);
    //                if (priority == i)
    //                {
    //                    sortedPriorities.Add(priority);
    //                    break;
    //                }
    //            }
    //        }

    //        //List<ConditionID> sortedConditions = new List<ConditionID>();
    //        foreach (var sortedPriority in sortedPriorities)
    //        {
    //            Debug.Log("sortedConditionPriority " + sortedPriority);
    //            foreach (var condition in attacker.BeastConditions)
    //            {
    //                if (ConditionDB.Conditions[condition].Priority == sortedPriority)
    //                {
    //                    sortedConditions.Add(condition);
    //                    break;
    //                }
    //            }

    //        }
    //        foreach (var sortedCondition in sortedConditions)
    //        {
    //            if (ConditionDB.Conditions[sortedCondition].OnBeforeMove(attacker))
    //            {
    //                ActivatedCondition = sortedCondition;
    //                break;
    //            }
    //        }
         
            
    //    }

    //    foreach (var sortedCondition in sortedConditions)
    //    {
    //        Debug.Log("sortedCondition " + sortedCondition);
    //    }

    //    Debug.Log("activatedCondition " + ActivatedCondition);

    //    //check Sleep / Frozen
    //    if (ActivatedStatus == StatusID.Asleep)
    //    {
    //        BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {StatusDB.Statuses[ActivatedStatus].BeforeTurnMessage}");
    //    }


    //    if (sortedConditions.Contains(ConditionID.Confused))
    //    {
            
    //        //BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {ConditionDB.Conditions[ConditionID.Confused].CurrentlyConfusedMessage}");

    //        if (attacker.ConditionCounter == 0)
    //        {
    //            ConditionDB.Conditions[ConditionID.Confused].OnRemoveCondition(attacker);
    //            BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} snapped out of Confusion");
    //        }
    //        else
    //        {
    //            BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {ConditionDB.Conditions[ConditionID.Confused].CurrentlyConfusedMessage}");
    //            attacker.ConditionCounter--;
    //        }

    //    }
    //    //if (sortedConditions.Contains(ConditionID.Enamored))
    //    //{
    //    //    BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {ConditionDB.Conditions[ConditionID.Enamored].CurrentlyEnamoredMessage}");
    //    //}

       
    //    if(ActivatedStatus != StatusID.None)
    //    {
    //        if(ActivatedStatus == StatusID.Paralyzed)
    //        {
    //            BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {StatusDB.Statuses[ActivatedStatus].BeforeTurnMessage}");
    //        }
           
    //    }
    //    else if (ActivatedCondition != ConditionID.None)
    //    {
    //        if(ActivatedCondition == ConditionID.Flinched && attacker == secondUnitToMove)
    //        {
    //            BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {ConditionDB.Conditions[ActivatedCondition].FullyFlinchedMessage}");
    //            ConditionDB.Conditions[ActivatedCondition].OnRemoveCondition(attacker);
    //        }

    //        if(ActivatedCondition == ConditionID.Confused)
    //        {
                
           

    //                BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} {ConditionDB.Conditions[ActivatedCondition].FullyConfusedMessage}");

    //                if (moveUsed.Category == MoveCategory.Physical)
    //                {
    //                    damage = (int)Math.Round(moveUsed.Power / 100f * (attacker.ModifiedStats[StatID.Attack] - attacker.ModifiedStats[StatID.Defense]) * effectiveness, MidpointRounding.AwayFromZero);
    //                }
    //                else
    //                {

    //                    damage = (int)Math.Round(moveUsed.Power / 100f * (attacker.ModifiedStats[StatID.SpecialAttack] - attacker.ModifiedStats[StatID.SpecialDefense]) * effectiveness, MidpointRounding.AwayFromZero);
    //                    //Debug.Log($"confusion damage breakdown: Power {moveUsed.Power}, SpA: {attacker.ModifiedStats[StatID.SpecialAttack]}, SpD ");
    //                }

    //                Debug.Log("confusion damage " + damage);

    //                if (damage <= 0)
    //                {
    //                    damage = 1;
    //                }

                
    //        }
    //    }
    //    else
    //    {
    //        BattleDialog.Enqueue($"{FoeString(attacker)} {attacker.Name} used {moveUsed.Name}");

    //        if (moveUsed.Category == MoveCategory.Status)
    //        {
    //            if (defender.Status == StatusID.None)
    //            {
    //                defender.Status = moveUsed.Status;
    //                StatusDB.Statuses[defender.Status].OnStatusActivated(defender);
    //                BattleDialog.Enqueue($"{FoeString(defender)} {defender.Name} {StatusDB.Statuses[defender.Status].ActivationMessage}");
    //            }
    //            else
    //            {
    //                BattleDialog.Enqueue($"{FoeString(defender)} {defender.Name} is already {defender.Status.ToString()}");
    //            }
    //        }
    //        else if (moveUsed.Category == MoveCategory.Condition)
    //        {
    //            if ( !defender.BeastConditions.Contains(moveUsed.Condition))
    //            {
                    
    //                defender.BeastConditions.Add(moveUsed.Condition);

    //                if (moveUsed.Condition != ConditionID.Flinched)
    //                {
    //                    ConditionDB.Conditions[defender.BeastConditions[defender.BeastConditions.Count - 1]].OnConditionActivated(defender);
    //                    BattleDialog.Enqueue($"{FoeString(defender)} {defender.Name} {ConditionDB.Conditions[defender.BeastConditions[defender.BeastConditions.Count - 1]].ActivationMessage}");
    //                }
    //            }
    //            else
    //            {
    //                BattleDialog.Enqueue($"{FoeString(defender)} {defender.Name} is already {moveUsed.Condition.ToString()}");
    //            }

    //        }
    //        else if (moveUsed.Category == MoveCategory.ModifyStats)
    //        {


    //            if (moveUsed.TargetSelf == true)
    //            {

    //                foreach (var buffedStat in moveUsed.BuffedStats)
    //                {
    //                    attacker.ModifiedStats[buffedStat] += (int)Math.Round(.5 * attacker.ModifiedStats[buffedStat], MidpointRounding.AwayFromZero);
    //                }
    //                foreach (var nerfedStat in moveUsed.NerfedStats)
    //                {
    //                    attacker.ModifiedStats[nerfedStat] -= (int)Math.Round(.5 * attacker.ModifiedStats[nerfedStat], MidpointRounding.AwayFromZero);
    //                }

    //            }
    //            else if (moveUsed.TargetSelf == false)
    //            {
    //                foreach (var buffedStat in moveUsed.BuffedStats)
    //                {
    //                    defender.ModifiedStats[buffedStat] += (int)Math.Round(.5 * defender.ModifiedStats[buffedStat], MidpointRounding.AwayFromZero);
    //                }
    //                foreach (var nerfedStat in moveUsed.NerfedStats)
    //                {
    //                    defender.ModifiedStats[nerfedStat] -= (int)Math.Round(.5 * defender.ModifiedStats[nerfedStat], MidpointRounding.AwayFromZero);
    //                }
    //            }

    //        }
    //        else if (moveUsed.Category == MoveCategory.Physical)
    //        {
    //            effectiveness = TypeChart.GetEffectiveness(moveUsed.Typing, BeastBaseDB.BeastBases[defender.Name].Typing1, BeastBaseDB.BeastBases[defender.Name].Typing2);
    //            damage = (int)Math.Round(moveUsed.Power / 100f * (attacker.ModifiedStats[StatID.Attack] - defender.ModifiedStats[StatID.Defense]) * effectiveness, MidpointRounding.AwayFromZero);
    //        }
    //        else if (moveUsed.Category == MoveCategory.Special)
    //        {
    //            effectiveness = TypeChart.GetEffectiveness(moveUsed.Typing, BeastBaseDB.BeastBases[defender.Name].Typing1, BeastBaseDB.BeastBases[defender.Name].Typing2);
    //            damage = (int)Math.Round(moveUsed.Power / 100f * (attacker.ModifiedStats[StatID.SpecialAttack] - defender.ModifiedStats[StatID.SpecialDefense]) * effectiveness, MidpointRounding.AwayFromZero);
    //        }

    //        if (moveUsed.Category == MoveCategory.Physical || moveUsed.Category == MoveCategory.Special)
    //        {

    //            if (damage <= 0)
    //                damage = 1;
    //        }
    //        else { damage = 0; }


    //        defender.ModifiedStats[StatID.HP] -= damage;

    //    }

 
    //    return effectiveness;

    //}
}
