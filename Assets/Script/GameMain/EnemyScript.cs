using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : UnitScript
{
    [SerializeField]
    public int id;                  //  �G�͂�����ID�ݒ肷��
    public Vector3 goalPos;   //  �S�[���n�_
    private Vector3[] routePos;     //  �S�[���Ɍ������܂ł̓�
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        InitStatus(id);
        if(agent == null) agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = unitData.getMoveSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead()) Destroy(gameObject);
        float dt = Time.deltaTime;
        if (!AttackActionCheck(dt)) //  �U���Ώۂ�����Έړ����Ȃ�
        {
            MoveAction(dt);
        }

    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    /// <param name="dt">�f���^�^�C��</param>
    private void MoveAction(float dt)
    {
        agent.destination = goalPos;
        agent.isStopped = false;

    }

    private bool AttackActionCheck(float dt)
    {
        if (targetObj == null) return false;   //  �U���Ώۖ���
        AttackAction(targetObj, dt);
        return true;
    }

    private void OnCollisionEnter(Collision col)
    {
        if( LayerMask.LayerToName(col.gameObject.layer) == "Player")
        {
            if (targetObj != null) return;  //  �܂��U���Ώۂ�����Ȃ�X���[
            targetObj = col.gameObject;
            ResetAtkWaitTime();
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
        }
    }
}
