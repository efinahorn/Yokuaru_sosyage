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
    public UnitData waitingUnit;    //  �ҋ@���B���ꂪ�L��Ƃ��ɐݒu�\�ӏ����N���b�N����Ɣz�u�����

    // Start is called before the first frame update
    void Start()
    {
        unitsData = new PlayerUnitScript[5];
        StartCoroutine(CheckUnit());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //�}�E�X���N���b�N���ꂽ��
        {
            Ray ray = cameraObj.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit))
            {
                Vector3 point = new Vector3((float)Math.Round(hit.point.x, MidpointRounding.AwayFromZero),0, (float)Math.Round(hit.point.z, MidpointRounding.AwayFromZero));
                if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Player")   //  �����ݒu�������j�b�g�������ꍇ
                {
                    PlayerUnitScript pus = hit.collider.gameObject.GetComponent<PlayerUnitScript>();
                    pus.UsedSkill();
                }
                else
                {
                    int sqtype = stageData.GetSquareData(point);
                    if (sqtype == -1)    //  �͈͊O
                    {
                        Debug.Log("�͈͊O�ł�");
                    }
                    else if (sqtype == (int)StageData.SquareType.WALL || sqtype == (int)StageData.SquareType.SPAWN || sqtype == (int)StageData.SquareType.GOAL)
                    {
                        Debug.Log("�ݒu�ł��܂���");
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
                    unitsData[i] = null; //  ��������Ȃ��Ƃ��Ȃ��Ă������Ă�Ƃ͎v��
                    continue;
                }
            }
            yield return 0;
        }
        yield return 0;
    }

    /// <summary>
    /// �{�^���Ŋ�{�ĂԁB�z�u���[�h�ɓ���
    /// </summary>
    /// <param name="unit"></param>
    public void SetUnitMode(int teamnum)
    {
        UnitData unitData = GameSession.Instance.userData.units[teamnum];
        if (Array.IndexOf(unitsData, unitData) != -1) return;   //  �z�u��
        waitingUnit = unitData;
    }
}
