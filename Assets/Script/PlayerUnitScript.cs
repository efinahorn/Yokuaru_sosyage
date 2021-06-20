using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[�����ł̓������Ǘ�
/// </summary>
public class PlayerUnitScript : UnitScript
{
    public float skillCast; //  �X�L�������܂�
    // Start is called before the first frame update
    private PlayerUnitScript(UnitData ud)
    {
        unitData = ud;
        lv = unitData.Lv;
    }
    public void InitPlayerUnit(UnitData ud)
    {
        unitData = ud;
        hp = unitData.getMaxHp();
        lv = unitData.Lv;

    }
    // Update is called once per frame
    void Update()
    {
        if (isDead()) Destroy(gameObject);
        float dt = Time.deltaTime;
        skillCast += dt;
        if (targetObj != null) AttackAction(targetObj, dt);
    }

    public void UsedSkill()
    {
        if (skillCast >= unitData.getSkillSpeed())
        {
            //  �X�L������
            Debug.Log("�X�L������");
            skillCast = 0;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (LayerMask.LayerToName(col.gameObject.layer) == "Enemy")
        {
            if (targetObj) return;  //  �܂��U���Ώۂ�����Ȃ�X���[
            targetObj = col.gameObject;
            ResetAtkWaitTime();
        }
    }
}
