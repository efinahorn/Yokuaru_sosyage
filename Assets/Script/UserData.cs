using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

[Serializable]
public class UserData
{
    //  キャラクターの所持情報とかとか
    public string playerName; //  プレイヤーの名前
    public int playerLevel;    //  プレイヤーのレベル
    public int gameMoney;      //  ゲーム内貨幣
    public int jewel;          //  とくべつな石
    public UnitData[] units;
    public int[] team;         //  チームデータ(Unitsの番号だけ格納しておく)
    public int maxUnit;        //  ユニットの最大所持数
    
    public void InitUserData()
    {
        playerName = "";
        playerLevel = 1;
        gameMoney = 0;
        jewel = 0;
        maxUnit = 100;
        units = new UnitData[maxUnit];
        team = new int[8];  //  とりあえずマジックナンバー
    }
    public void CreateUserData()
    {
        playerName = "testさん";
        playerLevel = 1;
        gameMoney = 0;
        jewel = 0;
        maxUnit = 100;
        units = new UnitData[maxUnit];
        for(int n=0; n<5; n++)
        {
            units[n] = new UnitData(n+1);
            team[n] = n;    //  格納場所を入れる
        }
        
    }

}
