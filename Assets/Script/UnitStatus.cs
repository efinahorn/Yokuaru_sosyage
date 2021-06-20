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

    //  �ς��Ȃ��l ��b�U���͂Ƃ��͂�����
    [SerializeField]
    private string unitName;    //  ���j�b�g��
    [SerializeField]
    private AtkType atkType;
    [SerializeField]
    private Element element;
    [SerializeField]
    private int id, maxLv, maxHp, atk, def;    //  ID�A�ő僌�x���A�̗́A�U���́A�h���
    [SerializeField]
    private float upAtkRate, upDefRate, upHpRate;    //  ���x���ɂ��U���́A�h��A�̗͂̏㏸�{�� 1.0�ɉ��Z���Ă����`
    [SerializeField]
    private float moveSpeed, atkSpeed, skillSpeed;    //  �ړ����x�A�U�����x�A�X�L�������\���x�B�b�ԂɊ�̌v�Z
    [SerializeField]
    private GameObject prefab;  //  ���j�b�g�̃v���n�u���

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
