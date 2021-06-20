using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ユニットの情報。これをプレイヤーは所持する。
/// レベルや経験値、他可変するステータスはここで管理。保存データに反映すること
/// 敵ユニットを生成する場合もこの情報で生成する
/// </summary>
[Serializable]
public class UnitData
{
    public int id;
    public int Lv;
    public int Exp;

    [NonSerialized]
    private UnitStatus UnitStatus;

    public UnitStatus unitStatus
    {
        get
        { 
            if(UnitStatus == null) UnitStatus = new UnitStatus(GameSession.Instance.unitDataBase.findUnitId(id));
            return UnitStatus; 
        }
        set { UnitStatus = value; }
    }




    public UnitData(int id)
    {
        unitStatus = new UnitStatus(GameSession.Instance.unitDataBase.findUnitId(id));
        this.id = id;
        Lv = 1;
        Exp = 0;
    }

    public string getUnitName()
    {
        return unitStatus.getUnitName();
    }
    public float getAtkSpeed()
    {
        float atkSpd = unitStatus.getAtkSpeed();
        return atkSpd;
    }
    public float getMoveSpeed()
    {
        float moveSpd = unitStatus.getMoveSpeed();
        return moveSpd;
    }
    public float getSkillSpeed()
    {
        float skillSpd = unitStatus.getSkillSpeed();
        return skillSpd;
    }
    public int getMaxHp()
    {
        int maxHp = unitStatus.getMaxHp();
        maxHp = (int)(maxHp * (1.0f + ((float)Lv * unitStatus.getUpHpRate())));
        return maxHp;
    }
    public int getMaxLv()
    {
        int maxLv = unitStatus.getMaxLv();
        return maxLv;
    }
    public int getAtk()
    {
        int atk = unitStatus.getAtk();
        atk = (int)(atk * (1.0f + ((float)Lv * unitStatus.getUpAtkRate())));
        return atk;
    }
    public int getDef()
    {
        int def = unitStatus.getDef();
        def = (int)(def * (1.0f + ((float)Lv * unitStatus.getUpDefRate())));
        return def;
    }
    public GameObject getPrefab()
    {
        return unitStatus.getPrefab();
    }
}
