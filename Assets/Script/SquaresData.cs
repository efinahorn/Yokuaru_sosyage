using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SquareType{
    EMPTY = 0,
    WALL = 1,
    SPAWN = 2,
    GOAL = 3,
    MAX,
}

public class SquaresData : MonoBehaviour
{
    public byte squareType;    //  �}�X�̃^�C�v�B0�Ȃ��A1�͕ǁA2�͓G�X�|�[���A3�͓G���������S�[��
    public byte squareNum;     //  �����^�C�v�����݂��Ă����ꍇ�̋�ʔԍ��B�G���ǂ���Ɍ��������Ƃ������߂�B
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSquareData( byte type, byte num)
    {
        squareType = type;
        squareNum = num;
        Debug.Log("TYPE:" + type + ", NUM:" + num);
    }
}
