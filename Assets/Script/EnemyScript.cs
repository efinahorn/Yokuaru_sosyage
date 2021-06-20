using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : UnitScript
{
    [SerializeField]
    public int id;                  //  敵はここでID設定する
    public Vector3 goalPos;   //  ゴール地点
    private Vector3[] routePos;     //  ゴールに向かうまでの道
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
        if (!AttackActionCheck(dt)) //  攻撃対象がいれば移動しない
        {
            MoveAction(dt);
        }

    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="dt">デルタタイム</param>
    private void MoveAction(float dt)
    {
        agent.destination = goalPos;
        agent.isStopped = false;

    }

    private bool AttackActionCheck(float dt)
    {
        if (targetObj == null) return false;   //  攻撃対象無し
        AttackAction(targetObj, dt);
        return true;
    }

    private void OnCollisionEnter(Collision col)
    {
        if( LayerMask.LayerToName(col.gameObject.layer) == "Player")
        {
            if (targetObj != null) return;  //  まだ攻撃対象がいるならスルー
            targetObj = col.gameObject;
            ResetAtkWaitTime();
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
        }
    }
}
