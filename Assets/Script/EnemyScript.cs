using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : UnitStatus
{
    private GameObject targetObj;   //  �U���Ώ�
    private Vector3 targetPos;   //  �S�[���n�_
    private Vector3[] routePos;     //  �S�[���Ɍ������܂ł̓�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        if (!AttackActionCheck(dt)) //  �U���Ώۂ�����Έړ����Ȃ�
        {
            MoveAction(dt);
        }

    }

    private IEnumerator RouteSearch()
    {

        yield return 0;
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    /// <param name="dt">�f���^�^�C��</param>
    private void MoveAction(float dt)
    {

    }

    private bool AttackActionCheck(float dt)
    {
        if (!targetObj) return false;   //  �U���Ώۖ���
        AttackAction(targetObj, dt);
        return true;
    }

    private void OnCollisionEnter(Collision col)
    {
        if( col.gameObject.layer == LayerMask.GetMask("Player"))
        {
            if (targetObj) return;  //  �܂��U���Ώۂ�����Ȃ�X���[
            targetObj = col.gameObject;
            ResetAtkWaitTime();
        }
    }
}
