using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPNumbers : MonoBehaviour
{
    GameObject HPNumberGO;
    GameObject EnemyHPNumberGO;

    void Awake()
    {
        HPNumberGO = GameObject.Find("HP Number");
        EnemyHPNumberGO = GameObject.Find("HP Number Enemy");
    }

    public IEnumerator SetHPSmooth(float newHP, float curHP)
    {
        //IsUpdating = true;

        //int.TryParse(CurrentHP, out int curHPInt);
        //int curHPInt = 
        //float curHP = (float)curHPInt;



        //float changeAmt = curHP - newHP;

        //while (curHP - newHP > Mathf.Epsilon)
        //{
        //    curHP -= changeAmt * Time.deltaTime;
        //    currentHP.text = curHP.ToString("F0"); //new Vector3(curHP, 1f); 
        //    //stop Coroutine and start it again in the next frame
        //    yield return null;
        //}
        //currentHP.text = newHP.ToString();

        return null;
        //IsUpdating = false;
    }
}
