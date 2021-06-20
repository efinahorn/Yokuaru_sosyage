using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "UnitStatus", menuName = "CreateUnitStatus")]
public class UnitStatus : ScriptableObject
{
    public enum AtkType
    {
        CUBE,
        SPHERE,
        CYLINDER,
        CAPSULE
    }
    public enum Element
    {
        RED,
        GREEN,
        BLUE,
    }

    //  可変しない値 基礎攻撃力とかはここで
    [SerializeField]
    private string unitName;    //  ユニット名
    [SerializeField]
    private AtkType atkType;
    [SerializeField]
    private Element element;
    [SerializeField]
    private int id, maxLv, maxHp, atk, def;    //  ID、最大レベル、体力、攻撃力、防御力
    [SerializeField]
    private float upAtkRate, upDefRate, upHpRate;    //  レベルによる攻撃力、防御、体力の上昇倍率 1.0に加算していく形
    [SerializeField]
    private float moveSpeed, atkSpeed, skillSpeed;    //  移動速度、攻撃速度、スキル発動可能速度。秒間に幾つの計算
    [SerializeField]
    private GameObject prefab;  //  ユニットのプレハブ情報

    public UnitStatus(UnitStatus us)
    {
        this.unitName = us.getUnitName();
        this.atkType = us.getAtkType();
        this.element = us.getElement();
        this.id = us.getID();
        this.maxLv = us.getMaxLv();
        this.maxHp = us.getMaxHp();
        this.atk = us.getAtk();
        this.def = us.getDef();
        this.upAtkRate = us.getUpAtkRate();
        this.upDefRate = us.getUpDefRate();
        this.upHpRate = us.getUpHpRate();
        this.moveSpeed = us.getMoveSpeed();
        this.atkSpeed = us.getAtkSpeed();
        this.skillSpeed = us.getSkillSpeed();
        this.prefab = us.getPrefab();
    }

    public int getID()
    {
        return id;
    }
    public float getAtkSpeed()
    {
        return atkSpeed;
    }
    public float getMoveSpeed()
    {
        return moveSpeed;
    }
    public float getSkillSpeed()
    {
        return skillSpeed;
    }
    public int getMaxHp()
    {
        return maxHp;
    }
    public int getMaxLv()
    {
        return maxLv;
    }
    public int getAtk()
    {
        return atk;
    }
    public int getDef()
    {
        return def;
    }
    public string getUnitName()
    {
        return unitName;
    }
    public AtkType getAtkType()
    {
        return atkType;
    }
    public Element getElement()
    {
        return element;
    }
    public float getUpAtkRate()
    {
        return upAtkRate;
    }
    public float getUpDefRate()
    {
        return upDefRate;
    }
    public float getUpHpRate()
    {
        return upHpRate;
    }
    public GameObject getPrefab()
    {
        return prefab;
    }
}
