using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    GameObject HPBarGO;
    GameObject EnemyHPBarGO;

    void Awake()
    {
        HPBarGO = GameObject.Find("HP Bar");
        EnemyHPBarGO = GameObject.Find("HP Bar Enemy");
    }

    public void SetHP(float hpNormalized)
    {
        if(hpNormalized < 0) hpNormalized = 0;
        HPBarGO.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    public void SetEnemyHP(float hpNormalized)
    {
        if (hpNormalized < 0) hpNormalized = 0;
        EnemyHPBarGO.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    public IEnumerator SetHPSmoothly(float newHP)
    {
        //IsUpdating = true;

        float curHP = HPBarGO.transform.localScale.x;
        float changeAmt = curHP - newHP;
        Debug.Log("changeAmt " + changeAmt);
        Debug.Log("changeAmt newHP " + newHP);
        Debug.Log("changeAmt curHP " + curHP);

        while (curHP - newHP > Mathf.Epsilon)
        {
            curHP -= changeAmt * Time.deltaTime;
            if (curHP < 0) break;
            HPBarGO.transform.localScale = new Vector3(curHP, 1f);
            //stop Coroutine and start it again in the next frame
            yield return null;
        }
        if (newHP < 0) newHP = 0;
        HPBarGO.transform.localScale = new Vector3(newHP, 1f);

        //IsUpdating = false;
    }

    public IEnumerator SetEnemyHPSmoothly(float newHP)
    {
        //IsUpdating = true;

        float curHP = EnemyHPBarGO.transform.localScale.x;
        float changeAmt = curHP - newHP;
        Debug.Log("changeAmt Enemy" + changeAmt);
        Debug.Log("changeAmt Enemy newHP " + newHP);
        Debug.Log("changeAmt Enemy curHP " + curHP);

        while (curHP - newHP > Mathf.Epsilon)
        {
            curHP -= changeAmt * Time.deltaTime;
            if (curHP < 0) break;
            EnemyHPBarGO.transform.localScale = new Vector3(curHP, 1f);
            //stop Coroutine and start it again in the next frame
            yield return null;
        }
        if (newHP < 0) newHP = 0;
        EnemyHPBarGO.transform.localScale = new Vector3(newHP, 1f);

        //IsUpdating = false;
    }
}
