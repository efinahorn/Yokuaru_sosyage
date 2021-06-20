using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEvent : MonoBehaviour
{
    public StageData stageData;
    public int maxEnemy;    //  ���̃X�e�[�W�ŏo�Ă���G��
    private int enemyCnt;   //  �o�����G�̐�
    public float spawnRate; //  �o�Ă���p�x
    private float spawnTimeCnt; //  �X�|�[�����ԃJ�E���g
    public GameObject endText;

    // Start is called before the first frame update
    void Start()
    {
        const string map_matrix = "300000000000:" +
                                    "300000000000:" +
                                    "111111111100:" +
                                    "111111111100:" +
                                    "111111111100:" +
                                    "111111111100:" +
                                    "111111111100:" +
                                    "200000000000:" +
                                    "200000000000";
        stageData.setData(map_matrix);
    }

    // Update is called once per frame
    void Update()
    {
        if( enemyCnt < maxEnemy)
        {
            spawnTimeCnt += Time.deltaTime;
            if (spawnTimeCnt > spawnRate)
            {
                Vector3 spawnPos = stageData.GetSpawnPos(-1);
                if (spawnPos.x < 0) //  �Ȃ�������X�e�[�W�����K�v����
                {
                    Debug.Log("Nothing spawn square.");
                    return;
                }
                GameObject obj = Instantiate(GameSession.Instance.unitDataBase.findUnitId(10000).getPrefab(), new Vector3(spawnPos.x, 0, spawnPos.z), Quaternion.identity);
                EnemyScript es = obj.GetComponent<EnemyScript>();
                es.goalPos = stageData.GetGoalPos(-1);
                enemyCnt++;
                spawnTimeCnt = 0;
            }
        }
        else
        {
            //  �o��������c�����G������
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            if( enemys.Length <= 0)
            {
                endText.SetActive(true);
                GameSession.Instance.SceneChange("Title", 1.0f);
                Destroy(this);
            }
        }
    }

}
