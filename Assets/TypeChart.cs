using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Typing
{
    None, Rock, Aquatic, Corrupt, Mystic, Sacred, Glacial, Static, Nature, Native, Toxic, Hostile, Aerial
}

public class TypeChart
{
    
    static float[][] chart ={
                                //Def
        //Ata                     Roc  Aqu  Cor  Mys  Sac  Gla  Sta  Nar  Nav  Tox Hos Aer
            /*Roc*/ new float[] {.5f, .5f,  1f,  1f,  2f, .5f,  2f, .5f,  1f,  1f,  2f,  1f},
            /*Aqu*/ new float[] { 2f, .5f,  1f,  1f,  2f,  1f, .5f, .5f,  1f,  2f,  1f, .5f},
            /*Cor*/ new float[] { 1f,  1f, .5f,  2f, .5f,  1f,  1f,  1f,  2f,  1f, .5f,  1f},
            /*Mys*/ new float[] { 1f,  1f, .5f, .5f,  1f,  1f,  1f,  1f, .5f,  2f,  2f,  1f},
            /*Sac*/ new float[] {.5f, .5f,  2f,  1f, .5f,  2f,  1f,  2f,  1f,  1f,  1f,  1f},
            /*Gla*/ new float[] { 2f,  1f,  1f,  1f, .5f, .5f,  1f,  2f,  1f,  1f, .5f,  2f},
            /*Sta*/ new float[] {.5f,  2f,  1f,  1f,  1f,  1f, .5f,  1f,  1f, .5f,  1f,  2f},
            /*Nar*/ new float[] { 2f,  2f,  1f,  1f, .5f, .5f,  1f, .5f,  1f,  2f,  1f, .5f},
            /*Nav*/ new float[] { 1f,  1f, .5f,  2f,  1f,  1f,  1f,  1f,  1f, .5f, .5f,  2f},
            /*Tox*/ new float[] { 1f, .5f,  1f, .5f,  1f,  1f,  2f, .5f,  2f, .5f,  1f,  1f},
            /*Hos*/ new float[] {.5f,  1f,  2f, .5f,  1f,  2f,  1f,  1f,  2f,  1f, .5f, .5f},
            /*Aer*/ new float[] { 1f,  2f,  1f,  1f,  1f, .5f, .5f,  2f, .5f,  1f,  2f, .5f},
    };

    public static float GetEffectiveness(Typing moveType, Typing beastType, Typing beastType2)
    {
        Debug.Log("info1 " + moveType + " " + beastType);
        int row = (int)moveType - 1;
        int col = (int)beastType - 1;
        Debug.Log("info2 " + row + " " + col);

        float firstInteraction = chart[row][col];
        float secondInteraction;
        Debug.Log("info3 " + firstInteraction);

        if (beastType2 == Typing.None)
        {
            secondInteraction = 1;
        }
        else
        {
            col = (int)beastType2 - 1;
            secondInteraction = chart[row][col];
        }

        return firstInteraction * secondInteraction;
    }

    public static string GetEffectivenessPhrase(float effectiveness)
    {
        switch (effectiveness)
        {
                case .5f:
                    return "It was not very effective.";
                case 2f:
                    return "It was super effective.";
                case 4f:
                    return "It was extremly effective.";
                default:
                    return "";
                   
        }
    }

    
}
