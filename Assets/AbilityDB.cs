using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AbilityID
{
    None, Blaze, Overgrow
}

public class AbilityDB
{
    public static void Init()
    {
        foreach (var kvp in Abilities)
        {
            var abilityName = kvp.Key;
            var ability = kvp.Value;

            ability.Name = abilityName;
            //beast.FrontSprite = Resources.Load<Sprite>("Sprites/Brown");
        }
    }

    public static Dictionary<AbilityID, Ability> Abilities { get; set; } = new Dictionary<AbilityID, Ability>()
    {
        {
            AbilityID.Blaze,
            new Ability()
            {
                OnCheckAbility = (Beast attacker) =>
                {
                    if(attacker.ModifiedStats[StatID.HP] < (attacker.Stats[StatID.HP] * .5) && attacker.AbilityActivated == false)
                    {
                        Debug.Log("While ability activated");
                        attacker.ModifiedStats[StatID.Attack] += (int)Math.Round(.5 * attacker.Stats[StatID.Attack]);
                        attacker.AbilityActivated = true;
                        Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} attack was raised from Blaze");

                    }else if(attacker.ModifiedStats[StatID.HP] >= (attacker.Stats[StatID.HP] * .5) && attacker.AbilityActivated == true)
                    {
                         Debug.Log("While ability deactivated");
                         attacker.ModifiedStats[StatID.Attack] -= (int)Math.Round(.5 * attacker.Stats[StatID.Attack]);
                         attacker.AbilityActivated = false;
                         Beast.BattleDialog.Enqueue($"{Beast.FoeString(attacker)} {attacker.Name} attack was lowered as its Blaze dissapates");
                    }

                    
                },
            }
        },
    };
    
}
