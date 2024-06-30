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


    public IEnumerator SetTheHPSmoothly(float newHP, Beast beast)
    {
        //IsUpdating = true;
        
        GameObject TheHPBarGO = beast.IsPlayer ? HPBarGO: EnemyHPBarGO;
        TMPro.TextMeshProUGUI TheHPNumberText = beast.IsPlayer ? HPNumberText: EnemyHPNumberText;

        float curHP = TheHPBarGO.transform.localScale.x;
        float changeAmt = curHP - newHP;
        
        float newHPNumb = (float)beast.ModifiedStats[StatID.HP];       
        float curHPNumb = (float)int.Parse(TheHPNumberText.text);       
        float numChangeAmt = curHPNumb - newHPNumb;

        while (curHP - newHP > Mathf.Epsilon)
        {
            curHP -= changeAmt * Time.deltaTime;
            TheHPBarGO.transform.localScale = new Vector3(curHP, 1f);
            
            curHPNumb -= numChangeAmt * Time.deltaTime;
            TheHPNumberText.text = curHPNumb.ToString("F0");

            if (curHP < 0) break;
            
            yield return null;
        }
        if (newHP < 0) newHP = 0;
        TheHPBarGO.transform.localScale = new Vector3(newHP, 1f);

    }

    public IEnumerator SetTheHPSmoothlyHeal(float newHP, Beast beast)
    {
        GameObject TheHPBarGO = beast.IsPlayer ? HPBarGO: EnemyHPBarGO;
        TMPro.TextMeshProUGUI TheHPNumberText = beast.IsPlayer ? HPNumberText: EnemyHPNumberText;
        //IsUpdating = true;

        float curHP = TheHPBarGO.transform.localScale.x;
        float changeAmt = curHP - newHP;

        float newHPNumb = (float)beast.ModifiedStats[StatID.HP];           
        float curHPNumb = (float)int.Parse(TheHPNumberText.text);          
        float numChangeAmt = curHPNumb - newHPNumb;

        while (curHP - newHP < Mathf.Epsilon/*curHP != newHP*/)
        {
            curHPNumb -= numChangeAmt * Time.deltaTime;
            TheHPNumberText.text = curHPNumb.ToString("F0");

            curHP -= changeAmt * Time.deltaTime;
            TheHPBarGO.transform.localScale = new Vector3(curHP, 1f);

            if (curHP < 0) break;
            //stop Coroutine and start it again in the next frame
            yield return null;
        }
        if (newHP < 0) newHP = 0;
        TheHPBarGO.transform.localScale = new Vector3(newHP, 1f);

    }
}
