using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : UnitStatus
{
    private GameObject targetObj;   //  攻撃対象
    private Vector3 targetPos;   //  ゴール地点
    private Vector3[] routePos;     //  ゴールに向かうまでの道

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        if (!AttackActionCheck(dt)) //  攻撃対象がいれば移動しない
        {
            MoveAction(dt);
        }

    }

    private IEnumerator RouteSearch()
    {

        yield return 0;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="dt">デルタタイム</param>
    private void MoveAction(float dt)
    {

    }

    private bool AttackActionCheck(float dt)
    {
        if (!targetObj) return false;   //  攻撃対象無し
        AttackAction(targetObj, dt);
        return true;
    }

    private void OnCollisionEnter(Collision col)
    {
        if( col.gameObject.layer == LayerMask.GetMask("Player"))
        {
            if (targetObj) return;  //  まだ攻撃対象がいるならスルー
            targetObj = col.gameObject;
            ResetAtkWaitTime();
        }
    }
}
