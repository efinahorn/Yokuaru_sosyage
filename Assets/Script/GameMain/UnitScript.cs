using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのユニット、敵ユニットでのアクションをここで処理する
/// </summary>
public class UnitScript : MonoBehaviour
{
    protected UnitData unitData;

    //  可変する値
    public int lv, hp; //  現在レベル、現在体力
    public float atkWaitTime;  //  攻撃待機時間。atkSpeedまでいったら攻撃
    protected GameObject targetObj;   //  攻撃対象

    public void InitStatus(int id)
    {
        if(unitData == null) unitData = new UnitData(id);
        hp = unitData.getMaxHp();
        atkWaitTime = 0;
    }

    /// <summary>
    /// 攻撃速度に応じて攻撃する。
    /// </summary>
    /// <param name="target">攻撃対象</param>
    /// <param name="dt">デルタタイム</param>
    public void AttackAction(GameObject target, float dt)
    {
        atkWaitTime += dt;
        if (atkWaitTime >= unitData.getAtkSpeed())
        {
            if(target.GetComponent<UnitScript>().DamageAction(this))
            {
                //  キルしたらターゲット外す
                targetObj = null;
            }
            ResetAtkWaitTime();    //  攻撃したのでリセット
        }
    }

    /// <summary>
    /// ダメージ処理。体力無くなったら死亡
    /// とりあえず基本的には王道を征くアルテリオス計算式で。気になったら後で変える
    /// </summary>
    /// <param name="attackerSt"></param>
    public bool DamageAction(UnitScript attackerSt)
    {
        int dmg = attackerSt.unitData.getAtk() - unitData.getDef();
        if(dmg <= 0)    //  ダメージが全く無い場合
        {
            dmg = Random.Range(1, 3);   //  1～3のダメージは与える
        }
        hp -= dmg;
        if(hp <= 0)
        {
            //  死亡
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public void ResetAtkWaitTime()
    {
        atkWaitTime = 0;
    }

    public bool isDead()
    {
        return hp <= 0;
    }

}