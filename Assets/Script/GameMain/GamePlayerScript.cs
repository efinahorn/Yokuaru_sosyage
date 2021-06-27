using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GamePlayerScript : MonoBehaviour
{
    public Camera cameraObj;
    private RaycastHit hit;
    public StageData stageData;
    public PlayerUnitScript[] unitsData;
    public UnitData waitingUnit;    //  待機中。これが有るときに設置可能箇所をクリックすると配置される

    // Start is called before the first frame update
    void Start()
    {
        unitsData = new PlayerUnitScript[5];
        StartCoroutine(CheckUnit());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //マウスがクリックされたら
        {
            Ray ray = cameraObj.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit))
            {
                Vector3 point = new Vector3((float)Math.Round(hit.point.x, MidpointRounding.AwayFromZero),0, (float)Math.Round(hit.point.z, MidpointRounding.AwayFromZero));
                if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Player")   //  もし設置したユニットだった場合
                {
                    PlayerUnitScript pus = hit.collider.gameObject.GetComponent<PlayerUnitScript>();
                    pus.UsedSkill();
                }
                else
                {
                    int sqtype = stageData.GetSquareData(point);
                    if (sqtype == -1)    //  範囲外
                    {
                        Debug.Log("範囲外です");
                    }
                    else if (sqtype == (int)StageData.SquareType.WALL || sqtype == (int)StageData.SquareType.SPAWN || sqtype == (int)StageData.SquareType.GOAL)
                    {
                        Debug.Log("設置できません");
                    }
                    else if(waitingUnit.id > 0)
                    {
                        GameObject obj = Instantiate(waitingUnit.getPrefab(), new Vector3(point.x, 0, point.z), Quaternion.identity);
                        obj.GetComponent<UnitScript>().InitStatus(waitingUnit.id);
                    }

                }
            }
        }
    }

    private IEnumerator CheckUnit()
    {
        while (true)
        {
            for(int i = 0; i < unitsData.Length; i++)
            {
                if(unitsData[i] == null) continue;
                if (unitsData[i].isDead())
                {
                    unitsData[i] = null; //  多分こんなことしなくても消えてるとは思う
                    continue;
                }
            }
            yield return 0;
        }
        yield return 0;
    }

    /// <summary>
    /// ボタンで基本呼ぶ。配置モードに入る
    /// </summary>
    /// <param name="unit"></param>
    public void SetUnitMode(int teamnum)
    {
        UnitData unitData = GameSession.Instance.userData.units[teamnum];
        if (Array.IndexOf(unitsData, unitData) != -1) return;   //  配置済
        waitingUnit = unitData;
    }
}
