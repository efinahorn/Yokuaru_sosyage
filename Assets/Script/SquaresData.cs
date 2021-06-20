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
    public byte squareType;    //  マスのタイプ。0なら空、1は壁、2は敵スポーン、3は敵が向かうゴール
    public byte squareNum;     //  同じタイプが存在していた場合の区別番号。敵がどちらに向かうかとかを決める。
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
