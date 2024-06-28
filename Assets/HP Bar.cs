using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    GameObject HPBarGO;
    GameObject EnemyHPBarGO;
    TMPro.TextMeshProUGUI HPNumberText;
    TMPro.TextMeshProUGUI EnemyHPNumberText;
    //GameObject EnemyHPNumberGO;

    void Awake()
    {
        HPBarGO = GameObject.Find("HP Bar");
        EnemyHPBarGO = GameObject.Find("HP Bar Enemy");
        HPNumberText = GameObject.Find("PlayerUnitUI/Player HP").GetComponent<TMPro.TextMeshProUGUI>();
        EnemyHPNumberText = GameObject.Find("EnemyUnitUI/Enemy HP").GetComponent<TMPro.TextMeshProUGUI>();
        //EnemyHPNumberGO = GameObject.Find("HP Number Enemy");
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

    // public IEnumerator SetHPSmoothly(float newHP)
    // {
    //     //IsUpdating = true;

    //     float curHP = HPBarGO.transform.localScale.x;
    //     float changeAmt = curHP - newHP;
    //     Debug.Log("changeAmt " + changeAmt);
    //     Debug.Log("changeAmt newHP " + newHP);
    //     Debug.Log("changeAmt curHP " + curHP);

    //     while (curHP - newHP > Mathf.Epsilon)
    //     {
    //         curHP -= changeAmt * Time.deltaTime;
    //         if (curHP < 0) break;
    //         HPBarGO.transform.localScale = new Vector3(curHP, 1f);
    //         //stop Coroutine and start it again in the next frame
    //         yield return null;
    //     }
    //     if (newHP < 0) newHP = 0;
    //     HPBarGO.transform.localScale = new Vector3(newHP, 1f);

    //     //IsUpdating = false;
    // }

    // public IEnumerator SetEnemyHPSmoothly(float newHP)
    // {
    //     //IsUpdating = true;

    //     float curHP = EnemyHPBarGO.transform.localScale.x;
    //     float changeAmt = curHP - newHP;
    //     curHP -= changeAmt * Time.deltaTime;
    //     //HPNumberGO.GetComponent<TMPro.TextMeshProUGUI>().text = "HP " + curHP.ToString("F0");
    //     Debug.Log("changeAmt Enemy" + changeAmt);
    //     Debug.Log("changeAmt Enemy newHP " + newHP);
    //     Debug.Log("changeAmt Enemy curHP " + curHP);

    //     while (curHP - newHP > Mathf.Epsilon)
    //     {
    //         curHP -= changeAmt * Time.deltaTime;
    //         if (curHP < 0) break;
    //         EnemyHPBarGO.transform.localScale = new Vector3(curHP, 1f);
    //         //stop Coroutine and start it again in the next frame
    //         yield return null;
    //     }
    //     if (newHP < 0) newHP = 0;
    //     EnemyHPBarGO.transform.localScale = new Vector3(newHP, 1f);

    //     //IsUpdating = false;
    // }

    public IEnumerator SetTheHPSmoothly(float newHP, Beast beast)
    {
        //IsUpdating = true;

        if (beast.IsPlayer)
        {

            float curHP = HPBarGO.transform.localScale.x;
            float changeAmt = curHP - newHP;
            Debug.Log("changeAmt Enemy" + changeAmt);
            Debug.Log("changeAmt Enemy newHP " + newHP);
            Debug.Log("changeAmt Enemy curHP " + curHP);
            float newHPNumb = (float)beast.ModifiedStats[StatID.HP];
            //float curHPNumb = (float)int.Parse(HPNumberText);
            //int curHPNumb = int.Parse(HPNumberText);
            //string something = HPNumberText;
            //int curHPNumb = int.Parse("200");
            float curHPNumb = (float)int.Parse(HPNumberText.text);
            //Debug.Log("num " + int.Parse(HPNumberText));
            
            Debug.Log("num parse " + curHPNumb);
            //Debug.Log("num something " + something);

            float numChangeAmt = curHPNumb - newHPNumb;
            Debug.Log("damage newHPnumb" + newHPNumb);

            while (curHP - newHP > Mathf.Epsilon/*curHP != newHP*/)
            {
                curHPNumb -= numChangeAmt * Time.deltaTime;
                curHP -= changeAmt * Time.deltaTime;
                HPBarGO.transform.localScale = new Vector3(curHP, 1f);
                //curHPNumb -= numChangeAmt * Time.deltaTime;
                //Debug.Log("Num text " + HPNumberText);
                Debug.Log("Num cur " + curHPNumb);
                Debug.Log("Num new " + newHPNumb);
                //Debug.Log("Num newhp " + (float)beast.ModifiedStats[StatID.HP]);
                //Debug.Log("Num numchangeamt " + numChangeAmt);
                HPNumberText.text = curHPNumb.ToString("F0");
                if (curHP < 0) break;
                //HPNumberGO.GetComponent<TMPro.TextMeshProUGUI>().text = "HP " + curHPNumb.ToString("F0");
                //HPBarGO.transform.localScale = new Vector3(curHP, 1f);
                //stop Coroutine and start it again in the next frame
                yield return null;
            }
            if (newHP < 0) newHP = 0;
            HPBarGO.transform.localScale = new Vector3(newHP, 1f);
            //HPNumberText = curHPNumb.ToString("F0");
            //HPNumberGO.GetComponent<TMPro.TextMeshProUGUI>().text = "HP " + curHPNumb.ToString("F0");
        }
        else
        {

            float curHP = EnemyHPBarGO.transform.localScale.x;
            float changeAmt = curHP - newHP;
            
            float newHPNumb = (float)beast.ModifiedStats[StatID.HP];
            
            float curHPNumb = (float)int.Parse(EnemyHPNumberText.text);
          
            float numChangeAmt = curHPNumb - newHPNumb;

            while (curHP - newHP > Mathf.Epsilon)
            {
                curHPNumb -= numChangeAmt * Time.deltaTime;
                curHP -= changeAmt * Time.deltaTime;
                EnemyHPBarGO.transform.localScale = new Vector3(curHP, 1f);
                
                Debug.Log("Num cur " + curHPNumb);
                Debug.Log("Num new " + newHPNumb);
                
                EnemyHPNumberText.text = curHPNumb.ToString("F0");
                if (curHP < 0) break;
                
                yield return null;
            }
            if (newHP < 0) newHP = 0;
            EnemyHPBarGO.transform.localScale = new Vector3(newHP, 1f);
        }

    }

    public IEnumerator SetTheHPSmoothlyHeal(float newHP, Beast beast)
    {
        //IsUpdating = true;

        if (beast.IsPlayer)
        {
            float curHP = HPBarGO.transform.localScale.x;
            float changeAmt = curHP - newHP;
            Debug.Log("changeAmt " + changeAmt);
            Debug.Log("changeAmt newHP " + newHP);
            Debug.Log("changeAmt curHP " + curHP);

            while (curHP - newHP < Mathf.Epsilon/*curHP != newHP*/)
            {
                Debug.Log("changeAmt curHP " + curHP);
                curHP -= changeAmt * Time.deltaTime;
                HPBarGO.transform.localScale = new Vector3(curHP, 1f);
                if (curHP < 0) break;
                //HPBarGO.transform.localScale = new Vector3(curHP, 1f);
                //stop Coroutine and start it again in the next frame
                yield return null;
            }
            if (newHP < 0) newHP = 0;
            HPBarGO.transform.localScale = new Vector3(newHP, 1f);
        }
        else
        {

            float curHP = EnemyHPBarGO.transform.localScale.x;
            float changeAmt = curHP - newHP;
            Debug.Log("changeAmt Enemy" + changeAmt);
            Debug.Log("changeAmt Enemy newHP " + newHP);
            Debug.Log("changeAmt Enemy curHP " + curHP);

            while (curHP - newHP > Mathf.Epsilon)
            {
                curHP -= changeAmt * Time.deltaTime;
                EnemyHPBarGO.transform.localScale = new Vector3(curHP, 1f);
                if (curHP < 0) break;
                //EnemyHPBarGO.transform.localScale = new Vector3(curHP, 1f);
                //stop Coroutine and start it again in the next frame
                yield return null;
            }
            if (newHP < 0) newHP = 0;
            EnemyHPBarGO.transform.localScale = new Vector3(newHP, 1f);
        }

        //IsUpdating = false;
    }
}
