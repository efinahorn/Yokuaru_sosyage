using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    [SerializeField]
    private int maxHp, atk, def;    //  体力、攻撃力、防御力
    [SerializeField]
    private float moveSpeed, atkSpeed;    //  移動速度、攻撃速度。秒間に幾つの計算

    //  可変する値
    public int hp; //  現在体力
    public float atkWaitTime;  //  攻撃待機時間。atkSpeedまでいったら攻撃

    /// <summary>
    /// 攻撃速度に応じて攻撃する。
    /// </summary>
    /// <param name="target">攻撃対象</param>
    /// <param name="dt">デルタタイム</param>
    public void AttackAction(GameObject target, float dt)
    {
        atkWaitTime += dt;
        if (atkWaitTime >= atkSpeed)
        {
            target.GetComponent<UnitStatus>().DamageAction(this);
            ResetAtkWaitTime();    //  攻撃したのでリセット
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="us"></param>
    public void DamageAction(UnitStatus us)
    {

    }

    public void ResetAtkWaitTime()
    {
        atkWaitTime = 0;
    }
}